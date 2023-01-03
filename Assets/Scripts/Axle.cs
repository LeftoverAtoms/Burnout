using UnityEngine;

namespace Burnout
{
    public class Axle : MonoBehaviour
    {
        public Wheel leftWheel;
        public Wheel rightWheel;

        public float antiRollStrength = 1;

        public float localWheelPosition;

        private void Awake()
        {
            leftWheel = this.gameObject.AddComponent<Wheel>();
            rightWheel = this.gameObject.AddComponent<Wheel>();

            leftWheel.transform.localPosition = transform.localPosition + (Vector3.left * localWheelPosition);
            rightWheel.transform.localPosition = transform.localPosition + (Vector3.right * localWheelPosition);
        }

        private void FixedUpdate()
        {
            Vector3 antiRollForce = Vector3.up * ((leftWheel.spring.offset - rightWheel.spring.offset) * antiRollStrength);

            leftWheel.ApplyForce(leftWheel.wishVelocity + antiRollForce);
            rightWheel.ApplyForce(rightWheel.wishVelocity + antiRollForce);
        }
    }
}