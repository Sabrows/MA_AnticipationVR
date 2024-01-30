#if (UNITY_EDITOR) 

using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Shuttle Settings", order = 2)]
    [System.Serializable]
    public class ShuttleSettings : ScriptableObject
    {
        [Header("Spawn Settings")] [Tooltip("How often a new shuttle will be spawned from the ShuttleSpawner.")]
        public float shuttleSpawnTime = 8f;

        [Tooltip("Impulse force applied when spawning a shuttle from the ShuttleSpawner.")]
        public float shuttleSpawnForce = 4f;


        [Space(10)] [Tooltip("Toggle for the in-game visualization of the shuttle flight path.")]
        public bool visualizeFlightPath = true;

        [Tooltip("After how many frames the flight path visualization should be updated.")]
        public int visualizationRefreshRate = 10;

        [Tooltip("Speed to rotate the shuttle towards the target rotation.")]
        public float shuttleAlignSpeed = 20f;


        [Header("Rigidbody Properties")] [Space(10)] [Tooltip("Mass value of the shuttle rigidbody component.")]
        public float shuttleMass = 0.5f;

        [Tooltip("Drag value of the shuttle rigidbody  component.")]
        public float shuttleDrag = 0.3f;

        [Tooltip("Angular Drag value of the shuttle rigidbody component.")]
        public float shuttleAngDrag = 0.2f;
    }
}

#endif