using UnityEngine;

namespace Burnout
{
    public class Wheel
    {
        public Transform transform;

        public Vehicle veh;
        public Axle axle;

        public Spring spring;

        public Wheel(Axle parent)
        {
            axle = parent;
            veh = axle.transform.parent.GetComponent<Vehicle>();

            transform.position = veh.transform.TransformPoint(transform.localPosition);
        }

        public void ApplySpringForce(Vector3 force)
        {
            force += GetSpringForce(100, 10);
            veh.body.AddForceAtPosition(veh.transform.TransformPoint(force), transform.position, ForceMode.Acceleration);
        }

        private Vector3 GetSpringForce(float degrees, float iterations)
        {
            degrees = Mathf.Clamp(degrees, 0, 360); // Arcs/circles are limited to 0-360 degrees.

            float angleDiff = (degrees / iterations); // Degrees between each raycast.
            float angleCurr = (angleDiff / 2);

            float rotationOffset = (angleDiff * (iterations / 2)) + (180 - degrees);

            float avgForce = 0;
            int contacts = 0;

            float force;

            for (uint i = 0; i < iterations; i++)
            {
                Quaternion dir = Quaternion.AngleAxis(angleCurr + rotationOffset, transform.right);

                if (Physics.Raycast(transform.position, dir * veh.transform.up, out RaycastHit hit, veh.wheelRadius + 0.1f))
                {
                    Vector3 velocity = veh.body.GetPointVelocity(transform.position);

                    spring.offset = veh.spring.restLength - hit.distance;

                    avgForce += (spring.offset * veh.spring.strength) - (velocity.y * veh.spring.damping);
                    contacts += 1;
                }

                angleCurr += angleDiff;
            }

            if (contacts > 0) { force = avgForce / contacts; }
            else { force = 0; }

            return new Vector3(0, force, 0);
        }

        public void DrawGizmos()
        {
            float degrees = 100;
            float iterations = 10;

            degrees = Mathf.Clamp(degrees, 0, 360); // Arcs/circles are limited to 0-360 degrees.

            float angleDiff = (degrees / iterations); // Degrees between each raycast.
            float angleCurr = (angleDiff / 2);

            float offset = (angleDiff * (iterations / 2)) + (180 - degrees);

            for (uint i = 0; i < iterations; i++)
            {
                float c = (i / iterations);
                if (c < 0.25f) { Gizmos.color = new Color(1, 0, 0, 1); }
                else if (c < 0.50f) { Gizmos.color = new Color(0, 1, 0, 1); }
                else if (c < 0.75f) { Gizmos.color = new Color(0, 0, 1, 1); }
                else if (c < 1.00f) { Gizmos.color = new Color(1, 1, 1, 1); }

                Quaternion dir = Quaternion.AngleAxis(angleCurr + offset, veh.transform.right);

                Gizmos.DrawRay(transform.position, dir * veh.transform.up);

                angleCurr += angleDiff;
            }
        }
    }
}