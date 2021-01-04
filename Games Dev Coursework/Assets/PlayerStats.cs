using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update

    public Dictionary<string, int> stats = null;

    //Player Stat will be loaded in before the Game Manager trys to get the HP Stat so that the player gameobject is not destroyed straight away
    void Awake()
    {
        stats = new Dictionary<string, int>();
        stats.Add("Attack", 10);
        stats.Add("HP", 100);
        stats.Add("Defence", 20);
        stats.Add("Speed", 30);
        stats.Add("SP", 50);
        //Debug.Log("Attack Stat:" + stats["Attack"]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
