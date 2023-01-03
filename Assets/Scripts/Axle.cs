using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public class Axle
    {
        public Wheel leftWheel;
        public Wheel rightWheel;

        public float antiRollStrength = 1;

        public void FixedUpdate()
        {
            Vector3 antiRollForce = Vector3.up * ((leftWheel.spring.offset - rightWheel.spring.offset) * antiRollStrength);

            leftWheel.ApplyForce(leftWheel.wishVelocity + antiRollForce);
            rightWheel.ApplyForce(rightWheel.wishVelocity + antiRollForce);
        }
    }
}