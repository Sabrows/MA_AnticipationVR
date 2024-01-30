#if (UNITY_EDITOR) 

using Scriptable_Objects;
using UnityEngine;

namespace _Setup
{
    /// <summary>
    /// This script is based on the answer to a prompt
    /// by ChatGPT
    /// <see cref="https://chat.openai.com/share/4bbb2331-3f33-4b40-a917-915ea1cb5c2f"/>
    /// </summary>
    public class FlightPathAligner : MonoBehaviour
    {
        private SetupManager _setupManager;
        private ShuttleSettings _shuttleSettings;
        private Rigidbody _shuttleRigidbody;

        private void Awake()
        {
            _shuttleRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _setupManager = SetupManager.Instance;
            _shuttleSettings = _setupManager.ShuttleSettings;
        }

        private void Update()
        {
            // Get the current velocity of the shuttlecock
            Vector3 velocity = _shuttleRigidbody.velocity;

            // Calculate the rotation to align the cork towards the velocity
            Quaternion targetRotation = Quaternion.LookRotation(-velocity.normalized);
            targetRotation *= Quaternion.Euler(90, 0, 0);

            // Smoothly rotate the shuttlecock towards the target rotation
            _shuttleRigidbody.rotation = Quaternion.Slerp(_shuttleRigidbody.rotation, targetRotation,
                _shuttleSettings.shuttleAlignSpeed * Time.deltaTime);
        }
    }
}

#endif