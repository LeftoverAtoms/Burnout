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

        private void Start()
        {
            axles[0] = gameObject.AddComponent<Axle>();
            axles[0].transform.localPosition = new Vector3(0f, 0.5f, 1.95f);
            axles[0].localWheelPosition = 0.85f;
            axles[1] = gameObject.AddComponent<Axle>();
            axles[1].transform.localPosition = new Vector3(0f, 0.5f, 1.85f);
            axles[1].localWheelPosition = 0.85f;
        }

        private void FixedUpdate()
        {
        }

        private void Update()
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}