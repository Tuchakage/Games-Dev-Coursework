using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class BattleEnemyAI : MonoBehaviour
{

    GameManager gm;
    NavMeshAgent na;
    TurnBasedSystem tbs;
    EnemyStats es;
    Skills sk;
    ButtonHandler bh;
    public Animator eanim;

    Transform target;
    Transform originalspot;
    public GameObject fire;
    GameObject player;
    GameObject enemyhitbox;

    float enemydist;
    public float originalspotdist;
    bool attack = false;
    int firedmg;
    int attackdmg;
    public int enemysp;
    bool eskillused;
    public float eskilltimer = 5;
    int currentscene;
    //Used to make the cube move towards the player the first time when the function is called 
    public bool moveonce = false;
    public bool block = false;
    // Start is called before the first frame update
    void Start()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().buildIndex;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        na = GetComponent<NavMeshAgent>();
        tbs = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
        es = GetComponent<EnemyStats>();
        sk = GameObject.Find("GameManager").GetComponent<Skills>();
        enemysp = es.stats["SP"];
        player = GameObject.Find("Player");
        bh = GameObject.Find("ButtonHandler").GetComponent<ButtonHandler>();
        eanim = GetComponent<Animator>();
        target = player.transform;
        originalspot = GameObject.Find("EnemyOriginalPosition").GetComponent<Transform>();

        if (currentscene == 6) 
        {
            //Enemy Hit box will be where the Enemy also attacks from
            enemyhitbox = gameObject.transform.Find("EnemyHitbox").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get the distance between the player and the enemy (If in normal battle scene it will just get the enemies position, in in the final boss scene then it will get the boss enemyhitbox pos)
        if (currentscene == 1)
        {
            enemydist = Vector3.Distance(target.transform.position, transform.position);
        }
        else 
        {
            enemydist = Vector3.Distance(target.transform.position, enemyhitbox.transform.position);
        }
        
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

        if (!attack) 
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //If in the final boss battle then it will look for the Enemy animation state called block
        if (currentscene == 6) 
        {
            if (block)
            {
                eanim.SetBool("block", true);
            }
            else
            {
                eanim.SetBool("block", false);
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
            eanim.SetBool("iswalking", true);
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
                //Player Animation for when he gets hit plays
                bh.anim.SetTrigger("hit");
            }
            else 
            {
                //Decrease Attack damage by 30% if Player is blocking
                gm.pHealth -= (attackdmg*30/100);
            }

            eanim.SetTrigger("punch");
            //Indicates that the Enemy has already attacked
            attack = true;
            //Debug.Log("1");

        }
        else if (enemydist < 3 && attack) // If the Enemy has finished his attack and is still near the player, it will go back to its original spot
        {
            na.SetDestination(originalspot.position);
            na.isStopped = false;
            eanim.SetBool("iswalking", true);
            //Debug.Log("2");

        }

        else if (originalspotdist < 0.5 && attack) // Once it gets back to its original position after its attack then it will stop and the destination will be set to the player again if it attacks again
        {
            
            na.SetDestination(target.position);
            na.isStopped = true;
            attack = false;
            //Debug.Log("Works");
            eanim.SetBool("iswalking", false);
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
            eanim.SetBool("iswalking", true);
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
                    bh.anim.SetTrigger("hit");
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

    public void Block() 
    {
        block = true;
        Debug.Log("block");
        //End the enemy turn
        tbs.enemyturn = false;
        //Start the Players Turn
        tbs.PlayerTurn();
    }
}
