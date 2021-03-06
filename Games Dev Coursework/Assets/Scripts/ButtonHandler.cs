using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    TurnBasedSystem tbs;
    GameManager gm;
    NavMeshAgent na;
    public Animator anim;
    Skills sk;
    ThirdPersonCamera tpc;
    BattleLevelChanger blc;
    EnemySpawn espawn;
    AudioManager am;

    GameObject player;

    public GameObject pui;
    public GameObject sui; //Skills UI
    public GameObject enemy;

    public TMP_Text nosp;

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
    public bool fireused = false;
    public Transform target;
    public Transform originalspot;

    public float enemydistance;
    public float playerogpos;
    float texttimer = 3;
    bool textused = false;


    private void Start()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        tbs = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        na = GameObject.Find("Player").GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        anim = GameObject.Find("Player").GetComponent<Animator>();
        sk = GameObject.Find("GameManager").GetComponent<Skills>();
        tpc = GameObject.Find("Third Person Camera").GetComponent<ThirdPersonCamera>();
        blc = GameObject.Find("GameManager").GetComponent<BattleLevelChanger>();
        espawn = GameObject.Find("GameManager").GetComponent<EnemySpawn>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        //Make the text Empty
        nosp.text = " ";
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

        //If the text to show that you have no SP left is being used
        if (textused) 
        {
            if (texttimer > 0)
            {
                texttimer -= Time.deltaTime;
            }
            else
            {
                nosp.text = " ";
                textused = false;
            }
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
        //This if statement acts like a Start() Function but it will only trigger when you press the attack button
        if (!moveonce)
        {
            //When the attack button is pressed then the destination will be set to the target
            na.SetDestination(target.position);
            //This will make the player move
            na.isStopped = false;
            //Running animation is played
            anim.SetBool("running", true);
            //Set this to true because we will not need it anymore
            moveonce = true;
        }
        //When the Player has already used its moveonce variable which is treated like its in the OnStart() function and is not attacking and is already at the original position 
        else if (playerogpos < 2.1 && !attack && moveonce)
        {
            //Player will move towards the enemy
            na.isStopped = false;
            anim.SetBool("running", true);
        }
        if (enemydistance < 3 && !attack) // If the Player is near the Enemy and it hasn't attacked yet
        {
            //Player will stop
            na.isStopped = true;
            //Attack Animation Of The Player Will Play
            anim.SetTrigger("attack");
            //Play Sword Swing Sound
            am.SwordSwingSound();

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
    }

    public void FireSkill() 
    {
        if (gm.pSP > 0)
        {
            anim.SetTrigger("cast");

            fireused = true;
            skillused = true;
            skilltimer = 5;
            sui.SetActive(false);
            skillmenu = false;
        }
        else 
        {
            //Set the timer to 3 seconds
            texttimer = 3;
            //When SP is 0 or less then this text will pop up
            nosp.text = "YOU DONT HAVE ANY SP LEFT";
            nosp.color = new Color(1, 0, 0, 1);
            //To show that text is being used
            textused = true;
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
        else 
        {
            //Set the timer to 3 seconds
            texttimer = 3;
            //When SP is 0 or less then this text will pop up
            nosp.text = "YOU DONT HAVE ANY SP LEFT";
            nosp.color = new Color(1, 0, 0, 1);
            //To show that Text is being used
            textused = true;
        }
    }
}
