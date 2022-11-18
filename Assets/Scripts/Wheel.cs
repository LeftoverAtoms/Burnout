using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float friction;
    public float torque;

    private void FixedUpdate()
    {
        // Calculate force per wheel.
        if (Physics.Raycast(new Ray(transform.position, -transform.up), out RaycastHit hit, 10f))
        {
            friction = 1;
        }
    }
}