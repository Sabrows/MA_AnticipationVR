#if (UNITY_EDITOR)

using Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _Setup
{
    /// <summary>
    /// This script is based on the tutorial:
    /// "Configurable TIMER / STOPWATCH Unity Tutorial"
    /// by @BMoDev
    /// <see cref="https://www.youtube.com/watch?v=u_n3NEi223E"/>
    /// </summary>
    public class CountdownController : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Tooltip("Text to be updated by the countdown.")] [SerializeField]
        private TextMeshProUGUI countdownText;

        [Tooltip("Text visual cue to indicate active recording.")] [SerializeField]
        private GameObject recordingCue;

        [Tooltip("Function to call once the timer reached the limit.")] [SerializeField]
        private UnityEvent countdownCallback;

        #endregion

        public bool CountdownStarted
        {
            get => _countdownStarted;
            set => _countdownStarted = value;
        }

        private SetupManager _setupManager;
        private ShuttleSettings _shuttleSettings;

        private float _currentTime = 5f;
        private float _timerLimit = 0f;
        private bool _countdownStarted = false;

        private void Awake()
        {
            recordingCue.SetActive(false);
        }

        private void Start()
        {
            _setupManager = SetupManager.Instance;
            _shuttleSettings = _setupManager.ShuttleSettings;
            _currentTime = _shuttleSettings.shuttleSpawnTime;
        }

        private void Update()
        {
            Time.timeScale = 1f; // TODO: timeScale gets set to 0 automatically idk

            if (_countdownStarted)
            {
                _currentTime -= Time.deltaTime;

                if (_currentTime <= _timerLimit)
                {
                    _currentTime = _timerLimit;
                    SetTimerText();
                    countdownCallback?.Invoke();
                }

                SetTimerText();
            }
        }

        private void SetTimerText()
        {
            countdownText.text = _currentTime.ToString("0");
        }

        public void ToggleRecordingCue()
        {
            bool inverseState = !recordingCue.activeSelf;
            recordingCue.SetActive(inverseState);
        }

        public void RestartTimer()
        {
            _currentTime = _shuttleSettings.shuttleSpawnTime;
        }
    }
}

#endif