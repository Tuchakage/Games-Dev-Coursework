using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Purpose Of This Script Is So That The Start Battle Script Can Send Info To This Script That Will Be On The Game Manager So When I Change Scenes To Battle Scene
//This Script Will Then Send The Info To The TurnBasedSystem Script
public class Advantage : MonoBehaviour
{
    StartBattle sb;
    EnemyBattleTrigger ebt;
    // Start is called before the first frame update
    void Start()
    {
        sb = GameObject.Find("Blade").GetComponent<StartBattle>();
        ebt = GameObject.Find("EnemyCube").GetComponent<EnemyBattleTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetPlayerAdvantage() 
    {
        
        return sb.playeradvantage; 
    }

    public bool GetEnemyAdvantage() 
    {
        return ebt.enemyadvantage;
    }
}
