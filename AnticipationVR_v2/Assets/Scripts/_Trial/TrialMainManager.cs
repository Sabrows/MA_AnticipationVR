using System;
using System.Collections.Generic;
using Enums;
using Scriptable_Objects;
using UnityEngine;

namespace _Trial
{
    public class TrialMainManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Trial Items")] [SerializeField]
        private TrialItems trialItems;

        [Header("Trial Animators")] [SerializeField]
        private Animator trialAnimator_cr;

        [SerializeField] private Animator trialAnimator_st;
        [SerializeField] private float animTriggerOffset;

        [Header("Trial Canvas")] [SerializeField]
        private TrialCanvasController trialCanvasController;

        [Header("Player Hand Controller")] [SerializeField]
        private PlayerHandController playerHandController;

        #endregion

        //private static readonly int ConstantSeed = 20230914;

        private TrialItem _currTrialItem;
        private int _currentTrialIndex = 0;
        private Animator _currTrialAnimator;
        private string _currShotTriggerID;
        private TrialData _trialData;
        private TrialDataSaver _trialDataSaver;
        private int _setAnswerPlacementIndex = 0;

        private Dictionary<string, int> _correctRoundsPerCondition = new();
        private Dictionary<string, int> _totalRoundsPerCondition = new();

        public static TrialMainManager Instance;

