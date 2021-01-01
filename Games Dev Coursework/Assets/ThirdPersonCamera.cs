using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public bool lockCursor = true;
    //Camera Rotation Of The X Axis Is called Pitch And Y Axis is Yaw
    float pitch;
    float yaw;
    public Vector2 pitchMinMax = new Vector2(-3, 60);

    public Transform target;
    public float dstfromTarget = 2.0f;

    public float rotationSmoothTime = 0.12f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    int currentscene;


    void Start() 
    {
        currentscene = SceneManager.GetActiveScene().buildIndex;
        //If not in Battle Scene then you can't use the cursor
        if (currentscene != 1) 
        {
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }

    private void Update()
    {
        //Checks to see if the current scene is scene 1 (Battle Scene) Where it will allow you to use the cursor
        if (currentscene == 1) 
        {
            if (lockCursor) 
            {
                lockCursor = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    // Update is called once per frame after all the other Update Methods
    void LateUpdate()
    {
        if (currentscene != 1)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            //Clamping it so Camera cant do a full rotation
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);



            //Smoothing Camera Rotation
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

            transform.eulerAngles = currentRotation;
        }


        //Makes the Camera look at the Target
        transform.position = target.position - transform.forward * dstfromTarget;
    }
}
