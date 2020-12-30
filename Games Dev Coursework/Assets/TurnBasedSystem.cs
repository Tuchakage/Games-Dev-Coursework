using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour
{
    //Disabling the player UI
    public GameObject pui;

    float Timer = 0;

    bool enemyturn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            if (enemyturn) 
            {
                PlayerTurn();
                enemyturn = false;
            }
            
        }

    }

    public void PlayerTurn() 
    {
        pui.SetActive(true);
        Debug.Log("My Turn");
    }

    public void EnemyTurn() 
    {
        enemyturn = true;
        pui.SetActive(false);
        Debug.Log("Enemy Turn");

        //After Enemy Animation then it goes back to player But For Now it is Timer based
        Timer = 5;
    }
}
