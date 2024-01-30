using System;
using Enums;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Trial Data", order = 5)]
    [Serializable]
    public class TrialData : ScriptableObject
    {
        [Header("General Data")] [Tooltip("ID submitted by the player to fetch corresponding questionnaire results.")]
        public string playerID;

        [Tooltip("Preferred racket hand to use during the trials.")]
        public PlayerHandedness playerHandedness;

        [Tooltip("Number of the trial.")] public int trialNr;


        [Header("Anticipation Data")] [Tooltip("Value if answer placement of center point was correct or not.")]
        public bool correctlyAnticipated_centerPoint;

        [Tooltip("Value if answer placement of top right point was correct or not.")]
        public bool correctlyAnticipated_topRightPoint;

        [Tooltip("Value if answer placement of top left point was correct or not.")]
        public bool correctlyAnticipated_topLeftPoint;

        [Tooltip("Placement of the center point of the racket strings at the end of the shuttle flight animation.")]
        public string answerPlacement_centerPoint;

        [Tooltip("Placement of the top right point of the racket strings at the end of the shuttle flight animation.")]
        public string answerPlacement_topRightPoint;

        [Tooltip("Placement of the top left point of the racket strings at the end of the shuttle flight animation.")]
        public string answerPlacement_topLeftPoint;


        [Header("Repeatability Data")] [Tooltip("ID of the shot used in the trial.")]
        public string shotID;

        [Tooltip("Shot placement used in the trial.")]
        public string shotPlacement;

        [Tooltip("Temporal occlusion condition used in the trial.")]
        public string tempOcclCondition;

        [Tooltip("Spatial occlusion condition used in the trial.")]
        public string spatOcclCondition;


        [Header("Timestamp Data")] [Tooltip("Timestamp before beginning the trials.")]
        public string testStartTimestamp;

        [Tooltip("Timestamp at the beginning of each trial.")]
        public string trialStartTimestamp;
    }
}