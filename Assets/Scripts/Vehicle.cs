using UnityEngine;

namespace Burnout
{
    public class Vehicle : MonoBehaviour
    {
        public GameObject wheelPrefab;
        public Suspension[] suspension = new Suspension[4];

        public Rigidbody body;
        private Vector3 input;

        [SerializeField]
        public Spring spring;

        public float wheelRadius;

        private void Start()
        {
            body.AddForce(10000f * transform.forward, ForceMode.Impulse);

            foreach(var obj in suspension)
            {
                obj.Init(this);
            }
            suspension[0].name = "Front Left";
            suspension[1].name = "Front Right";
            suspension[2].name = "Back Left";
            suspension[3].name = "Back Right";
        }

        private void FixedUpdate()
        {
            // Gravity
            body.velocity = new Vector3(body.velocity.x, Mathf.Clamp(body.velocity.y, -54f, 54f), body.velocity.z);

            foreach(var obj in suspension)
            {
                obj.FixedUpdate();
            }
        }

        private void Update()
        {
            foreach(var obj in suspension)
            {
                obj.Update();
            }

            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        private void OnValidate()
        {
            foreach(var obj in suspension)
            {
                obj.UpdateValues();
            }
        }

        private void OnDrawGizmos()
        {
            foreach(var obj in suspension)
            {
                //Gizmos.color = Color.red;
                //Gizmos.DrawWireSphere(obj.transform.position + new Vector3(0f, spring.maxRange + wheelRadius, 0f), 0.1f);
                //Gizmos.DrawWireSphere(obj.transform.position - new Vector3(0f, spring.minRange + wheelRadius, 0f), 0.1f);

                //Gizmos.color = Color.white;
                //Gizmos.DrawLine(obj.transform.position + new Vector3(0f, spring.maxRange, 0f), obj.transform.position - new Vector3(0f, spring.minRange, 0f));

                //Gizmos.color = Color.magenta;
                //Gizmos.DrawSphere(transform.up, 0.1f);
                //Gizmos.color = Color.blue;
                //Gizmos.DrawSphere(transform.position, 0.1f);

                uint amount = 45;
                float sum = 135;

                sum = Mathf.Clamp(sum, 0, 360); // Bad things...

                float angle = (sum / amount);
                float current = (angle / 2);

                var offset = (angle * (amount / 2f)) + (180 - sum);
                for(uint i = 0; i < amount; i++)
                {
                    var c = (float)i / amount;
                    switch(c)
                    {
                        case < 0.25f:
                        Gizmos.color = new Color(1, 0, 0, 1);
                        break;
                        case < 0.50f:
                        Gizmos.color = new Color(0, 1, 0, 1);
                        break;
                        case < 0.75f:
                        Gizmos.color = new Color(0, 0, 1, 1);
                        break;
                        case <= 1.00f:
                        Gizmos.color = new Color(1, 1, 1, 1);
                        break;
                    }

                    Vector3 rot = transform.rotation.eulerAngles;
                    Quaternion dir = Quaternion.Euler(current + offset, rot.y, -rot.z);

                    Gizmos.DrawRay(obj.transform.position, dir * transform.up);

                    current += angle;
                }
            }
        }
    }
}