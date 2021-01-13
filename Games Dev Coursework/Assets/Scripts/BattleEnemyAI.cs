using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BattleEnemyAI : MonoBehaviour
{

    GameManager gm;
    NavMeshAgent na;
    TurnBasedSystem tbs;
    EnemyStats es;
    Skills sk;
    ButtonHandler bh;

    public Transform target;
    public Transform originalspot;
    public GameObject fire;
    GameObject player;

    float enemydist;
    public float originalspotdist;
    bool attack = false;
    int firedmg;
    int attackdmg;
    public int enemysp;
    bool eskillused;
    public float eskilltimer = 5;

    //Used to make the cube move towards the player the first time when the function is called 
    public bool moveonce = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        na = GetComponent<NavMeshAgent>();
        tbs = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
        es = GetComponent<EnemyStats>();
        sk = GameObject.Find("GameManager").GetComponent<Skills>();
        enemysp = es.stats["SP"];
        player = GameObject.Find("Player");
        bh = GameObject.Find("ButtonHandler").GetComponent<ButtonHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the dsitance between the player and the enemy
        enemydist = Vector3.Distance(target.transform.position, transform.position);
        originalspotdist = Vector3.Distance(originalspot.transform.position, transform.position);

        if (eskillused)
        {
            if (eskilltimer > 0)
            {
                eskilltimer -= Time.deltaTime;
            }
            else
            {
                tbs.enemyturn = false;
                //If The Timer is at 0 then it will be the Players go
                tbs.PlayerTurn();
                eskillused = false;
                attack = false;
            }
        }
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
            attackdmg = es.stats["Attack"];
            //Player loses health
            if (!bh.block)
            {
                gm.pHealth -= attackdmg;
            }
            else 
            {
                //Decrease Attack damage by 30% if Player is blocking
                gm.pHealth -= (attackdmg*30/100);
            }
            

            //Indicates that the Enemy has already attacked
            attack = true;
            //Debug.Log("1");

        }
        else if (enemydist < 3 && attack) // If the Enemy has finished his attack and is still near the player, it will go back to its original spot
        {
            na.SetDestination(originalspot.position);
            na.isStopped = false;
            //Debug.Log("2");

        }

        else if (originalspotdist < 0.5 && attack) // Once it gets back to its original position after its attack then it will stop and the destination will be set to the player again if it attacks again
        {
            na.SetDestination(target.position);
            na.isStopped = true;
            attack = false;
            //Debug.Log("Works");

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

    public void Fire() 
    {
        if (enemysp > 0) 
        {
            if (!attack) 
            {
                firedmg = sk.skills["Fire"];
                if (!bh.block)
                {
                    gm.pHealth -= firedmg;
                }
                else 
                {
                    //Reduce Damage by 30% if Blocking
                    firedmg -= (firedmg * 30 / 100);
                    gm.pHealth -= firedmg;
                }
                
                enemysp -= 5;               
                eskillused = true;
                eskilltimer = 5;
                GameObject fireprefab = Instantiate(fire, player.transform.position, player.transform.rotation);
                Destroy(fireprefab, 5);
                attack = true;
            }        
        }
    }
}
