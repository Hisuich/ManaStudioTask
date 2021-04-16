using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState
{ 
    Free,
    Moving,
    Rotating
}


public class CameraController : MonoBehaviour
{
    private CameraState state;

    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private float cameraSpeed;

    [SerializeField]
    private float cameraRotateSpeed;

    [SerializeField]
    private float zoomSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Zoom();
    }

    private void Move()
    {

        float y = transform.eulerAngles.y * Mathf.Deg2Rad;
        Vector3 input = new Vector3(0, 0, 0);
        float x = Mathf.Sin(y);
        float z = Mathf.Cos(y);
        if (Input.GetButton("Camera Down"))
        {
            input += new Vector3(-cameraSpeed*x, 0, -cameraSpeed*z);
            //z = -cameraSpeed;
        }
        if (Input.GetButton("Camera Up"))
        {
            input += new Vector3(cameraSpeed*x, 0, cameraSpeed * z);
        }
        if (Input.GetButton("Camera Right"))
        {
            input += new Vector3(cameraSpeed * z, 0, -cameraSpeed*x);
        }
        if (Input.GetButton("Camera Left"))
        {
            input += new Vector3(-cameraSpeed * z, 0, cameraSpeed*x);
        }
        input *= Time.deltaTime;

        //Debug.Log("x = " + x + " y = " + y);
        //Debug.Log(new Vector3(x * Mathf.Sin(y), 0, z * Mathf.Cos(y)));
        transform.Translate(input, Space.World);
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X") * cameraRotateSpeed * Time.deltaTime;
            //float y = -Input.GetAxis("Mouse Y") * cameraRotateSpeed * Time.deltaTime;
            //Quaternion rotation = Quaternion.Euler(y, x, 0);
            Camera.main.transform.Rotate(new Vector3(0,x,0), Space.World);
            //Camera.main.transform.position = Camera.main.transform.position + new Vector3(x, 0, y);
        }
    }

    private void Zoom()
    {
        float y = 0;
        y -= (Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime);

        if (transform.position.y < minDistance && y < 0) return;
        if (transform.position.y > maxDistance && y > 0) return;
        //y = Mathf.Clamp(y, minDistance, maxDistance);
        transform.Translate(new Vector3(0, y, 0), Space.World);
    
    }
    
}
