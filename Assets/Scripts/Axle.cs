using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public class Axle : MonoBehaviour
    {
        [HideInInspector]
        public Wheel leftWheel, rightWheel;

        public float antiRoll = 1;

        private void FixedUpdate()
        {
            // Calculate force per wheel with antiRollForce -> Apply forces.
            
            float antiRollForce = (leftWheel.spring.offset - rightWheel.spring.offset) * antiRoll;

            leftWheel.ApplyForce();
            rightWheel.ApplyForce();
        }
    }
}