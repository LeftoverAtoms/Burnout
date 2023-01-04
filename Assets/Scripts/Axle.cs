using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public class Axle
    {
        //[HideInInspector]
        public Transform transform;

        private Wheel leftWheel;
        private Wheel rightWheel;

        public float antiRollStrength = 1;

        // Stopping Point: Does the new keyword not work in constructors? Same with Debug.Logs?
        public Axle(Vehicle parent, float wheel_offset)
        {
            transform.parent = parent.transform;

            leftWheel = new Wheel(this);
            rightWheel = new Wheel(this);

            leftWheel.transform.localPosition = transform.localPosition + (Vector3.left * wheel_offset);
            rightWheel.transform.localPosition = transform.localPosition + (Vector3.right * wheel_offset);
        }

        public void SimulateWheels()
        {
            Vector3 antiRollForce = (leftWheel.spring.offset - rightWheel.spring.offset) * antiRollStrength * Vector3.up;

            leftWheel.ApplySpringForce(antiRollForce);
            rightWheel.ApplySpringForce(antiRollForce);
        }

        public void DrawGizmos()
        {
            leftWheel?.DrawGizmos();
            rightWheel?.DrawGizmos();
        }
    }
}