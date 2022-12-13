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

        public Vector3 wishVelocity;

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
            Vector3 velocity = owner.body.GetPointVelocity(transform.position);

            Vector3 springForce = CalculateSpringForce(135, 10, velocity);

            Debug.Log(springForce);

            owner.body.AddRelativeForce(wishVelocity + springForce, ForceMode.Acceleration);
        }

        public void Update()
        {
        }

        // NOTE: These variables should only be calculated once! NOT EVERY PHYSICS FRAME!
        private Vector3 CalculateSpringForce(float degrees, float iterations, Vector3 velocity)
        {
            degrees = Mathf.Clamp(degrees, 0, 360); // Arcs/Circles are limited to 0-360 degrees.

            float angleDiff = (degrees / iterations); // Degrees between each raycast.
            float angleCurr = (angleDiff / 2);

            float rotationOffset = (angleDiff * (iterations / 2)) + (180 - degrees);

            Quaternion dirCompression = default;
            float mostCompression = default;

            for (uint i = 0; i < iterations; i++)
            {
                Quaternion dir = Quaternion.AngleAxis(angleCurr + rotationOffset, transform.right);

                if (Physics.Raycast(transform.position, dir * owner.transform.up, out RaycastHit hit, (owner.wheelRadius * 2) + 0.1f))
                {
                    float compression = spring.restLength - hit.distance;
                    if (compression < mostCompression)
                    {
                        mostCompression = compression;
                        dirCompression = dir;
                    }
                }

                angleCurr += angleDiff;
            }

            spring.offset = mostCompression;

            var force = (spring.offset * spring.strength) - (velocity.y * spring.damping);
            return (dirCompression * owner.transform.up) * force;
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