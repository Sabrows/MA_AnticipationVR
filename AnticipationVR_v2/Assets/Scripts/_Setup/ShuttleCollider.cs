#if (UNITY_EDITOR) 

using UnityEngine;

namespace _Setup
{
    public class ShuttleCollider : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private ShuttleSpawner shuttleSpawner;

        # endregion
        
        private FlightPathAligner _flightPathAligner;
        private FlightPathVisualizer _flightPathVisualizer;

        private void Awake()
        {
            _flightPathAligner = GetComponent<FlightPathAligner>();
            _flightPathVisualizer = GetComponentInChildren<FlightPathVisualizer>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                _flightPathAligner.enabled = false;
                _flightPathVisualizer.ResetVisualizer();
                shuttleSpawner.ResetShuttlePosition();
            }
        }
    }
}

#endif