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
            Vector3 srt = position + new Vector3(0f, spring.maxRange + owner.wheelRadius, 0f);
            Vector3 end = position - new Vector3(0f, spring.minRange + owner.wheelRadius, 0f);

            if(Physics.Linecast(srt, end, out RaycastHit hit))
            {
                float velocity = owner.body.GetPointVelocity(position).y;

                spring.offset = spring.restLength - hit.distance + owner.wheelRadius;

                float force = (spring.offset * spring.strength) - (velocity * spring.damping);

                owner.body.AddForceAtPosition(force * owner.transform.up, position);

                //Debug.Log($"Name: {name} || Force: {force} || Velocity: {velocity} || Offset: {spring.offset}");
                //Debug.Log($"Ratio: {spring.damping / Mathf.Sqrt(spring.strength * owner.body.mass)}");
            }
            else
            {
                spring.offset = spring.maxRange - owner.wheelRadius;
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