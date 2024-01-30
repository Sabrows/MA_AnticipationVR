using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Trial
{
    public class TrialCanvasController : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Trial Content")] [SerializeField]
        private TMP_Text trialText;

        [SerializeField] private Button nextTrialButton;

        [Header("Feedback Content")] [SerializeField]
        private Button feedbackButton;

        [Space(5)] [SerializeField] private TMP_Text shotPlacement_stValue;
        [SerializeField] private TMP_Text shotPlacement_crValue;

        [Space(10)] [SerializeField] private TMP_Text tempOccl_NoneValue;
        [SerializeField] private TMP_Text tempOccl_42BIValue;
        [SerializeField] private TMP_Text tempOccl_126AIValue;
        [SerializeField] private TMP_Text tempOccl_336AIValue;

        [Space(10)] [SerializeField] private TMP_Text spatOccl_visValue;
        [SerializeField] private TMP_Text spatOccl_hiddValue;

        [Space(10)] [SerializeField] private TMP_Text totalValue;

        #endregion

        private TrialMainManager _trialMainManager;

        private void Awake()
        {
            TrialDataSaver.TrialDataSaved += ToggleTrialCanvas;
        }

        private void OnDestroy()
        {
            TrialDataSaver.TrialDataSaved -= ToggleTrialCanvas;
        }

        private void Start()
        {
            _trialMainManager = TrialMainManager.Instance;
        }

        public void ToggleTrialCanvas()
        {
            // Display Canvas with next trial button
            bool inverseState = !gameObject.activeSelf;
            gameObject.SetActive(inverseState);
        }

        public void UpdateTrialText(int currentTrialIndex, int trialItemsLength)
        {
            trialText.text = $"Trial: {currentTrialIndex} / {trialItemsLength}";
        }

        public void DisplayFeedbackButton()
        {
            trialText.gameObject.SetActive(false);
            nextTrialButton.gameObject.SetActive(false);
            feedbackButton.gameObject.SetActive(true);
        }

        public void FetchStatData()
        {
            // ShotPlacement.Straight
            int[] shotPlacement_st =
                _trialMainManager.GetStatResults(TrialConditions.ShotPlacement.Straight.ToString());
            shotPlacement_stValue.text = $"{shotPlacement_st[0]} / {shotPlacement_st[1]} correct";

            // ShotPlacement.Cross
            int[] shotPlacement_cr =
                _trialMainManager.GetStatResults(TrialConditions.ShotPlacement.Cross.ToString());
            shotPlacement_crValue.text = $"{shotPlacement_cr[0]} / {shotPlacement_cr[1]} correct";


            // TempOcclCondition.None
            int[] tempOccl_none =
                _trialMainManager.GetStatResults(TrialConditions.TempOcclCondition.None.ToString());
            tempOccl_NoneValue.text = $"{tempOccl_none[0]} / {tempOccl_none[1]} correct";

            // TempOcclCondition.BI_42MS
            int[] tempOccl_42BI =
                _trialMainManager.GetStatResults(TrialConditions.TempOcclCondition.BI_42MS.ToString());
            tempOccl_42BIValue.text = $"{tempOccl_42BI[0]} / {tempOccl_42BI[1]} correct";

            // TempOcclCondition.AI_126MS
            int[] tempOccl_126AI =
                _trialMainManager.GetStatResults(TrialConditions.TempOcclCondition.AI_126MS.ToString());
            tempOccl_126AIValue.text = $"{tempOccl_126AI[0]} / {tempOccl_126AI[1]} correct";

            // TempOcclCondition.AI_336MS
            int[] tempOccl_336AI =
                _trialMainManager.GetStatResults(TrialConditions.TempOcclCondition.AI_336MS.ToString());
            tempOccl_336AIValue.text = $"{tempOccl_336AI[0]} / {tempOccl_336AI[1]} correct";


            // SpatOcclCondition.VisibleAvatar
            int[] spatOccl_vis =
                _trialMainManager.GetStatResults(TrialConditions.SpatOcclCondition.VisibleAvatar.ToString());
            spatOccl_visValue.text = $"{spatOccl_vis[0]} / {spatOccl_vis[1]} correct";

            // SpatOcclCondition.HiddenAvatar
            int[] spatOccl_hid =
                _trialMainManager.GetStatResults(TrialConditions.SpatOcclCondition.HiddenAvatar.ToString());
            spatOccl_hiddValue.text = $"{spatOccl_hid[0]} / {spatOccl_hid[1]} correct";


            // Total
            int[] total =
                _trialMainManager.GetStatResults("Total");
            totalValue.text = $"{total[0]} / {total[1]} shots";
        }

        public void EndTrial()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}