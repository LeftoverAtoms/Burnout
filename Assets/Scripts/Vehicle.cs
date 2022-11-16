using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public GameObject[] wheels;

    public Vector2 input;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        wheels[0].transform.rotation = new Quaternion(wheels[0].transform.rotation.x, input.y, input.x, 1f);
        wheels[1].transform.rotation = new Quaternion(input.y, wheels[1].transform.rotation.x, input.x, 1f);
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));
    }
}

public class Wheel
{
    private void Initialize()
    {
    }

    private void Update()
    {
    }
}