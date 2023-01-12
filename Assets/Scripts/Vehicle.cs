using UnityEngine;

namespace Burnout
{
    public class Vehicle : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody Body;

        [SerializeField]
        public Spring Spring;

        public Axle[] Axles = new Axle[2];

        private Vector3 InputDirection;

        public float WheelRadius;

        private void Start()
        {
            Body = gameObject.GetComponent<Rigidbody>();
            Body.centerOfMass = transform.position;

            Axles[0] = new Axle(new Vector3(0, 0.5f, 1.95f), 0.85f, this);
            Axles[1] = new Axle(new Vector3(0, 0.5f, -1.85f), 0.85f, this);
        }

        private void Update()
        {
            InputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Body.AddRelativeForce(InputDirection.z * 2 * Vector3.forward, ForceMode.Acceleration);
            Body.AddRelativeTorque(InputDirection.x * 0.25f * Vector3.up, ForceMode.Acceleration);

            Axles[0].SimulateWheels();
            Axles[1].SimulateWheels();
        }

        private void OnDrawGizmos()
        {
            Axles[0].DrawGizmos();
            Axles[1].DrawGizmos();
        }
    }
}