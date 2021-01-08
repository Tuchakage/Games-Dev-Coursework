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

    int currentscene;
    // Start is called before the first frame update
    void Start()
    {
        sb = GameObject.Find("Blade").GetComponent<StartBattle>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (currentscene != 1) 
        {
            if (sb.collision)
            {
                if (sb.enemyref != null) 
                {
                    ebt = GameObject.Find(sb.enemyref.name).GetComponent<EnemyBattleTrigger>();
                }
                
            }
        }

        
    }

    public bool GetPlayerAdvantage() 
    {
        
        return sb.playeradvantage; 
    }

    public bool GetEnemyAdvantage() 
    {
        return ebt.enemyadvantage;
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
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Check What Scene you are on
        currentscene = SceneManager.GetActiveScene().buildIndex;

    }
}
