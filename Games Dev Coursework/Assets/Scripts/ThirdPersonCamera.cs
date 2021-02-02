using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonCamera : MonoBehaviour
{
    ButtonHandler bh;
    Menus menu;
    public float mouseSensitivity = 10;
    public bool lockCursor = false;
    //Camera Rotation Of The X Axis Is called Pitch And Y Axis is Yaw
    float pitch;
    float yaw;
    public Vector2 pitchMinMax = new Vector2(-3, 60);

    public Transform target;
    public Transform attackcam;
    public float dstfromTarget = 2.0f;
    public float rotationspeed = 1.0f;
    public bool backtopos = true; //Is The Camera in its orginal spot?

    public float rotationSmoothTime = 0.12f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    string currentscene;


    void Start() 
    {
        currentscene = SceneManager.GetActiveScene().name;
        //If not in Battle Scene then you will look for the Pause Script otherwise look for Button Handler Script
        if (currentscene != "battle test" && currentscene != "finalbattle")
        {
            menu = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Menus>();
        }
        else 
        {
            bh = GameObject.Find("ButtonHandler").GetComponent<ButtonHandler>();
        }
        
    }

    private void Update()
    {
        //Checks to see if the current scene is scene 1 (Battle Scene) Where it will allow you to use the cursor Or if you are not in the battle scene and ispaused variable is true
        if ((currentscene == "battle test" || currentscene == "finalbattle") || (currentscene != "battle test" && currentscene != "finalbattle" && menu.isPaused))
        {
            //Cursor Is Enabled
            if (lockCursor)
            {
                lockCursor = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //Debug.Log("Cursor enabled");
            }
            
        }
        else if ((currentscene != "battle test" && currentscene != "finalbattle" && !menu.isPaused)) //If not in Battle Scene and the game isnt paused
        {
            //Disable the Cursor
            if (!lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                lockCursor = true;
                //Debug.Log("Cursor Disabled");
            }
            
        }
    }
    // Update is called once per frame after all the other Update Methods
    void LateUpdate()
    {
        if (currentscene != "battle test" && currentscene != "finalbattle")
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            //Clamping it so Camera cant do a full rotation
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            //Smoothing Camera Rotation
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

            transform.eulerAngles = currentRotation;

            //Makes the Camera look at the Target
            transform.position = target.position - transform.forward * dstfromTarget;
        }
        else if ((currentscene == "battle test" || currentscene == "finalbattle") && !bh.attackbuttonpressed) //When the player hasn't pressed anything then the camera will just rotate
        {
            if (!backtopos) 
            {
                this.transform.position = new Vector3(15, 7, 31);
                backtopos = true;
            }
            
            this.transform.LookAt(target);
            transform.Translate(Vector3.right * Time.deltaTime * rotationspeed);
        }
        else if ((currentscene == "battle test" || currentscene == "finalbattle") && bh.attackbuttonpressed) //Camera will focus on the player once the attack button is pressed
        {
            this.transform.position = new Vector3(12.1f, 4.61f, 31.79f);
            this.transform.eulerAngles = new Vector3(24, 0, 0);
            transform.position = attackcam.position - transform.forward * dstfromTarget;
        }
    }
}
