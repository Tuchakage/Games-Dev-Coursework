using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderEnemyAi : MonoBehaviour
{
    NavMeshAgent na;
    Animator eanim;

    GameObject player;
    Vector3 wandertarget;
    public float playerdist;
    float wanderSpeed = 0.5f;
    float chaseSpeed = 10.0f;
    int wanderRange = 10;
    bool startwander = false; //This Variable will get the wander function working after the enemy loses the chase against the player
    // Start is called before the first frame update
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        eanim = GetComponent<Animator>();
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        playerdist = Vector3.Distance(player.transform.position, transform.position);

        if (playerdist < 7)
        {
            Chasing();

            //This is reset when chasing so that when the enemy loses the player then the wander function will be called once again
            startwander = false;
        }
        else if (playerdist > 10 && !startwander) 
        {
            startwander = true;
            Wander();
        }
        else
        {
            //Gets the length between the Enemy and the wander target and if it is less than 5 then the Wander Function will be called again
            if ((transform.position - wandertarget).magnitude < 6)
            {
                Wander();
            }
        }
    }

    void Chasing() 
    {
        na.SetDestination(player.transform.position);
        eanim.SetBool("isRunning", true);
        na.isStopped = false;
        na.speed = chaseSpeed;
    }

    void Wander() 
    {
        eanim.SetBool("isRunning", false);
        //This is the wander target that will randomly move around everytime this function is called
        wandertarget = new Vector3(Random.Range(transform.position.x - wanderRange, transform.position.x + wanderRange), 1, Random.Range(transform.position.z - wanderRange, transform.position.z + wanderRange));
        
        //Makes it so the Enemy will follow this Wander Target
        na.SetDestination(wandertarget);
        na.speed = wanderSpeed;
        //Debug.Log(gameObject.name+" "+wandertarget + " and " + (transform.position - wandertarget).magnitude);       
    }
}
