using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Scriptable_Objects;
using UnityEngine;

namespace _Trial
{
    /// <summary>
    /// This script is based on the answer to a prompt
    /// by ChatGPT
    /// <see cref="https://chat.openai.com/share/08870bd3-b173-4f49-9f5a-6d2aa72a37b0"/>
    /// </summary>
    public class TrialDataSaver : MonoBehaviour
    {
        private const string FileName = "TrialResults.csv";
        private static string _filePath;
        private static FieldInfo[] _trialDataFields;

        public static Action TrialDataSaved;
        
        private void Awake()
        {
            _filePath = Path.Combine(Application.persistentDataPath, FileName);
            _trialDataFields =  typeof(TrialData).GetFields(BindingFlags.Public | BindingFlags.Instance);
        }
        
        public static void SaveTrialData(TrialData trialData)
        {
            List<string> dataRow = new List<string>();

            foreach (FieldInfo field in _trialDataFields)
            {
                object value = field.GetValue(trialData);
                dataRow.Add(value != null ? value.ToString() : "");
            }

            string dataRowString = string.Join(",", dataRow.ToArray());

            if (!File.Exists(_filePath))
            {
                string headerRow = string.Join(",", _trialDataFields.Select(f => f.Name).ToArray());
                File.WriteAllText(_filePath, headerRow + "\n");
            }

            File.AppendAllText(_filePath, dataRowString + "\n");
            
            // call next round
            TrialDataSaved?.Invoke();
        }

        /*
         * First custom approach to save as JSON failed due to File.AppendAllText resulting in multiple JSON root objects (not allowed).
         * As File.WriteAllText overwrites the existing content, previous results would have been lost after an application restart.
         * Second approach was to load previous results into a List<TrialData> on Start, add new TrialData to the list after each trial, and save it as JSON again.
         * While the serialisation of the list ended up working with the Newtonsoft Json package, the deserialization when loading the data on Start kept throwing problems.
         * It seems that the deserialization is trying to recreate the TrialData scriptable objects with new() instead of .CreateInstance().
         * Since using a .csv file is also an option in my case and saves me further headaches, I opted for the above solution.
         */
        
        /*public void SaveTrialData(TrialData trialData)
        {            
            string fileName = $"TrialResults.json";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            string trialDataJSON = JsonUtility.ToJson(trialData);

#if UNITY_STANDALONE
            File.WriteAllText(fileName, trialDataJSON);
#elif UNITY_ANDROID
            // string combinedPath = Path.Combine(Application.persistentDataPath, fileName);
            // Debug.Log("path: " + combinedPath);
            // File.WriteAllText(combinedPath, trialDataJSON);
#endif
            // call next round
            TrialDataSaved?.Invoke();
        }*/
    }
}