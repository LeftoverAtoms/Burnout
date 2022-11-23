using System;
using UnityEngine;

namespace StuntGame
{
    [Serializable]
    public class Wheel
    {
        [HideInInspector]
        public string name;

        private Vehicle owner;
        private Spring spring;

        public GameObject model;
        public Vector3 localPosition;

        public void Init(Vehicle parent)
        {
            owner = parent;
            model = GameObject.Instantiate(model, localPosition, model.transform.rotation);
            model.transform.parent = owner.transform;
            model.AddComponent<SphereCollider>().radius = owner.wheelRadius * 0.5f;
        }

        public void FixedUpdate()
        {
            // Debug refresh variables from vehicle.
            spring.Range = owner.spring.Range;
            spring.damping = owner.spring.damping;
            spring.restLength = owner.spring.restLength;
            spring.stiffness = owner.spring.stiffness;

            // Counteract force if tire if fully compressed.
            if (Physics.Raycast(new Ray(owner.transform.TransformPoint(localPosition), -owner.transform.up), out RaycastHit hit, owner.spring.maxRange + owner.wheelRadius))
            {
                if(hit.transform.IsChildOf(owner.transform))
                    return;

                var prevLength = spring.length;

                spring.length = hit.distance - owner.wheelRadius;
                spring.length = Mathf.Clamp(spring.length, owner.spring.minRange, owner.spring.maxRange);

                float velocity = (prevLength - spring.length) / Time.fixedDeltaTime;
                float force = owner.spring.stiffness * (owner.spring.restLength - spring.length) + owner.spring.damping * velocity;

                owner.body.AddForceAtPosition(force * owner.transform.up, hit.point);

                //Debug.Log($"Name: {name} || Force: {force} || Velocity: {velocity} || Length: {spring.length}");
                Debug.Log($"Ratio: {spring.damping / Mathf.Sqrt(spring.stiffness * owner.body.mass)}");
            }
            else
            {
                spring.length = owner.spring.maxRange;
            }
        }

        public void Update()
        {
            model.transform.localPosition = new Vector3(localPosition.x, localPosition.y - spring.length, localPosition.z);
        }
    }
}