using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{

    GameManager gm;
    NavMeshAgent na;
    TurnBasedSystem tbs;

    public Transform target;
    public Transform originalspot;

    float enemydist;
    public float originalspotdist;
    bool attack = false;

    //Used to make the cube move towards the player the first time when the function is called 
    public bool moveonce = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        na = GetComponent<NavMeshAgent>();
        tbs = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the dsitance between the player and the enemy
        enemydist = Vector3.Distance(target.transform.position, transform.position);
        originalspotdist = Vector3.Distance(originalspot.transform.position, transform.position);
    }

    public void EnemyAttack()
    {
        if (!moveonce) 
        {
            na.SetDestination(target.position);
            na.isStopped = false;
            moveonce = true;
        }
        if (enemydist < 3 && !attack) // If the Enemy is near the player and it hasn't attacked yet
        {
            //Enemy will stop
            na.isStopped = true;

            //Player loses health
            gm.phealth -= 10;

            //Indicates that the Enemy has already attacked
            attack = true;
            Debug.Log("1");

        }
        else if (enemydist < 3 && attack) // If the Enemy has finished his attack and is still near the player, it will go back to its original spot
        {
            na.SetDestination(originalspot.position);
            na.isStopped = false;
            Debug.Log("2");

        }

        else if (originalspotdist < 0.5 && attack) // Once it gets back to its original position after its attack then it will stop and the destination will be set to the player again if it attacks again
        {
            na.SetDestination(target.position);
            na.isStopped = true;
            attack = false;
            Debug.Log("Works");

            //Used to indicate the end of the enemy turn
            tbs.enemyturn = false;

            //After The Enemy Turn it will be the Players Turn 
            tbs.PlayerTurn();
        }
        //When the enemy has already used its moveonce variable which is treated like its in the OnStart() function and is not attacking and is already at the original position 
        else if (originalspotdist < 0.5 && !attack && moveonce) 
        {
            //Enemy will move towards the player
            na.isStopped = false;
        }

    }
}
