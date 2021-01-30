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
    public Animator anim;
    Skills sk;
    ThirdPersonCamera tpc;
    EnemyStats es;
    BattleEnemyAI bea;
    BattleLevelChanger blc;
    PlayerDealsDamage pdd;
    EnemySpawn espawn;

    GameObject player;

    public GameObject pui;
    public GameObject sui; //Skills UI
    public GameObject fire;
    public GameObject lightning;
    public GameObject enemy;

    bool attack = false;
    bool moveonce = false;
    public bool attackbuttonpressed = false;
    public string attacktype;
    public bool block = false; //Is The Player Blocking?
    string currentscene;

    //Skills
    bool skillmenu = false;
    float skilltimer;
    bool skillused = false;
    bool checkfornewmoves = false; //When you press the Skill button it will only look for the Thunder Skill once
    public bool thunderused = false; //Tells the program when the Thunder Skill has been used

    public Transform target;
    public Transform originalspot;

    public float enemydistance;
    public float playerogpos;


    private void Start()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        eh = enemy.GetComponent<EnemyHealth>();
        tbs = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        na = GameObject.Find("Player").GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        anim = GameObject.Find("Player").GetComponent<Animator>();
        sk = GameObject.Find("GameManager").GetComponent<Skills>();
        tpc = GameObject.Find("Third Person Camera").GetComponent<ThirdPersonCamera>();
        es = enemy.GetComponent<EnemyStats>();
        //So we can get the Enemies Animator
        bea = enemy.GetComponent<BattleEnemyAI>();
        blc = GameObject.Find("GameManager").GetComponent<BattleLevelChanger>();
        pdd = GameObject.Find("Player").GetComponent<PlayerDealsDamage>();
        espawn = GameObject.Find("GameManager").GetComponent<EnemySpawn>();

        //If on the final boss battle then the target will be the enemy hit box
        if (currentscene == "finalbattle")
        {
            target = GameObject.Find("EnemyHitbox").GetComponent<Transform>();
        }
        else //If just a normal battle scene then the target will be the enemy
        {
            target = enemy.transform;
        }
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
        else 
        {
            player.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //When you are in the skill menu and you want to go back to the normal player ui because you decide you want to do a normal attack instead, Press the right mouse button down
        if (Input.GetButtonDown("Fire2") && skillmenu) 
        {
            skillmenu = false;
            pui.SetActive(true);
            sui.SetActive(false);
        }

        //If Skill used is set to true then a timer will start
        if (skillused) 
        {
            if (skilltimer > 0)
            {
                skilltimer -= Time.deltaTime;
            }
            else 
            {
                //If The Timer is at 0 then it will be the Enemys go
                tbs.enemyturn = true;
                skillused = false;
            }
        }

        if (block)
        {
            anim.SetBool("block", true);
        }
        else 
        {
            anim.SetBool("block", false);
        }
    }

    public void Block() 
    {
        block = true;  
        tbs.enemyturn = true;
    }
    public void attackButton() 
    {
        Debug.Log("Attack Button");
        attacktype = "Physical";
        attackbuttonpressed = true;
        //Whenever the button is pressed then disable all the other Buttons
        pui.SetActive(false);
     
    }

    public void skillButton() 
    {
        //Whenever the button is pressed then disable all the other Buttons
        pui.SetActive(false);
        sui.SetActive(true);

        //If tht Thunder Skill hasn't been unlocked then the player can not see the Thunder Skill
        if (!sk.thunderunlock && !checkfornewmoves) 
        {
            GameObject thunder = GameObject.Find("Thunder");
            thunder.SetActive(false);
            //In this recent battle we have already checked for any new moves once
            checkfornewmoves = true;
        }

        skillmenu = true;
    }

    public void escapeButton() //Can be used for the Retry Button As well
    {
        Debug.Log("Escape Button");
        gm.battleend = true;

        //These If Statements make it so that when you press the Escape button depending on the scene you were just in, it will spawn you back in
        if (blc.GetLevelName() == "Dungeon")
        {
            SceneManager.LoadScene("dungeon");
        }
        else if (blc.GetLevelName() == "Desert") 
        {
            SceneManager.LoadScene("desert");
        }
        else if (blc.GetLevelName() == "Bar") 
        {
            SceneManager.LoadScene("bar");
        }

        if (gm.pHealth <= 0) 
        {
            //Reset Health and SP when you press the Retry button
            gm.pHealth = 100;
            gm.pSP = 50;
            //Reset the First Spawn Variable in Enemy Spawn
            espawn.resetFirstSpawn();
            //Delete everything currently in the Spawnpoints list 
            espawn.ResetSpawnList();
            if (blc.GetLevelName() == "Final") 
            {
                SceneManager.LoadScene("final");
            }
        }
    }

    public void PlayerAttack() 
    {
        if (!moveonce)
        {
            na.SetDestination(target.position);
            na.isStopped = false;
            anim.SetBool("running", true);
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

            //Player Damage Is Done In The PlayerDealsDamage script where it will be triggered by the attack animation as an animation event

        }
        else if (enemydistance < 3 && attack) // If the Player has finished his attack and is still near the Enemy, it will go back to its original spot
        {
            na.SetDestination(originalspot.position);
            na.isStopped = false;
        }

        else if (playerogpos < 2.1 && attack) // Once it gets back to its original position after its attack then it will stop and the destination will be set to the Enemy again if it attacks again
        {
            tpc.backtopos = false;
            na.SetDestination(target.position);
            na.isStopped = true;
            anim.SetBool("running", false);
            attack = false;
            attackbuttonpressed = false;
            //When it gets back to position it will be the Enemies turn
            tbs.enemyturn = true;          
        }
        //When the Player has already used its moveonce variable which is treated like its in the OnStart() function and is not attacking and is already at the original position 
        else if (playerogpos < 2.1 && !attack && moveonce)
        {
            //Player will move towards the player
            na.isStopped = false;
            anim.SetBool("running", true);
        }
        
    }

    public void FireSkill() 
    {
        if (gm.pSP > 0) 
        {
            anim.SetTrigger("cast");
            int firedamage = sk.skills["Fire"];
            //You lose SP When doing a Skill
            gm.pSP -= 5;

            attacktype = "Fire";
            if (!bea.block)
            {
                //Check the enemy weakness
                if (es.Weakness == attacktype)
                {
                    //Enemy Takes Double Damage
                    eh.LoseHealth(firedamage * 2);
                }
                else
                {
                    //Enemy Takes Damage
                    eh.LoseHealth(firedamage);
                }
                //Enemy Animation for when he gets hit plays
                bea.eanim.SetTrigger("hit");
            }
            else
            {
                //Damage To Enemy Reduced By 30%
                eh.LoseHealth(firedamage * 30 / 100);
            }


            GameObject fireprefab = Instantiate(fire, enemy.transform.position, enemy.transform.rotation);
            skillused = true;
            skilltimer = 5;
            sui.SetActive(false);
            skillmenu = false;
            Destroy(fireprefab, 5);

        }
     
    }

    public void ThunderSkill()
    {
        if (gm.pSP > 0)
        {
            anim.SetTrigger("cast");

            //This means the Thunder Button has been pressed
            thunderused = true;
            skillused = true;
            skilltimer = 3;
            sui.SetActive(false);
            skillmenu = false;
        }
    }
}
