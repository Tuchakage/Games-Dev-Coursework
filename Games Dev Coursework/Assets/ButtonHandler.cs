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
    Skills sk;

    GameObject player;

    public GameObject pui;
    public GameObject sui; //Skills UI
    public GameObject fire;
    public GameObject enemy;

    int playerdamage;
    bool attack = false;
    bool moveonce = false;
    public bool attackbuttonpressed = false;

    //Skills
    int firedamage;
    bool skillmenu = false;
    float skilltimer;
    bool skillused = false;

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
        sk = GameObject.Find("GameManager").GetComponent<Skills>();
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

        //When you are in the skill menu and you want to go back to the normal player ui because you decide you want to do a normal attack instead, Press the right mouse button down
        if (Input.GetButtonDown("Fire2") && skillmenu) 
        {
            skillmenu = false;
            pui.SetActive(true);
            sui.SetActive(false);
        }

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
    }
    public void attackButton() 
    {
        playerdamage = ps.stats["Attack"];
        Debug.Log("Attack Button");

        attackbuttonpressed = true;
    }

    public void skillButton() 
    {
        pui.SetActive(false);

        sui.SetActive(true);
        skillmenu = true;
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

    public void FireSkill() 
    {
        if (gm.pSP > 0) 
        {
            firedamage = sk.skills["Fire"];
            //You lose SP When doing a Skill
            gm.pSP -= 5;
            //Enemy Takes Damage
            eh.LoseHealth(firedamage);
            GameObject fireprefab = Instantiate(fire, enemy.transform.position, enemy.transform.rotation);
            skillused = true;
            skilltimer = 5;
            Destroy(fireprefab, 5);
        }
     
    }
}
