using Enums;
using Scriptable_Objects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Trial
{
    public class TrialStartManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private ViewFader viewFader;

        #endregion

        private TrialData _trialData;

        public static TrialStartManager Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
        }

        private void OnEnable()
        {
            ViewFader.ViewFaded += LoadTrialMainScene;
        }

        private void OnDisable()
        {
            ViewFader.ViewFaded -= LoadTrialMainScene;
        }

        private void Start()
        {
            GetTrialData();
        }

        private void GetTrialData()
        {
            _trialData = Resources.Load<TrialData>(@"Scriptable Objects/TrialData");
            if (_trialData == null) Debug.LogError($"TrialData.asset not found!");
        }

        public void SetTrialData(string playerId, PlayerHandedness playerHandedness, string testStartTimestamp)
        {
            SetPlayerId(playerId);
            SetPlayerHandedness(playerHandedness);
            SetTestStartTimestamp(testStartTimestamp);
        }

        private void LoadTrialMainScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #region TRIAL DATA SETTER

        private void SetPlayerId(string playerId)
        {
            _trialData.playerID = playerId;
        }

        private void SetPlayerHandedness(PlayerHandedness playerHandedness)
        {
            _trialData.playerHandedness = playerHandedness;
        }

        private void SetTestStartTimestamp(string testStartTimestamp)
        {
            _trialData.testStartTimestamp = testStartTimestamp;
        }

        #endregion
    }
}