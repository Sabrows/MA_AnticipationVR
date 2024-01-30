using UnityEngine;
using UnityEngine.InputSystem;

namespace _Third_Party.Oculus_Hands.Scripts
{
    /// <summary>
    /// This script is based on the tutorial:
    /// "How To : Unity VR Basics 2023 – Input System and Hands (XR Interaction Toolkit)"
    /// by Fist Full of Shrimp
    /// <see cref="https://fistfullofshrimp.com/how-to-unity-vr-basics-2023-input-system-and-hands-xr-interaction-toolkit/"/> 
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimateHandController : MonoBehaviour
    {
        public InputActionReference gripInputActionReference;
        public InputActionReference triggerInputActionReference;

        private Animator _handAnimator;
        private float _gripValue;
        private float _triggerValue;

        void Start()
        {
            _handAnimator = GetComponent<Animator>();
        }

        void Update()
        {
            if (gameObject.CompareTag("RacketGrip"))
            {
                AnimateRacketGrip();
                return;
            }
            
            AnimateGrip();
            AnimateTrigger();
        }

        private void AnimateRacketGrip()
        {
            _handAnimator.SetFloat("Grip", 1);
            _handAnimator.SetFloat("Trigger", 1);
        }

        private void AnimateGrip()
        {
            _gripValue = gripInputActionReference.action.ReadValue<float>();
            _handAnimator.SetFloat("Grip", _gripValue);
        }

        private void AnimateTrigger()
        {
            _triggerValue = triggerInputActionReference.action.ReadValue<float>();
            _handAnimator.SetFloat("Trigger", _triggerValue);
        }
    }
}