using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public class Suspension
    {
        [HideInInspector] public string name;

        [HideInInspector] public Vehicle owner;
        [HideInInspector] public Spring spring;

        public Transform transform;

        public GameObject entity;

        public void Init(Vehicle parent)
        {
            owner = parent;
            entity = GameObject.Instantiate(owner.wheelPrefab, transform.localPosition, owner.wheelPrefab.transform.rotation);
            entity.transform.parent = owner.transform;

            transform.position = owner.transform.TransformPoint(transform.localPosition);
        }

        public void FixedUpdate()
        {
            int amt = 32;
            float angle = 180f / (amt - 1);
            float curAngle = 0;
            for(int i = 0; i < amt; i++)
            {
                if(Physics.Raycast(transform.position, new Vector3(curAngle, 0, 0), out RaycastHit hit, owner.wheelRadius + 0.1f))
                {
                    Vector3 velocity = owner.body.GetPointVelocity(transform.position);

                    spring.offset = spring.restLength - hit.distance;

                    spring.force = (spring.offset * spring.strength) - (velocity.y * spring.damping);

                    owner.body.AddForceAtPosition(spring.force * owner.transform.up, transform.position);

                    //Debug.Log($"Name: {name} || Force: {force} || Velocity: {velocity} || Offset: {spring.offset}");
                    //Debug.Log($"Ratio: {spring.damping / Mathf.Sqrt(spring.strength * owner.body.mass)}");
                }
                else
                {
                    spring.offset = spring.maxRange - owner.wheelRadius;
                }

                curAngle += angle;
            }
        }

        public void Update()
        {
            entity.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + spring.offset, transform.localPosition.z);
        }

        private void CastRays(uint amount, float offset)
        {
            float angle = 180f / amount;
            float current = 0;

            for(uint i = 0; i <= amount; i++)
            {
                current += angle;
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