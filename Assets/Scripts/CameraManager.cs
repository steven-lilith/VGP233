using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   
    public GameObject player;
    public float mouseSensitivity=1000.0f;
    public float xRotation = 0.0f;
    public float maxXRotation = 90.0f;
    public float minXRotation = -90.0f;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= moveY;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        player.transform.Rotate(Vector3.up * moveX);
        transform.position = player.transform.position + offset;
    }
}
