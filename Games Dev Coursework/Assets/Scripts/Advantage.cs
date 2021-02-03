using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//The Purpose Of This Script Is So That The Start Battle Script Can Send Info To This Script That Will Be On The Game Manager So When I Change Scenes To Battle Scene
//This Script Will Then Send The Info To The TurnBasedSystem Script
public class Advantage : MonoBehaviour
{
    StartBattle sb;
    EnemyBattleTrigger ebt;

    bool enemyadvantage = false;
    bool playeradvantage = false;

    string currentscene;

    public bool GetPlayerAdvantage() 
    {
        
        return playeradvantage; 
    }

    public bool GetEnemyAdvantage() 
    {
        return enemyadvantage;
    }

    public void setEnemyAdvantage(bool adv) 
    {
        enemyadvantage = adv;
    }

    public void setPlayerAdvantage(bool adv)
    {
        playeradvantage = adv;
    }

    //Whenever the Game Object is enabled this function will run
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        //Adds OnSceneLoaded()
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    //When A New Scene Loads this function will be run
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Check What Scene you are on
        currentscene = SceneManager.GetActiveScene().name;

    }
}
