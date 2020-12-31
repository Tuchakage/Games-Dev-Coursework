using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    EnemyHealth eh;
    TurnBasedSystem tbs;

    private void Start()
    {
        eh = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
        tbs = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
    }
    public void attackButton() 
    {
        Debug.Log("Attack Button");
        //Everytime i press button take away -10
        eh.LoseHealth(10);

        //My Turn will be over and it will be the enemies turn
        tbs.enemyturn = true;
    }
}
