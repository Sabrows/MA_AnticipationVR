using Enums;
using UnityEngine;

namespace _Trial
{
    public enum StringPointPosition
    {
        Center,
        TopRight,
        TopLeft
    }

    public class StringPointController : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private StringPointPosition stringPointPosition;

        [SerializeField] private float overlapSphereRadius = 0.50f;

        [SerializeField] private int maxColliders = 2;

        [SerializeField] private LayerMask answerVolumesLayer;

        #endregion

        private TrialMainManager _trialMainManager;
        private Vector3 _pointPosition;
        private TrialConditions.ShotPlacement _answerPlacement;

        private void OnEnable()
        {
            AnimationEventController.EndOfAnimReached += CheckColliderOverlap;
        }

        private void OnDisable()
        {
            AnimationEventController.EndOfAnimReached -= CheckColliderOverlap;
        }

        private void Start()
        {
            _trialMainManager = TrialMainManager.Instance;
        }

        private void CheckColliderOverlap()
        {
            Collider[] hitColliders = new Collider[maxColliders];

            int colliderCount = Physics.OverlapSphereNonAlloc(gameObject.transform.position, overlapSphereRadius,
                hitColliders, answerVolumesLayer);

            if (colliderCount > 1)
            {
                Debug.LogWarning($"More than one overlapping collider for {gameObject.name}. {colliderCount} in total.");
                _trialMainManager.SetAnswerPlacement(stringPointPosition, TrialConditions.ShotPlacement.ND);
                return;
            }

            if (colliderCount < 1)
            {
                _trialMainManager.SetAnswerPlacement(stringPointPosition, TrialConditions.ShotPlacement.ND);
                return;
            }

            string colliderTag = hitColliders[0].gameObject.tag;
            _answerPlacement = GetAnswerPlacement(colliderTag);

            // Call method on trial manager instance
            _trialMainManager.SetAnswerPlacement(stringPointPosition, _answerPlacement);
        }

        private TrialConditions.ShotPlacement GetAnswerPlacement(string colliderTag)
        {
            switch (colliderTag)
            {
                case "Cross":
                    return TrialConditions.ShotPlacement.Cross;
                case "Straight":
                    return TrialConditions.ShotPlacement.Straight;
                default:
                    return TrialConditions.ShotPlacement.ND;
            }
        }
    }
}