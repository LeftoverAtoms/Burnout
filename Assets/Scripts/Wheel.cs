using UnityEngine;

namespace StuntGame
{
    [System.Serializable]
    public class Wheel
    {
        [HideInInspector]
        public string name;

        private Vehicle owner;
        private Spring spring;

        public GameObject model;
        public Vector3 localPos;

        public void Init(Vehicle parent)
        {
            owner = parent;
            model = GameObject.Instantiate(model, localPos, default);
            model.transform.parent = owner.transform;
        }

        public void FixedUpdate()
        {
            spring.Range = owner.spring.Range;
            spring.damping = owner.spring.damping;
            spring.restLength = owner.spring.restLength;
            spring.stiffness = owner.spring.stiffness;

            if (Physics.Raycast(new Ray(owner.transform.position + localPos, -owner.transform.up), out RaycastHit hit, owner.spring.maxRange + owner.wheelRadius))
            {
                float prevLength = spring.length;

                spring.length = hit.distance - owner.wheelRadius;
                spring.length = Mathf.Clamp(spring.length, owner.spring.minRange, owner.spring.maxRange);

                float velocity = (prevLength - spring.length) / Time.fixedDeltaTime;
                float force = owner.spring.stiffness * (owner.spring.restLength - spring.length) + owner.spring.damping * velocity;

                owner.body.AddForceAtPosition(force * owner.transform.up, hit.point);

                //Debug.Log($"Name: {name} || Force: {force} || Velocity: {velocity} || Length: {spring.length}");
            }
            else
            {
                spring.length = owner.spring.maxRange;
            }

            model.transform.localPosition = new Vector3(localPos.x, localPos.y - spring.length, localPos.z);
        }
    }
}