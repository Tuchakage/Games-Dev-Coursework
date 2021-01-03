using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class ButtonHandler : MonoBehaviour
{
    EnemyHealth eh;
    TurnBasedSystem tbs;
    GameManager gm;
    PlayerStats ps;
    NavMeshAgent na;
    Animator anim;

    GameObject player;

    int playerdamage;
    bool attack = false;
    bool moveonce = false;
    public bool attackbuttonpressed = false;

    public Transform target;
    public Transform originalspot;

    public float enemydistance;
    public float playerogpos;

    private void Start()
    {
        eh = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
        tbs = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        na = GameObject.Find("Player").GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        //Get the dsitance between the player and the enemy
        enemydistance = Vector3.Distance(target.transform.position, player.transform.position);
        playerogpos = Vector3.Distance(originalspot.transform.position, player.transform.position);

        if (attackbuttonpressed == true) 
        {
            PlayerAttack();         
        }
    }
    public void attackButton() 
    {
        playerdamage = ps.stats["Attack"];
        Debug.Log("Attack Button");

        attackbuttonpressed = true;
    }

    public void escapeButton() 
    {
        Debug.Log("Escape Button");
        gm.battleend = true;
        SceneManager.LoadScene("test");
        

    }

    public void PlayerAttack() 
    {
        if (!moveonce)
        {
            na.SetDestination(target.position);
            na.isStopped = false;
            moveonce = true;
        }
        if (enemydistance < 3 && !attack) // If the Player is near the Enemy and it hasn't attacked yet
        {
            //Player will stop
            na.isStopped = true;
            //Attack Animation Of The Player Will Play
            anim.SetTrigger("attack");
            //Indicates that the Player has already attacked
            attack = true;
            //Enemy Takes Damage
            eh.LoseHealth(playerdamage);
        }
        else if (enemydistance < 3 && attack) // If the Player has finished his attack and is still near the Enemy, it will go back to its original spot
        {
            na.SetDestination(originalspot.position);
            na.isStopped = false;
        }

        else if (playerogpos < 2.1 && attack) // Once it gets back to its original position after its attack then it will stop and the destination will be set to the Enemy again if it attacks again
        {
            na.SetDestination(target.position);
            na.isStopped = true;
            attack = false;

            attackbuttonpressed = false;
            //When it gets back to position it will be the Enemies turn
            tbs.enemyturn = true;

        }
        //When the Player has already used its moveonce variable which is treated like its in the OnStart() function and is not attacking and is already at the original position 
        else if (playerogpos < 2.1 && !attack && moveonce)
        {
            //Enemy will move towards the player
            na.isStopped = false;
        }
        
    }
}
