using UnityEditor;
using UnityEngine;

//simple camera control for the sake of just looking around
public class CameraControl : MonoBehaviour
{
    public float lookSpeedH = 2f;
    public float lookSpeedV = 2f;
    public float moveSpeed = 2f;
    public float dragSpeed = 6f;
     
    private float yaw = 0f;
    private float pitch = 0f;
         
    void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += lookSpeedH * Input.GetAxis("Mouse X");
            pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }

        if (Input.GetMouseButton(2))
        {
            transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed,
                -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed,
                0);
        }

        transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed, Space.Self);
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed, 0, 0, Space.Self);
    }
}
