using Enums;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Trial
{
    public class PlayerHandController : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Left Hand Settings")] [SerializeField]
        private GameObject leftHandController;

        [SerializeField] private Transform leftHand_wRacket;

        [Header("Right Hand Settings")] [SerializeField]
        private GameObject rightHandController;

        [SerializeField] private Transform rightHand_wRacket;

        #endregion

        private Transform _leftHand;
        private Transform _rightHand;

        private void Start()
        {
            _leftHand = leftHandController.GetComponent<ActionBasedController>().modelPrefab;
            _rightHand = rightHandController.GetComponent<ActionBasedController>().modelPrefab;
        }

        public void SetHandControllers(PlayerHandedness playerHandedness)
        {
            switch (playerHandedness)
            {
                case PlayerHandedness.Right:
                    ToggleRayInteractorComponents(leftHandController, true);
                    ToggleRayInteractorComponents(rightHandController, false);

                    SetXRControllerModel(leftHandController, _leftHand);
                    SetXRControllerModelPrefab(rightHandController, rightHand_wRacket);
                    break;

                case PlayerHandedness.Left:
                    ToggleRayInteractorComponents(leftHandController, false);
                    ToggleRayInteractorComponents(rightHandController, true);

                    SetXRControllerModelPrefab(leftHandController, leftHand_wRacket);
                    SetXRControllerModel(rightHandController, _rightHand);
                    break;
            }
        }

        private void ToggleRayInteractorComponents(GameObject handController, bool enabledState)
        {
            // XR Ray Interactor
            handController.GetComponent<XRRayInteractor>().enabled = enabledState;

            // Line Renderer
            handController.GetComponent<LineRenderer>().enabled = enabledState;

            // XRInteractorLineVisual
            handController.GetComponent<XRInteractorLineVisual>().enabled = enabledState;
        }

        private void SetXRControllerModel(GameObject handController, Transform model)
        {
            handController.GetComponent<ActionBasedController>().model = model;
        }
        
        private void SetXRControllerModelPrefab(GameObject handController, Transform modelPrefab)
        {
            handController.GetComponent<ActionBasedController>().modelPrefab = modelPrefab;
        }
    }
}