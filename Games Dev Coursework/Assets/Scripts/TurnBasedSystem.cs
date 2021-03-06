using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TurnBasedSystem : MonoBehaviour
{
    //Disabling the player UI
    public GameObject pui;
    public GameObject sk; // Skills Menu
    public TMP_Text advtext;

    BattleEnemyAI ea; //This is where the Enemies moveset is 
    EnemyStats es;
    PlayerStats ps;
    Advantage adv;
    ButtonHandler bh;

    string currentscene;
    int random;
    int playerspeed;
    int enemyspeed;
    public float texttimer = 3;
    public bool textused = false;
    public bool enemyturn = false;
    bool choseanumber = false; //Makes it so that everytime the function is called the number will change
    bool attackorderchange = false; //Makes it so the Attack Order is only changed once when the function is called

    public int attackorder = 1; //The Final Boss will not randomly choose a move, it will go in a sequence

    // Start is called before the first frame update
    void Start()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;
        ea = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BattleEnemyAI>();
        es = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        adv = GameObject.Find("GameManager").GetComponent<Advantage>();
        bh = GameObject.Find("ButtonHandler").GetComponent<ButtonHandler>();
        playerspeed = ps.stats["Speed"];
        enemyspeed = es.stats["Speed"];
        Debug.Log("TURN BASED SYSTEM SCRIPT ESPEED: " + enemyspeed);

        if (adv.GetPlayerAdvantage())
        {
            advtext.text = "PLAYER ADVANTAGE";
            textused = true;
            advtext.color = new Color(0, 0, 1, 1);
        }
        else if (adv.GetEnemyAdvantage())
        {
            advtext.text = "ENEMY ADVANTAGE";
            advtext.color = new Color(1, 0, 0, 1);
            textused = true;
        }
        else 
        {
            advtext.text = " ";
        }
        

        //Enemy Goes First If the speed is higher than the Players And Player Advantage is not true Or If Enemy Advantage is true
        if (playerspeed < enemyspeed && !adv.GetPlayerAdvantage() || adv.GetEnemyAdvantage())
        {
            enemyturn = true;
        }
        //Player Goes First if its speed is higher than the Enemys And Enemy Advantage is not true Or If Player Advantage is true
        else if (playerspeed > enemyspeed && !adv.GetEnemyAdvantage() || adv.GetPlayerAdvantage())
        {
            PlayerTurn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyturn && currentscene == "battle test")
        {
            EnemyTurn();
        }
        else if (enemyturn && currentscene == "finalbattle") 
        {
            BossTurn();
        }

        if (textused) 
        {
            if (texttimer > 0) //Timer will go down if the text timer is more than 0
            {
                texttimer -= Time.deltaTime;
            }
            else //When it reaches 0 the text will be empty and textused will be set to false
            {
                advtext.text = " ";
                textused = false;
            }
        }

    }

    public void PlayerTurn() 
    {
        //When Players Turn again then it will no longer be blocking
        bh.block = false;
        pui.SetActive(true);
        sk.SetActive(false);
        //Debug.Log("My Turn");
        //Enemy randomly chooses what it wants to do again in its next turn
        choseanumber = false;
        if (currentscene == "finalbattle") 
        {
            if (!attackorderchange) 
            {
                if (attackorder == 1)
                {
                    attackorder = 2;
                }
                else if (attackorder == 2)
                {
                    attackorder = 1;
                }
                attackorderchange = true;
            }

        }
    }

    public void EnemyTurn() 
    {
        pui.SetActive(false);
        sk.SetActive(false);
        //Debug.Log("Enemy Turn");
        if (!choseanumber) 
        {
            random = Random.Range(1, 3);

            Debug.Log("Random Number Chosen: " + random);
            choseanumber = true;
        }

        if (random == 1 || (random == 2 && ea.enemysp <= 0))
        {
            //Gets the enemy attack function from the Enemy Ai script
            ea.EnemyAttack();
        }
        else if (random == 2 && ea.enemysp > 0) 
        {
            ea.Fire();
            //Debug.Log("FIRE ATTACK");
        }


    }

    public void BossTurn() 
    {
        Debug.Log("boss turn");
        //When Final Boss Turn again then it will no longer be blocking
        ea.block = false;
        pui.SetActive(false);
        sk.SetActive(false);
        //The Attack Order is not being changed it will change when it is the players turn
        attackorderchange = false;
        if (attackorder == 1)
        {
            ea.EnemyAttack();
        }
        else if (attackorder == 2) 
        {
            ea.Block();
        }
    }
}
