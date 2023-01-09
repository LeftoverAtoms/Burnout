using UnityEngine;

namespace Burnout
{
    public class Wheel : Object
    {
        public Vehicle Veh;
        public Spring Spring;

        public Wheel(Vehicle veh)
        {
            Veh = veh;
        }

        public void ApplySpringForce(Vector3 additional = default)
        {
            additional += GetSpringForce();
            Veh.Body.AddForceAtPosition(Veh.transform.TransformVector(additional), Position, ForceMode.Acceleration);
        }

        RaycastHit Hit;
        bool IsGrounded;

        public Vector3 GetSpringForce()
        {
            RaycastHit[] contacts = Physics.SphereCastAll(Position, 0, Vector3.down, Veh.WheelRadius); // Radius and MaxDistance are misleading.
            IsGrounded = false;

            // Check for the bottom-most contact and that is Spring.Offset -> Using contact.normal convert to direction vector3 then calculate force.
            float distance = float.MaxValue;
            for(int i = 0; i < contacts.Length; i++)
            {
                if (contacts[i].distance < distance)
                {
                    distance = contacts[i].distance;

                    Hit = contacts[i];
                    IsGrounded = true;
                }
            }

            if (IsGrounded)
            {
                Vector3 velocity = Veh.Body.GetPointVelocity(Position);

                Spring.offset = Veh.Spring.restLength - Hit.distance;

                float force = (Spring.offset * Veh.Spring.strength) - (velocity.y * Veh.Spring.damping);
                return new Vector3(0, force, 0);
            }

            return Vector3.zero;
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(Position, Veh.WheelRadius);

            if (IsGrounded)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(Position, Veh.transform.up - Hit.normal * 10);
            }
        }
    }
}