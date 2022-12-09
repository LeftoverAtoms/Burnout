using System;
using UnityEngine;
using System.Collections.Generic;

namespace Burnout
{
    [Serializable]
    public class Wheel
    {
        [HideInInspector] public string name;

        [HideInInspector] public Vehicle owner;
        [HideInInspector] public Spring spring;

        //private GameObject entity;
        public Transform transform;

        private Vector3 wishVelocity = Vector3.zero;

        public void Init(Vehicle parent)
        {
            owner = parent;

            transform.position = owner.transform.TransformPoint(transform.localPosition);

            UpdateValues();

            //entity = GameObject.Instantiate(owner.wheelPrefab, transform.localPosition, owner.wheelPrefab.transform.rotation);
            //entity.transform.parent = owner.transform;
        }

        public void FixedUpdate()
        {
            CastRays(100, 10);

            owner.body.AddForceAtPosition(wishVelocity, transform.position, ForceMode.Acceleration);
        }

        public void Update()
        {
        }

        // NOTE: These variables should only be calculated once! NOT EVERY PHYSICS FRAME!
        private void CastRays(float degrees, float iterations)
        {
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
                    Vector3 velocity = owner.body.GetPointVelocity(transform.position);

                    spring.offset = spring.restLength - hit.distance;

                    avgForce += (spring.offset * spring.strength) - (velocity.y * spring.damping);
                    contacts += 1;
                }

                angleCurr += angleDiff;
            }

            if (contacts > 0) { wishVelocity.y = avgForce / contacts; }
            else { wishVelocity.y = 0; }

            Debug.Log(avgForce);

            //entity.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + spring.offset, transform.localPosition.z);
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