using System;
using UnityEngine;

namespace Stunt
{
    [Serializable]
    public class Suspension
    {
        [HideInInspector] public string name;

        [HideInInspector] public Vehicle owner;
        [HideInInspector] public Spring spring;

        public Vector3 localPosition;
        public Vector3 position => owner.transform.TransformPoint(localPosition);

        public GameObject entity;

        public void Init(Vehicle parent)
        {
            owner = parent;
            entity = GameObject.Instantiate(owner.wheelPrefab, localPosition, owner.wheelPrefab.transform.rotation);
            entity.transform.parent = owner.transform;
        }

        public void FixedUpdate()
        {
            int amt = 32;
            float angle = 180f / (amt - 1);
            float curAngle = 0;
            for(int i = 0; i < amt; i++)
            {
                if(Physics.Raycast(position, new Vector3(curAngle, 0, 0), out RaycastHit hit, owner.wheelRadius + 0.1f))
                {
                    Vector3 velocity = owner.body.GetPointVelocity(position);

                    spring.offset = spring.restLength - hit.distance;

                    spring.force = (spring.offset * spring.strength) - (velocity.y * spring.damping);

                    owner.body.AddForceAtPosition(spring.force * owner.transform.up, position);

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
            entity.transform.localPosition = new Vector3(localPosition.x, localPosition.y + spring.offset, localPosition.z);
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