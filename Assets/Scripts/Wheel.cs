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

        public Transform transform;

        public GameObject entity;

        public void Init(Vehicle parent)
        {
            owner = parent;
            //entity = GameObject.Instantiate(owner.wheelPrefab, transform.localPosition, owner.wheelPrefab.transform.rotation);
            //entity.transform.parent = owner.transform;

            transform.position = owner.transform.TransformPoint(transform.localPosition);
        }

        public void FixedUpdate()
        {
            CastRays(135, 32, 0);
        }

        public void Update()
        {
            entity.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + spring.offset, transform.localPosition.z);
        }

        private void CastRays(float arc, uint iterations, float radius)
        {
            arc = Mathf.Clamp(arc, 0, 180);

            float angle = (arc / iterations);
            float currentAngle = (angle / 2);

            var offset = (angle * (iterations / 2f)) + (180 - arc);

            RaycastHit hit = default;
            bool rayhit = false;

            for(uint i = 0; i < iterations; i++)
            {
                Vector3 rot = transform.rotation.eulerAngles;
                Quaternion dir = owner.transform.rotation * Quaternion.Euler(currentAngle + offset, rot.y, -rot.z);

                if(Physics.Raycast(transform.position, dir * owner.transform.up, out hit, owner.wheelRadius + 0.1f))
                {
                    rayhit = true;

                    Vector3 velocity = owner.body.GetPointVelocity(transform.position);

                    spring.offset = spring.restLength - hit.distance;

                    spring.force = (spring.offset * spring.strength) - (velocity.y * spring.damping);

                    owner.body.AddForceAtPosition(spring.force * owner.transform.up, transform.position);

                    //Debug.Log($"Ratio: {spring.damping / Mathf.Sqrt(spring.strength * owner.body.mass)}");
                }

                currentAngle += angle;
            }

            if(rayhit)
            {
                spring.offset = spring.restLength - hit.distance;
            }
            else
            {
                spring.offset = spring.maxRange - owner.wheelRadius;
            }
        }

        public void UpdateValues()
        {
            spring.Range = owner.spring.Range;
            spring.damping = owner.spring.damping;
            spring.restLength = owner.spring.restLength;
            spring.strength = owner.spring.strength;
        }
    }
}