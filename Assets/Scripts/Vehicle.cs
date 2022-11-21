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
        public GameObject wheelModel;
        public float wheelRadius;

        private void Update()
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        /*
        private void GetWheelPosition()
        {
            if(Physics.Raycast(new Ray(transform.position, -transform.up), out RaycastHit hit, spring.maxRange + wheelRadius))
            {
                float prevLength = spring.length;

                spring.length = hit.distance - wheelRadius;
                spring.length = Mathf.Clamp(spring.length, spring.minRange, spring.maxRange);

                float velocity = (prevLength - spring.length) / Time.fixedDeltaTime;
                float force = spring.stiffness * (spring.restLength - spring.length) + spring.damping * velocity;

                body.AddForceAtPosition(force * transform.up, hit.point);
            }
        }
        */
    }
}