using System.Collections;
using TMPro;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// This script is based on the tutorial:
    /// "Unity VR Frames Per Second"
    /// by Fist Full of Shrimp
    /// <see cref="https://fistfullofshrimp.com/unity-vr-frames-per-second/"/> 
    /// </summary>
    public class FPSDisplayer : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private float targetFPS = 72f;
        [SerializeField] private float updateDelay;
        [SerializeField] private Color aboveTargetFPSColor;
        [SerializeField] private Color belowTargetFPSColor;

        #endregion

        private float _currentFPS;
        private float _deltaTime;
        private TextMeshProUGUI _textFPS;

        private void Awake()
        {
            _textFPS = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            StartCoroutine(DisplayFramesPerSecond());
        }

        void Update()
        {
            GenerateFramesPerSecond();
        }

        private void GenerateFramesPerSecond()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            _currentFPS = 1.0f / _deltaTime;
        }

        private IEnumerator DisplayFramesPerSecond()
        {
            while (true)
            {
                if (_currentFPS >= targetFPS)
                {
                    _textFPS.color = aboveTargetFPSColor;
                }
                else
                {
                    _textFPS.color = belowTargetFPSColor;
                }

                _textFPS.text = $"FPS: {_currentFPS.ToString(".0")}";
                yield return new WaitForSeconds(updateDelay);
            }
        }
    }
}