using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Menus menu;
    Animator anim;
    private CharacterController controller;
    public Transform camera;
    private Vector3 playerVelocity;
    public float speed = 1.0f;

    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;
    float gravity = -9.2f;
    string currentscene;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        currentscene = SceneManager.GetActiveScene().name;
        menu = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Menus>();
    }

    // Update is called once per frame
    void Update()
    {
        //If in Battle Scene then you can't move
        if (currentscene != "battle test") 
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

        //If You press the right mouse button down and the game isnt paused then the attack animation will play
        if (Input.GetButtonDown("Fire1") && !menu.isPaused)
        {
            if (currentscene != "battle test")
            {               
                anim.SetTrigger("attack");
                Debug.Log("Attack");
            }
            
        }
        //Gravity
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
}
