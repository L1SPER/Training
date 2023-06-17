using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float xRotationSpeed;
    [SerializeField] float yRotationSpeed;
    [SerializeField] float distance;

    private float horizontalAngle;
    private float verticalAngle;
    public Transform playerBody;
    float verticalInput;
    float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Mouse X")*xRotationSpeed*Time.deltaTime;
        verticalInput = Input.GetAxis("Mouse Y") * yRotationSpeed * Time.deltaTime;

        playerBody.Rotate(Vector3.up*horizontalInput);

        verticalAngle -= verticalInput;
        verticalAngle = Mathf.Clamp(verticalAngle, 30f, 89f);

        horizontalAngle += horizontalInput;
        transform.position=playerBody.position+Quaternion.Euler(verticalAngle,horizontalAngle,0f)*new Vector3(0f,0f,-distance);

        transform.LookAt(playerBody);
    }
}
