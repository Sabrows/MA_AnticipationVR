using System;
using Enums;
using UnityEngine;

namespace _Trial
{
    public class AnimationEventController : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private GameObject shuttle;

        #endregion

        private TrialMainManager _trialMainManager;
        private bool _noOcclusion = false;

        public static Action EndOfAnimReached;

        private void Start()
        {
            _trialMainManager = TrialMainManager.Instance;
        }

        public void CheckTempOcclCondition_None()
        {
            if (_trialMainManager.CurrentTrialItem.tempOcclCondition != TrialConditions.TempOcclCondition.None) return;
            _noOcclusion = true;
        }

        public void CheckTempOcclCondition_BI_42MS()
        {
            if (_noOcclusion) return;
            if (_trialMainManager.CurrentTrialItem.tempOcclCondition !=
                TrialConditions.TempOcclCondition.BI_42MS) return;
            Debug.Log("Triggered temporal occlusion condition: 42ms BEFORE impact");
            HideShuttle();
        }

        public void CheckTempOcclCondition_AI_126MS()
        {
            if (_noOcclusion) return;
            if (_trialMainManager.CurrentTrialItem.tempOcclCondition !=
                TrialConditions.TempOcclCondition.AI_126MS) return;
            Debug.Log("Triggered temporal occlusion condition: 126ms AFTER impact");
            HideShuttle();
        }

        public void CheckTempOcclCondition_AI_336MS()
        {
            if (_noOcclusion) return;
            if (_trialMainManager.CurrentTrialItem.tempOcclCondition !=
                TrialConditions.TempOcclCondition.AI_336MS) return;
            Debug.Log("Triggered temporal occlusion condition: 336ms AFTER impact");
            HideShuttle();
        }

        public void InvokeEndOfAnim()
        {
            EndOfAnimReached?.Invoke();
        }

        public void ResetOcclusion()
        {
            if (_noOcclusion) _noOcclusion = false;
            shuttle.GetComponent<MeshRenderer>().enabled = true;
        }

        private void HideShuttle()
        {
            shuttle.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}