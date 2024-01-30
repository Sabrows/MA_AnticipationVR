#if UNITY_EDITOR

using Scriptable_Objects;
using UnityEditor.Recorder;
using UnityEngine;

namespace _Setup
{
    public class RacketAnimationRecorder : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private GameObject racketGameObject;

        # endregion

        private SetupManager _setupManager;
        private RecordingSettings _recordingSettings;
        private RecorderController _recorderController;
        private RecorderControllerSettings _recorderControllerSettings;
        private AnimationRecorderSettings _animationRecorderSettings;

        private void Awake()
        {
            _recorderControllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
            _recorderController = new RecorderController(_recorderControllerSettings);
            _animationRecorderSettings = ScriptableObject.CreateInstance<AnimationRecorderSettings>();
        }

        private void OnEnable()
        {
            SetupManager.RecordingStarted += StartRecording;
            SetupManager.RecordingStopped += StopRecording;
        }

        private void OnDisable()
        {
            SetupManager.RecordingStarted -= StartRecording;
            SetupManager.RecordingStopped -= StopRecording;
        }

        private void Start()
        {
            _setupManager = SetupManager.Instance;
            _recordingSettings = _setupManager.RecordingSettings;

            CreateNewAnimationRecorder();
        }

        private void CreateNewAnimationRecorder()
        {
            #region ANIMATION RECORDER SETTINGS

            _animationRecorderSettings.name = $"Racket Animation Recorder";
            _animationRecorderSettings.Enabled = true;

            // GameObject
            _animationRecorderSettings.AnimationInputSettings.gameObject = racketGameObject;

            // Recursive
            _animationRecorderSettings.AnimationInputSettings.Recursive = _recordingSettings.recursive;

            // AddComponentToRecord
            _animationRecorderSettings.AnimationInputSettings.AddComponentToRecord(typeof(Transform));

            // OutputFile
            _animationRecorderSettings.OutputFile =
                _recordingSettings.racketOutputFolder + "/Racket_" + _recordingSettings.trialConditionPrefix + "_" +
                DefaultWildcard.Take;

            // SimplyCurves
            _animationRecorderSettings.AnimationInputSettings.SimplyCurves = _recordingSettings.curveSimplification;

            // ClampedTangents
            _animationRecorderSettings.AnimationInputSettings.ClampedTangents = _recordingSettings.clampedTangents;

            #endregion

            #region RECORDER CONTROLLER SETTINGS

            // FrameRate
            _recorderControllerSettings.FrameRate = _recordingSettings.frameRate;

            // FrameRatePlayback
            _recorderControllerSettings.FrameRatePlayback = _recordingSettings.frameRatePlayback;

            // CapFrameRate
            _recorderControllerSettings.CapFrameRate = _recordingSettings.capFrameRate;

            // ExitPlayMode
            _recorderControllerSettings.ExitPlayMode = _recordingSettings.exitPlayMode;

            // SetRecordModeToManual
            _recorderControllerSettings.SetRecordModeToManual();

            #endregion

            _recorderControllerSettings.AddRecorderSettings(_animationRecorderSettings);
            _recorderController.PrepareRecording();
        }

        // Subscribed action from SetupManager
        private void StartRecording()
        {
            if (_recordingSettings.recordingMode == RecordingMode.Recording && !_recorderController.IsRecording())
            {
                Debug.Log($"{_animationRecorderSettings.name} starting to record.");
                _recorderController.StartRecording();
            }
        }

        // Subscribed action from SetupManager
        private void StopRecording()
        {
            if (_recorderController.IsRecording())
            {
                Debug.Log($"{_animationRecorderSettings.name} stopping to record.");
                _recorderController.StopRecording();
                _recorderController.PrepareRecording();
            }
        }
    }
}

#endif