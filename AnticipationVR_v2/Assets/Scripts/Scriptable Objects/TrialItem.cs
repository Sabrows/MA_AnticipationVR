using Enums;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Trial Item", order = 3)]
    [System.Serializable]
    public class TrialItem : ScriptableObject
    {
        [Header("Shot ID")] [Tooltip("Identifier of the displayed shot in this trial.")]
        public string shotID;

        [Header("Trial Conditions")] [Tooltip("Shot placement used in this trial.")]
        public TrialConditions.ShotPlacement shotPlacement;

        [Tooltip("Temporal occlusion condition used in this trial.")]
        public TrialConditions.TempOcclCondition tempOcclCondition;

        [Tooltip("Spatial occlusion condition used in this trial.")]
        public TrialConditions.SpatOcclCondition spatOcclCondition;
    }
}
