﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    private CharacterController controller;
    public Transform camera;

    public float speed = 1.0f;

    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;

    int currentscene;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        currentscene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene: "+currentscene);
    }

    // Update is called once per frame
    void Update()
    {
        //If in Battle Scene then you can't move
        if (currentscene != 1) 
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            //Check if we are moving in any direction
            //.magnitude checks the length of our direction vector
            if (direction.magnitude >= 0.1)
            {
                anim.SetBool("running", true);
                //Getting the angle we want to use
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

                // SmoothDampAngle(Current Angle, Target Angle, Reference to a variable that holds our current smooth velocity, Turn Smooth Time)
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                //Setting our Rotation
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //Allows us to move in the direction of the Camera
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else 
            {
                anim.SetBool("running", false);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentscene != 1)
            {
                anim.SetTrigger("attack");
                Debug.Log("Attack");
            }
            
        }


    }
}
