using UnityEngine;

namespace StuntGame
{
    public class Wheel : MonoBehaviour
    {
        private Vehicle vehicle;
        private Spring spring;

        //public float friction;
        //public float torque;

        private float radius = 0.25f;

        private void Start()
        {
            vehicle = transform.parent.GetComponent<Vehicle>();
        }

        private void FixedUpdate()
        {
            spring = vehicle.spring;

            if(Physics.Raycast(new Ray(transform.position, -transform.up), out RaycastHit hit, spring.maxRange + radius))
            {
                spring.lastCompression = spring.compression;
                spring.compression = hit.distance - radius;

                float velocity = (spring.lastCompression - spring.compression) / Time.fixedDeltaTime;
                float force = spring.stiffness * spring.compression + spring.dampening * velocity;

                vehicle.GetComponent<Rigidbody>().AddForceAtPosition(force * -transform.up, hit.point);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            if(vehicle != null)
            {
                Gizmos.DrawRay(transform.position, -transform.up * (spring.maxRange + radius));
            }
        }
    }
}