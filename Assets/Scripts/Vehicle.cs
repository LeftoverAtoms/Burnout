using UnityEngine;

namespace StuntGame
{
    public class Vehicle : MonoBehaviour
    {
        public GameObject[] wheels;

        private Vector3 input;

        /*
        private Vector3 acceleration;
        private Vector3 friction;
        public Vector3 Velocity;
        private const float airResistence = 1;
        private float torque;
        */

        public float weight;
        public float dispersedWeight; // Weight that each wheel must support. (Based on amount of wheels on the ground)

        [Header("Wheels")]
        [SerializeField] public Spring spring;

        private void FixedUpdate()
        {
        }

        private void Update()
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}