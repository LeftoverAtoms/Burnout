using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public class Axle : Object
    {
        public Vehicle Vehicle;

        private Wheel LeftWheel;
        private Wheel RightWheel;

        public float AntiRollStrength = 1;

        public Axle(Vector3 axle_offset, float wheel_offset, Vehicle parent)
        {
            LocalPosition = axle_offset;

            Vehicle = parent;

            LeftWheel = new Wheel(parent);
            RightWheel = new Wheel(parent);

            LeftWheel.LocalPosition = axle_offset + (Vector3.left * wheel_offset);
            RightWheel.LocalPosition = axle_offset + (Vector3.right * wheel_offset);
        }

        public void SimulateWheels()
        {
            // Convert LocalPosition to worldspace. [Cannot use Transform in classes without inheriting MonoBehaviour]
            LeftWheel.Position = Vehicle.transform.rotation * LeftWheel.LocalPosition + Vehicle.transform.position;
            RightWheel.Position = Vehicle.transform.rotation * RightWheel.LocalPosition + Vehicle.transform.position;
            Position = Vehicle.transform.rotation * LocalPosition + Vehicle.transform.position;

            Vector3 anti_roll_force = (LeftWheel.Spring.offset - RightWheel.Spring.offset) * AntiRollStrength * Vehicle.transform.up;

            Debug.Log(anti_roll_force);

            LeftWheel.ApplySpringForce(anti_roll_force);
            RightWheel.ApplySpringForce(anti_roll_force);
        }

        public void DrawGizmos()
        {
            LeftWheel?.DrawGizmos();
            RightWheel?.DrawGizmos();
        }
    }
}