using UnityEngine;

namespace Burnout
{
    public class Vehicle : MonoBehaviour
    {
        public Axle[] axles;

        public Rigidbody body;
        private Vector3 input;

        [SerializeField]
        public Spring spring;

        public GameObject wheelPrefab;
        public float wheelRadius;

        private void Update() { input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); }

        private void OnValidate()
        {
            if(axles.Length >= 2)
            {
                axles[0].name = "Front Axle";
                axles[axles.Length - 1].name = "Rear Axle";
            }
        }
    }
}