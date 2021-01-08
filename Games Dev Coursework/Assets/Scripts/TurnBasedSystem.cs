using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour
{
    //Disabling the player UI
    public GameObject pui;
    public GameObject sk; // Skills Menu

    EnemyAI ea;
    EnemyStats es;
    PlayerStats ps;
    StartBattle sb;
    Advantage adv;
    EnemyStatHolder esh;

    int playerspeed;
    int enemyspeed;

    public bool enemyturn = false;
    // Start is called before the first frame update
    void Start()
    {
        ea = GameObject.Find("Enemy").GetComponent<EnemyAI>();
        es = GameObject.Find("GameManager").GetComponent<EnemyStats>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        sb = GameObject.Find("Blade").GetComponent<StartBattle>();
        adv = GameObject.Find("GameManager").GetComponent<Advantage>();
        esh = GameObject.Find("GameManager").GetComponent<EnemyStatHolder>();
        playerspeed = ps.stats["Speed"];
        enemyspeed = esh.enemyspeed;
        Debug.Log("TURN BASED: " + enemyspeed);

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

    }

    public void PlayerTurn() 
    {
        pui.SetActive(true);
        sk.SetActive(false);
        Debug.Log("My Turn");
    }

    public void EnemyTurn() 
    {
        pui.SetActive(false);
        sk.SetActive(false);
        //Debug.Log("Enemy Turn");

        //Gets the enemy attack function from the Enemy Ai script
        ea.EnemyAttack();
    }


}
