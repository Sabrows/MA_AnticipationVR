using System;
using System.Collections;
using UnityEngine;

namespace _Trial
{
    /// <summary>
    /// This script is based on the tutorial:
    /// "Smooth Scene Fade Transition in VR"
    /// by @ValemTutorials
    /// <see cref="https://www.youtube.com/watch?v=JCyJ26cIM0Y"/> 
    /// </summary>
    public class ViewFader : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Fade Settings")] [SerializeField]
        private bool fadeOnStart = true;

        [SerializeField] private float fadeDuration = 2f;
        [SerializeField] private Color fadeColor = Color.black;

        [Header("Countdown Textures")] [SerializeField]
        private Texture2D[] countdownTextures;

        #endregion

        private Renderer _renderer;
        private int _countdownIndex;

        public static Action ViewFaded;

        private void Awake()
        {
            _countdownIndex = (int)fadeDuration - 1;

            _renderer = GetComponent<Renderer>();
            
            // Used for transition to Trial_Main scene
            if (fadeOnStart) Fade(1, 0, false, false);
        }

        public void FadeIn()
        {
            Fade(1, 0, true, true);
        }

        public void FadeOut()
        {
            Fade(0, 1, false, true);
        }

        private void Fade(float alphaIn, float alphaOut, bool useCountdown, bool invokeAction)
        {
            StartCoroutine(FadeRoutine(alphaIn, alphaOut, useCountdown, invokeAction));
        }

        private IEnumerator FadeRoutine(float alphaIn, float alphaOut, bool useCountdown, bool invokeAction)
        {
            float timer = 0.01f;
            while (timer <= fadeDuration)
            {
                if (useCountdown)
                {
                    float diff = fadeDuration - timer;
                    if ((int)diff != _countdownIndex)
                    {
                        _countdownIndex = (int)diff;
                        SetCountdownTexture();
                    }
                }

                Color newColor = fadeColor;
                newColor.a = useCountdown ? Mathf.Lerp(alphaIn, alphaOut, (timer / fadeDuration) - fadeDuration) : Mathf.Lerp(alphaIn, alphaOut, (timer / fadeDuration));
                _renderer.material.SetColor("_BaseColor", newColor);

                timer += Time.deltaTime;
                yield return null;
            }

            // After fade duration
            Color finalColor = fadeColor;
            finalColor.a = alphaOut;
            _renderer.material.SetColor("_BaseColor", finalColor);

            if (invokeAction) ViewFaded?.Invoke();
        }

        private void SetCountdownTexture()
        {
            Texture2D countdownTexture = countdownTextures[_countdownIndex];
            _renderer.material.SetTexture("_BaseMap", countdownTexture);
        }
    }
}