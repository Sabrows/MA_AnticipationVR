#if (UNITY_EDITOR) 

using Scriptable_Objects;
using UnityEngine;

namespace _Setup
{
    [RequireComponent(typeof(LineRenderer))]
    public class FlightPathVisualizer : MonoBehaviour
    {
        private SetupManager _setupManager;
        private ShuttleSettings _shuttleSettings;
        private LineRenderer _flightPathRenderer;
        private Rigidbody _shuttleRigidBody;
        private Vector3 _rigidbodyPosition;
        private int _refreshRateCounter = 0;
        private int _positionCount = 1;

        private void Awake()
        {
            _flightPathRenderer = GetComponent<LineRenderer>();
            _shuttleRigidBody = GetComponentInParent<Rigidbody>();
            _rigidbodyPosition = GetComponentInParent<Rigidbody>().position;
            _flightPathRenderer.SetPosition(0, _rigidbodyPosition);
        }

        private void Start()
        {
            _setupManager = SetupManager.Instance;
            _shuttleSettings = _setupManager.ShuttleSettings;
        }

        private void Update()
        {
            if (_shuttleSettings.visualizeFlightPath)
            {
                if (_refreshRateCounter >= _shuttleSettings.visualizationRefreshRate)
                {
                    _rigidbodyPosition = _shuttleRigidBody.position;
                    _positionCount = _flightPathRenderer.positionCount;
                    _positionCount++;
                    _flightPathRenderer.positionCount = _positionCount;
                    _flightPathRenderer.SetPosition(_positionCount - 1, _rigidbodyPosition);
                    _refreshRateCounter = 0;
                }

                _refreshRateCounter++;
            }
        }

        public void ResetVisualizer()
        {
            _flightPathRenderer.positionCount = 0;
            _flightPathRenderer.enabled = false;
        }
    }
}

#endif