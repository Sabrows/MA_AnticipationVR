using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using VRUiKits.Utils;

namespace _Trial
{
    public class IntroCanvasController : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Welcome Panel")] [SerializeField]
        private GameObject welcomePanel;

        [Header("Task Description Panel")] [SerializeField]
        private VideoPlayer videoPlayer;

        [SerializeField] private Button continueButton;
        
        [Header("Last Settings Panel")] [SerializeField]
        private UIKitInputField playerIDInputField;

        [SerializeField] private OptionsManager playerHandednessOptions;
        [SerializeField] private Button lastButton;

        #endregion

        private TrialStartManager _trialStartManager;

        void OnEnable()
        {
            videoPlayer.loopPointReached += EnableContinueButton;
        }

        void OnDisable()
        {
            videoPlayer.loopPointReached -= EnableContinueButton;
        }

        private void Start()
        {
            _trialStartManager = TrialStartManager.Instance;
            if (!welcomePanel.activeSelf) welcomePanel.SetActive(true);
        }

        private void EnableContinueButton(VideoPlayer source)
        {
            if (!continueButton.interactable) continueButton.interactable = true;
        }

        // Called by on-"Enter"-button click of keyboard
        public void EnableFinalButton()
        {
            if (!lastButton.interactable) lastButton.interactable = true;
        }

        // Called on-"Let's go!"-button click
        public void ForwardTrialData()
        {
            string playerId = playerIDInputField.text;
            PlayerHandedness playerHandedness = GetPlayerHandedness();
            string testStartTimestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            _trialStartManager.SetTrialData(playerId, playerHandedness, testStartTimestamp);
        }

        private PlayerHandedness GetPlayerHandedness()
        {
            switch (playerHandednessOptions.selectedValue)
            {
                case "Right-handed":
                    return PlayerHandedness.Right;
                case "Left-handed":
                    return PlayerHandedness.Left;
                default:
                    return PlayerHandedness.Right; // at least statistically speaking correct
            }
        }
    }
}