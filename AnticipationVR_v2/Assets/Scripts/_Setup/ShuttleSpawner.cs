#if UNITY_EDITOR

using Scriptable_Objects;
using UnityEngine;

namespace _Setup
{
    public class ShuttleSpawner : MonoBehaviour
    {
        private SetupManager _setupManager;
        private ShuttleSettings _shuttleSettings;
        private ShuttleAnimationRecorder _shuttleAnimationRecorder;
        private CountdownController _countdownController;

        private Transform _shuttleSpawnerTransform;
        private GameObject _shuttle;
        private Rigidbody _shuttleRigidbody;
        private Quaternion _shuttleOrigRotation;

        private void Awake()
        {
            _shuttleSpawnerTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            _setupManager = SetupManager.Instance;
            _shuttleSettings = _setupManager.ShuttleSettings;
            _shuttleAnimationRecorder = _setupManager.gameObject.GetComponent<ShuttleAnimationRecorder>();
            _countdownController = _setupManager.gameObject.GetComponent<CountdownController>();

            _shuttle = _shuttleAnimationRecorder.shuttleGameObject;
            _shuttleRigidbody = _shuttle.GetComponent<Rigidbody>();
            _shuttleOrigRotation = _shuttle.transform.rotation;
        }

        private void Update()
        {
            if (_shuttleSpawnerTransform.position != transform.position) _shuttleSpawnerTransform = transform;

            Debug.DrawRay(_shuttleSpawnerTransform.position,
                _shuttleSpawnerTransform.up * _shuttleSettings.shuttleSpawnForce, Color.blue, 10f);
        }

        // Callback from CountdownController
        public void SpawnShuttle()
        {
            SetRigidbodyProperties();

            Vector3 spawnForce = _shuttleSpawnerTransform.up * _shuttleSettings.shuttleSpawnForce;
            _shuttleRigidbody.AddForce(spawnForce, ForceMode.VelocityChange);

            _shuttle.GetComponent<FlightPathAligner>().enabled = true;
            _shuttle.GetComponentInChildren<LineRenderer>().enabled = true;

            _countdownController.RestartTimer();
        }

        private void SetRigidbodyProperties()
        {
            _shuttleRigidbody.useGravity = true;
            _shuttleRigidbody.isKinematic = false;
            _shuttleRigidbody.mass = _shuttleSettings.shuttleMass;
            _shuttleRigidbody.drag = _shuttleSettings.shuttleDrag;
            _shuttleRigidbody.angularDrag = _shuttleSettings.shuttleAngDrag;
        }

        public void ResetShuttlePosition()
        {
            _shuttle.transform.position = _shuttleSpawnerTransform.position;
            _shuttle.transform.rotation = _shuttleOrigRotation;
            _shuttleRigidbody.useGravity = false;
            _shuttleRigidbody.isKinematic = true;
        }
    }
}

#endif