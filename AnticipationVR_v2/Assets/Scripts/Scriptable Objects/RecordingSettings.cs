#if (UNITY_EDITOR)

using System.IO;
using Enums;
using Unity.VisualScripting;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Recording Settings", order = 1)]
    [System.Serializable]
    public class RecordingSettings : ScriptableObject
    {
        [Header("Recording Settings")]
        [Tooltip("Recording of shuttle and racket animations only enabled during RECORDING mode.")]
        public RecordingMode recordingMode = RecordingMode.Training;

        [Tooltip("Direction of shot placement to be recorded.")]
        public TrialConditions.ShotPlacement shotPlacement;


        [Header("Animation Recorder Settings")] [Tooltip("If true, all the gameObject hierarchy will be recorded.")]
        public bool recursive = true;

        [Tooltip("Path were to store the recorded racket animations.")]
        public string racketOutputFolder = Application.dataPath + "/Animations/Racket";

        [Tooltip("Path were to store the recorded shuttle animations.")]
        public string shuttleOutputFolder = Application.dataPath + "/Animations/Shuttle";

        [Tooltip("Prefix for the output path showing the recorded trial conditions.")]
        public string trialConditionPrefix = " ";

        [Tooltip("Keyframe reduction level to use to compress the recorded animation curve data.")]
        public AnimationInputSettings.CurveSimplificationOptions curveSimplification =
            AnimationInputSettings.CurveSimplificationOptions.Lossless;

        [Tooltip(
            "If true, the Recorder sets the generated animation key tangents to ClampedAuto. Clamped tangents are useful to prevent curve overshoots when the animation data is discontinuous.")]
        public bool clampedTangents = true;


        [Header("Recorder Controller Settings")]
        [Tooltip("Allows setting and retrieving the frame rate for the current list of Recorders.")]
        public float frameRate = 60f;

        [Tooltip("Indicates the type of frame rate (constant or variable) for the current list of Recorders.")]
        public FrameRatePlayback frameRatePlayback = FrameRatePlayback.Constant;

        [Tooltip(
            "Indicates if the Recorders frame rate should cap the Unity rendering frame rate. When enabled, Unity is prevented from rendering faster than the set FrameRate.")]
        public bool capFrameRate = true;

        [Tooltip("Instructs the recorder to exit Play Mode once the recording has finished.")]
        public bool exitPlayMode = false;
    }
}

public enum RecordingMode
{
    Training,
    Recording
}

#endif