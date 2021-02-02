using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is mainly for Game Developers and not the Players Just in case they want to test things out in only one scene without going through the hub
//The Purpose of this Script is so that when the player touches the trigger this script will send the level name to the BattleLevelChanger Script
public class LevelNameSender : MonoBehaviour
{
    BattleLevelChanger blc;
    public string sendlevelname;
    // Start is called before the first frame update
    void Start()
    {
        blc = GameObject.Find("GameManager").GetComponent<BattleLevelChanger>();
    }

    void OnTriggerEnter(Collider col)
    {       
        if (col.gameObject.tag == "Player") 
        {
            blc.SetLevelName(sendlevelname);
            Debug.Log(col.gameObject.name + " Touched " + gameObject.name);

        }
    }
}