        public TrialItem CurrentTrialItem
        {
            get => _currTrialItem;
        }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);

            /*// Set the random seed for Unity's random generator
            UnityEngine.Random.InitState(ConstantSeed);

            // Shuffle the list using the constant seed
            ShuffleTrialItemsWithSeed();*/
        }

        /*private void ShuffleTrialItemsWithSeed()
        {
            for (int i = 0; i < trialItems.trialItemsList.Count; i++)
            {
                TrialItem trialItem = trialItems.trialItemsList[i];
                int randomIndex = UnityEngine.Random.Range(i, trialItems.trialItemsList.Count);
                Debug.Log($"randomIndex: {randomIndex}");
                trialItems.trialItemsList[i] = trialItems.trialItemsList[randomIndex];
                trialItems.trialItemsList[randomIndex] = trialItem;
            }
        }*/

        private void OnEnable()
        {
            ViewFader.ViewFaded += StartTrial;
        }

        private void OnDisable()
        {
            ViewFader.ViewFaded -= StartTrial;
        }

        private void Start()
        {
            // Create trial data scriptable object
            GetTrialData();

            // Set racket and ray interactor hand according to playerHandedness
            playerHandController.SetHandControllers(_trialData.playerHandedness);

            // Set init values for canvas
            trialCanvasController.UpdateTrialText(_currentTrialIndex + 1, trialItems.trialItemsList.Count);
        }

        private void GetTrialData()
        {
            _trialData = Resources.Load<TrialData>(@"Scriptable Objects/TrialData");
            if (_trialData == null) Debug.LogError($"TrialData.asset not found!");
        }

        // Subscribed action from ScreenFader
        private void StartTrial()
        {
            // check if we are at least on penultimate Trial Item
            if (_currentTrialIndex + 1 == trialItems.trialItemsList.Count)
                trialCanvasController.DisplayFeedbackButton();

            // get current trial item
            _currTrialItem = trialItems.trialItemsList[_currentTrialIndex];

            // set trial data
            SetTrialData();

            // update current index
            _currentTrialIndex++;

            // update trial text for upcoming trial
            trialCanvasController.UpdateTrialText(_currentTrialIndex + 1, trialItems.trialItemsList.Count);

            // setup trial
            SetupTrial();

            // trigger animation after time offset
            Invoke(nameof(TriggerAnimation), animTriggerOffset);
        }

        private void SetTrialData()
        {
            SetTrialShotID();
            SetTrialNr();
            SetTrialConditions();
            SetTrialStartTimestamp();
        }

        private void SetupTrial()
        {
            switch (_currTrialItem.shotPlacement)
            {
                case TrialConditions.ShotPlacement.Cross:
                    _currTrialAnimator = trialAnimator_cr;
                    _currShotTriggerID = "cr_" + _currTrialItem.shotID;
                    trialAnimator_cr.gameObject.SetActive(true);
                    trialAnimator_cr.gameObject.GetComponent<AnimationEventController>().ResetOcclusion();
                    trialAnimator_st.gameObject.SetActive(false);
                    break;
                case TrialConditions.ShotPlacement.Straight:
                    _currTrialAnimator = trialAnimator_st;
                    _currShotTriggerID = "st_" + _currTrialItem.shotID;
                    trialAnimator_cr.gameObject.SetActive(false);
                    trialAnimator_st.gameObject.SetActive(true);
                    trialAnimator_st.gameObject.GetComponent<AnimationEventController>().ResetOcclusion();
                    break;
            }
        }

        private void TriggerAnimation()
        {
            _currTrialAnimator.SetTrigger(_currShotTriggerID);
        }

        public int[] GetStatResults(string condition)
        {
            int[] correctOutOfTotalRounds = new int[2];

            int correctRounds = _correctRoundsPerCondition.ContainsKey(condition)
                ? _correctRoundsPerCondition[condition]
                : 0;
            int totalRounds = _totalRoundsPerCondition.ContainsKey(condition) ? _totalRoundsPerCondition[condition] : 0;

            correctOutOfTotalRounds[0] = correctRounds;
            correctOutOfTotalRounds[1] = totalRounds;
            return correctOutOfTotalRounds;
        }

        #region TRIAL DATA SETTER

        private void SetTrialShotID()
        {
            _trialData.shotID = _currTrialItem.shotID;
        }

        private void SetTrialNr()
        {
            _trialData.trialNr = _currentTrialIndex;
        }

        private void SetTrialConditions()
        {
            _trialData.shotPlacement = _currTrialItem.shotPlacement.ToString();
            _trialData.tempOcclCondition = _currTrialItem.tempOcclCondition.ToString();
            _trialData.spatOcclCondition = _currTrialItem.spatOcclCondition.ToString();
        }

        private void SetTrialStartTimestamp()
        {
            string currentDateTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            _trialData.trialStartTimestamp = currentDateTime;
        }

        public void SetAnswerPlacement(StringPointPosition stringPointPosition,
            TrialConditions.ShotPlacement answerPlacement)
        {
            switch (stringPointPosition)
            {
                case StringPointPosition.Center:
                    _trialData.answerPlacement_centerPoint = answerPlacement.ToString();
                    _setAnswerPlacementIndex++;
                    break;

                case StringPointPosition.TopRight:
                    _trialData.answerPlacement_topRightPoint = answerPlacement.ToString();
                    _setAnswerPlacementIndex++;
                    break;

                case StringPointPosition.TopLeft:
                    _trialData.answerPlacement_topLeftPoint = answerPlacement.ToString();
                    _setAnswerPlacementIndex++;
                    break;
            }

            // TODO hacky way to check if every string point sent a position
            if (_setAnswerPlacementIndex == 3)
            {
                // reset answer index
                _setAnswerPlacementIndex = 0;

                // Set correct anticipation values
                SetCorrectAnticipation();

                // Call data saver
                //_trialDataSaver.SaveTrialData(_trialData);
                TrialDataSaver.SaveTrialData(_trialData);
            }
        }

        private void SetCorrectAnticipation()
        {
            _trialData.correctlyAnticipated_centerPoint =
                (_trialData.answerPlacement_centerPoint == _currTrialItem.shotPlacement.ToString());
            _trialData.correctlyAnticipated_topRightPoint =
                (_trialData.answerPlacement_topRightPoint == _currTrialItem.shotPlacement.ToString());
            _trialData.correctlyAnticipated_topLeftPoint =
                (_trialData.answerPlacement_topLeftPoint == _currTrialItem.shotPlacement.ToString());

            // check if at least one point was correctly anticipated
            if (_trialData.correctlyAnticipated_centerPoint || _trialData.correctlyAnticipated_topRightPoint ||
                _trialData.correctlyAnticipated_topLeftPoint)
            {
                UpdateStats(_currTrialItem.shotPlacement.ToString(), true);
                UpdateStats(_currTrialItem.tempOcclCondition.ToString(), true);
                UpdateStats(_currTrialItem.spatOcclCondition.ToString(), true);
                UpdateStats("Total", true);
            }
            else
            {
                UpdateStats(_currTrialItem.shotPlacement.ToString(), false);
                UpdateStats(_currTrialItem.tempOcclCondition.ToString(), false);
                UpdateStats(_currTrialItem.spatOcclCondition.ToString(), false);
                UpdateStats("Total", false);
            }
        }

        #endregion

        private void UpdateStats(string condition, bool correctlyAnticipated)
        {
            if (!_correctRoundsPerCondition.ContainsKey(condition))
            {
                _correctRoundsPerCondition[condition] = 0;
                _totalRoundsPerCondition[condition] = 0;
            }

            if (correctlyAnticipated)
            {
                _correctRoundsPerCondition[condition]++;
                _totalRoundsPerCondition[condition]++;
            }
            else _totalRoundsPerCondition[condition]++;
        }
    }
}