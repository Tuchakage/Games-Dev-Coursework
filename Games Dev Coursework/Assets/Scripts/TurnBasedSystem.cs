using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnBasedSystem : MonoBehaviour
{
    //Disabling the player UI
    public GameObject pui;
    public GameObject sk; // Skills Menu
    public TMP_Text advtext;

    BattleEnemyAI ea;
    EnemyStats es;
    PlayerStats ps;
    StartBattle sb;
    Advantage adv;
    ButtonHandler bh;

    int random;
    int playerspeed;
    int enemyspeed;
    public float texttimer = 3;
    public bool textused = false;
    public bool enemyturn = false;
    bool choseanumber = false;
    // Start is called before the first frame update
    void Start()
    {
        ea = GameObject.Find("Enemy(battle)").GetComponent<BattleEnemyAI>();
        es = GameObject.Find("Enemy(battle)").GetComponent<EnemyStats>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        sb = GameObject.Find("Blade").GetComponent<StartBattle>();
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
        else if (playerspeed > enemyspeed && adv.GetEnemyAdvantage() || adv.GetPlayerAdvantage())
        {
            PlayerTurn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyturn)
        {
            EnemyTurn();
        }

        if (textused) 
        {
            if (texttimer > 0)
            {
                texttimer -= Time.deltaTime;
            }
            else 
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
        Debug.Log("My Turn");
        choseanumber = false;
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
            Debug.Log("FIRE ATTACK");
        }


    }


}
