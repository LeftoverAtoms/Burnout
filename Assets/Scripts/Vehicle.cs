using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public GameObject[] wheels;

    private Vector3 input;

    private Vector3 acceleration;
    private Vector3 friction;
    public Vector3 Velocity;

    private const float airResistence = 1;

    private float torque;

    private void FixedUpdate()
    {
        Vector3 v = Velocity;

        Vector3 Ftraction = torque * transform.forward;

        friction = -airResistence * v * v.magnitude;

        float speed = Mathf.Sqrt(v.x * v.x + v.y * v.y);

        //fdrag.x = -Cdrag * v.x * speed;
        //fdrag.y = -Cdrag * v.y * speed;

        transform.Translate(acceleration * Time.fixedDeltaTime);
    }

    private void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}