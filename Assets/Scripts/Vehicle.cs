using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.UIElements;

namespace Stunt
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
            //body.AddForce(100000f * transform.forward, ForceMode.Impulse);

            foreach(var obj in suspension) obj.Init(this);
            suspension[0].name = "Front Left";
            suspension[1].name = "Front Right";
            suspension[2].name = "Back Left";
            suspension[3].name = "Back Right";
        }

        private void FixedUpdate()
        {
            // Gravity
            body.velocity = new Vector3(body.velocity.x, Mathf.Clamp(body.velocity.y, -54f, 54f), body.velocity.z);

            foreach(var obj in suspension) obj.FixedUpdate();
        }

        private void Update()
        {
            foreach(var obj in suspension) obj.Update();

            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        private void OnValidate()
        {
            foreach(var obj in suspension) obj.UpdateValues();
        }

        private void OnDrawGizmos()
        {
            foreach(var obj in suspension)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(obj.position + new Vector3(0f, spring.maxRange + wheelRadius, 0f), 0.1f);
                Gizmos.DrawWireSphere(obj.position - new Vector3(0f, spring.minRange + wheelRadius, 0f), 0.1f);

                Gizmos.color = Color.white;
                Gizmos.DrawLine(obj.position + new Vector3(0f, spring.maxRange, 0f), obj.position - new Vector3(0f, spring.minRange, 0f));

                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(obj.position + new Vector3(0f, obj.spring.offset, 0f), 0.1f);

                Gizmos.color = Color.magenta;

                int amt = 8;
                float angle = 180f / (amt - 1);
                float curAngle = 0;
                for(int i = 0; i < amt; i++)
                {
                    Vector3 end = Quaternion.Euler(new Vector3(curAngle, 0, 0)) * new Vector3(obj.position.x, obj.position.y - wheelRadius, obj.position.z);
                    Gizmos.DrawLine(obj.position, end);
                    curAngle += angle;
                }
            }
        }
    }
}