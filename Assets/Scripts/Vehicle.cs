using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public GameObject[] wheels;

    private Vector3 input;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        transform.Translate(input.z * transform.forward);
        transform.Rotate(input.x * transform.up);
    }

    private void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
}