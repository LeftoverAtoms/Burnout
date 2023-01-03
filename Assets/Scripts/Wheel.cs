using System;
using UnityEngine;

namespace Burnout
{
    public class Wheel : MonoBehaviour
    {
        [HideInInspector] public Vehicle owner;
        [HideInInspector] public Spring spring;

        //private GameObject entity;

        public Vector3 wishVelocity;

        public void Init(Vehicle parent)
        {
            owner = parent;

            transform.position = owner.transform.TransformPoint(transform.localPosition);
        }

        private void FixedUpdate()
        {
            CastRays(100, 10);
        }

        public void ApplyForce(Vector3 force)
        {
            owner.body.AddForceAtPosition(owner.transform.TransformPoint(force), transform.position, ForceMode.Acceleration);
        }

        ///<summary> NOTE: These variables should only be calculated once! NOT EVERY PHYSICS FRAME! </summary>
        private void CastRays(float degrees, float iterations)
        {
            degrees = Mathf.Clamp(degrees, 0, 360); // Arcs/circles are limited to 0-360 degrees.

            float angleDiff = (degrees / iterations); // Degrees between each raycast.
            float angleCurr = (angleDiff / 2);

            float rotationOffset = (angleDiff * (iterations / 2)) + (180 - degrees);

            float avgForce = 0;
            int contacts = 0;

            for (uint i = 0; i < iterations; i++)
            {
                Quaternion dir = Quaternion.AngleAxis(angleCurr + rotationOffset, transform.right);

                if (Physics.Raycast(transform.position, dir * owner.transform.up, out RaycastHit hit, owner.wheelRadius + 0.1f))
                {
                    Vector3 velocity = owner.body.GetPointVelocity(transform.position);

                    spring.offset = spring.restLength - hit.distance;

                    avgForce += (spring.offset * spring.strength) - (velocity.y * spring.damping);
                    contacts += 1;
                }

                angleCurr += angleDiff;
            }

            if (contacts > 0) { wishVelocity.y = avgForce / contacts; }
            else { wishVelocity.y = 0; }
        }

        private void OnValidate()
        {
            if (owner == null) { return; }

            spring.range = owner.spring.range;
            spring.damping = owner.spring.damping;
            spring.restLength = owner.spring.restLength;
            spring.strength = owner.spring.strength;
        }

        private void OnDrawGizmos()
        {
            float iterations = 10;
            float degrees = 100;

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

                Quaternion dir = Quaternion.AngleAxis(angleCurr + offset, owner.transform.right);

                Gizmos.DrawRay(transform.position, dir * owner.transform.up);

                angleCurr += angleDiff;
            }
        }
    }
}