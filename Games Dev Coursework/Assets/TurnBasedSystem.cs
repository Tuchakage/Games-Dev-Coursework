using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour
{
    //Disabling the player UI
    public GameObject pui;

    EnemyAI ea;
    EnemyStats es;
    PlayerStats ps;

    int playerspeed;
    int enemyspeed;

    public bool enemyturn = false;
    // Start is called before the first frame update
    void Start()
    {
        ea = GameObject.Find("Enemy").GetComponent<EnemyAI>();
        es = GameObject.Find("GameManager").GetComponent<EnemyStats>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();

        playerspeed = ps.stats["Speed"];
        enemyspeed = es.stats["Speed"];

        //If The Player is slower than the Enemy than the Enemy goes first
        if (playerspeed < enemyspeed) 
        {
            enemyturn = true;
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
        Debug.Log("My Turn");
    }

    public void EnemyTurn() 
    {
        pui.SetActive(false);
        //Debug.Log("Enemy Turn");

        //Gets the enemy attack function from the Enemy Ai script
        ea.EnemyAttack();
    }


}
