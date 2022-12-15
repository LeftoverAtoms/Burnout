using UnityEngine;
using System;

namespace Burnout
{
    public class Vehicle : MonoBehaviour
    {
        public WheelCollider[] wheels = new WheelCollider[4];

        public Rigidbody body;
        private Vector3 input;

        private void FixedUpdate()
        {
            for(int i = 0; i < wheels.Length; i++)
            {
                if(i <= 1)
                {
                    wheels[i].steerAngle += input.z;
                    wheels[i].steerAngle = Mathf.Clamp(wheels[i].steerAngle, -25, 25);

                    if(input.z == 0) { wheels[i].steerAngle = 0; }
                }
                else if(i <= 3)
                {
                    wheels[i].motorTorque += input.x * 10;

                    if(input.x == 0) { wheels[i].motorTorque = 0; }
                }

                if(input.y > 0)
                {
                    wheels[i].brakeTorque = 50;
                }
                else
                {
                    wheels[i].brakeTorque = 0;
                }
            }
        }

        private void Update()
        {
            input = new Vector3(Input.GetAxis("Vertical"), Convert.ToInt32(Input.GetKey(KeyCode.Space)), Input.GetAxis("Horizontal"));
        }
    }
}