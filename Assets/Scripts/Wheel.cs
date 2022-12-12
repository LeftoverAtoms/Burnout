using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public class Wheel
    {
        [HideInInspector] public string name;

        [HideInInspector] public Vehicle owner;
        [HideInInspector] public Spring spring;
        public bool isSteerable;

        //private GameObject entity;
        public Transform transform;

        public Vector3 wishVelocity = Vector3.zero;

        public void Init(Vehicle parent)
        {
            transform.position = parent.transform.TransformPoint(transform.localPosition);
            owner = parent;

            UpdateValues();

            //entity = GameObject.Instantiate(owner.wheelPrefab, transform.localPosition, owner.wheelPrefab.transform.rotation);
            //entity.transform.parent = owner.transform;
        }

        public void FixedUpdate()
        {
            Vector3 velocity = CastRays(135, 10);

            owner.body.AddRelativeForce(wishVelocity + velocity, ForceMode.Acceleration);
        }

        public void Update()
        {
        }

        // NOTE: These variables should only be calculated once! NOT EVERY PHYSICS FRAME!
        private Vector3 CastRays(float degrees, float iterations)
        {
            Vector3 velocity = owner.body.GetPointVelocity(transform.position);

            degrees = Mathf.Clamp(degrees, 0, 360); // Arcs/Circles are limited to 0-360 degrees.

            float angleDiff = (degrees / iterations); // Degrees between each raycast.
            float angleCurr = (angleDiff / 2);

            float rotationOffset = (angleDiff * (iterations / 2)) + (180 - degrees); // MAGIC!

            float avgForce = 0;
            int contacts = 0;

            for (uint i = 0; i < iterations; i++)
            {
                Quaternion dir = Quaternion.AngleAxis(angleCurr + rotationOffset, transform.right); // Pure hatred for Quaternions.

                if (Physics.Raycast(transform.position, dir * owner.transform.up, out RaycastHit hit, owner.wheelRadius + 0.1f))
                {
                    spring.offset = spring.restLength - hit.distance;

                    avgForce += (spring.offset * spring.strength) - (velocity.y * spring.damping);
                    contacts += 1;
                }

                angleCurr += angleDiff;
            }

            return new Vector3(-velocity.x, avgForce / contacts, -velocity.z);
        }

        public void UpdateValues()
        {
            name = transform.name;

            if (owner != null)
            {
                spring.Range = owner.spring.Range;
                spring.damping = owner.spring.damping;
                spring.restLength = owner.spring.restLength;
                spring.strength = owner.spring.strength;
            }
        }
    }
}