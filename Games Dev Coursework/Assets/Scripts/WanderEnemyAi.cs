using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderEnemyAi : MonoBehaviour
{
    NavMeshAgent na;

    GameObject player;
    Vector3 wandertarget;

    public float playerdist;
    public float wandertimer = 5;
    int wanderRange = 10;
    bool startwander = false; //This Variable will get the wander function working after the enemy loses the chase against the player
    // Start is called before the first frame update
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        playerdist = Vector3.Distance(player.transform.position, transform.position);

        if (playerdist < 10)
        {
            Chasing();

            //This is reset when chasing so that when the enemy loses the player then the wander function will be called once again
            startwander = false;
        }
        else if (playerdist > 10 && !startwander) 
        {
            startwander = true;
            Wander();
            Debug.Log("Player Distance more than 10");
        }
        else
        {
            //Gets the length between the Enemy and the wander target and if it is less than 5 then the Wander Function will be called again
            if ((transform.position - wandertarget).magnitude < 5)
            {
                Wander();
            }
        }



    }

    void Chasing() 
    {
        na.SetDestination(player.transform.position);
        na.isStopped = false;
    }

    void Wander() 
    {
        //This is the wander target that will randomly move around everytime this function is called
        wandertarget = new Vector3(Random.Range(transform.position.x - wanderRange, transform.position.x + wanderRange), 1, Random.Range(transform.position.z - wanderRange, transform.position.z + wanderRange));

        //Makes it so the Enemy will follow this Wander Target
        na.SetDestination(wandertarget);
        Debug.Log(wandertarget + " and " + (transform.position - wandertarget).magnitude);       
    }
}
