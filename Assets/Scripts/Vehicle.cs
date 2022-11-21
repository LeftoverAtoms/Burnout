using UnityEngine;

namespace StuntGame
{
    public class Vehicle : MonoBehaviour
    {
        public Rigidbody body;
        private Vector3 input;

        [SerializeField]
        public Spring spring;

        public Wheel[] wheels = new Wheel[4];
        public float wheelRadius;

        private void Start()
        {
            foreach(var wheel in wheels)
            {
                wheel.Init(this);
            }
        }

        private void FixedUpdate()
        {
            foreach(var wheel in wheels)
            {
                wheel.FixedUpdate();
            }
        }

        private void Update()
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}