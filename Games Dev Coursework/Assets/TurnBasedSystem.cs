using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour
{
    //Disabling the player UI
    public GameObject pui;

    EnemyAI ea;

    public bool enemyturn = false;
    // Start is called before the first frame update
    void Start()
    {
        ea = GameObject.Find("Enemy").GetComponent<EnemyAI>();
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
