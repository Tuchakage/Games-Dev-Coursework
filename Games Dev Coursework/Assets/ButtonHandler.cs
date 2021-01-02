using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    EnemyHealth eh;
    TurnBasedSystem tbs;
    GameManager gm;
    PlayerStats ps;

    int playerdamage;

    private void Start()
    {
        eh = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
        tbs = GameObject.Find("TurnBasedSystem").GetComponent<TurnBasedSystem>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
    }
    public void attackButton() 
    {
        playerdamage = ps.stats["Attack"];
        Debug.Log("Attack Button");
        //Everytime i press button take away -10
        eh.LoseHealth(playerdamage);

        //My Turn will be over and it will be the enemies turn
        tbs.enemyturn = true;
    }

    public void escapeButton() 
    {
        Debug.Log("Escape Button");
        gm.battleend = true;
        SceneManager.LoadScene("test");
        

    }
}
