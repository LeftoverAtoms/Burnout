using UnityEngine;

namespace Burnout
{
    public class Vehicle : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody body;

        [SerializeField]
        public Spring spring;

        public Axle[] axles = new Axle[2];

        private Vector3 input;

        //public GameObject wheelPrefab;
        public float wheelRadius;

        private void Start()
        {
            body = gameObject.GetComponent<Rigidbody>();

            axles[0] = new Axle(this, 0.85f);
            axles[1] = new Axle(this, 0.85f);
            axles[0].transform.localPosition = new Vector3(0f, 0.5f, 1.95f);
            axles[1].transform.localPosition = new Vector3(0f, 0.5f, 1.85f);
        }

        private void FixedUpdate()
        {
            axles[0].SimulateWheels();
            axles[1].SimulateWheels();
        }

        private void Update()
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        private void OnDrawGizmos()
        {
            axles[0].DrawGizmos();
            axles[1].DrawGizmos();
        }
    }
}