using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Trial Items", order = 4)]
    [System.Serializable]
    public class TrialItems : ScriptableObject
    {
        [Tooltip("List and order of selected trial items.")]
        public List<TrialItem> trialItemsList;
        
        // Keep this to avoid trial items getting reset
        private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}