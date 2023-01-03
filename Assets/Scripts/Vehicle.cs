using UnityEngine;

namespace Burnout
{
    public class Vehicle : MonoBehaviour
    {
        public Axle[] axles = new Axle[2];

        public Rigidbody body;
        private Vector3 input;

        [SerializeField]
        public Spring spring;

        public GameObject wheelPrefab;
        public float wheelRadius;

        private void FixedUpdate()
        {
            foreach(var axle in axles)
            {
                axle.FixedUpdate();
            }
        }

        private void Update()
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}