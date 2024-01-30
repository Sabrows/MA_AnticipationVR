#if (UNITY_EDITOR) 

using System;
using Scriptable_Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Setup
{
    public class SetupManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Scriptable Objects")] [field: SerializeField]
        public RecordingSettings recordingSettings;

        [field: SerializeField] public ShuttleSettings shuttleSettings;

        [Header("Countdown Settings")] [SerializeField]
        private CountdownController countdownController;

        [SerializeField] private InputAction toggleCountdownAction;

        [Header("Recording Settings")] [SerializeField]
        private InputAction toggleRecordingAction;

        #endregion

        public static SetupManager Instance;
        
        public static Action RecordingStarted;
        public static Action RecordingStopped;

        public RecordingSettings RecordingSettings
        {
            get => recordingSettings;
        }

        public ShuttleSettings ShuttleSettings
        {
            get => shuttleSettings;
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);

            recordingSettings.recordingMode = RecordingMode.Training;

            toggleCountdownAction.performed += ToggleCountdown;
            toggleRecordingAction.performed += ToggleRecordingMode;
            SetTrialConditionPrefix();
        }

        private void OnEnable()
        {
            toggleCountdownAction.Enable();
            toggleRecordingAction.Enable();
        }

        private void OnDisable()
        {
            toggleCountdownAction.Disable();
            toggleRecordingAction.Disable();
        }

        private void SetTrialConditionPrefix()
        {
            string prefix = $"{RecordingSettings.shotPlacement}";
            RecordingSettings.trialConditionPrefix = prefix;
        }

        private void ToggleCountdown(InputAction.CallbackContext obj)
        {
            bool inverseState = !countdownController.CountdownStarted;
            countdownController.CountdownStarted = inverseState;
        }

        private void ToggleRecordingMode(InputAction.CallbackContext obj)
        {
            switch (recordingSettings.recordingMode)
            {
                case RecordingMode.Training:
                    Debug.Log("Setting mode to RECORDING");
                    recordingSettings.recordingMode = RecordingMode.Recording;
                    countdownController.ToggleRecordingCue();
                    RecordingStarted?.Invoke();
                    break;

                case RecordingMode.Recording:
                    Debug.Log("Setting mode to TRAINING");
                    recordingSettings.recordingMode = RecordingMode.Training;
                    countdownController.ToggleRecordingCue();
                    RecordingStopped?.Invoke();
                    break;
            }
        }
    }
}

#endif