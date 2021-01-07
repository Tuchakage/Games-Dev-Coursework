using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : MonoBehaviour
{
    GameObject player;
    public bool playeradvantage = false;
    GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) 
    {
        //The Trigger will only work if it is not at the end of the battle 
        if (!gm.battleend) 
        {
            //If The Blade touches the Enemy Back Then A Battle Will Start and the player is guranteed to go first
            if (col.gameObject.name == "EnemyBack" && col.gameObject.name != "Player")
            {
                //When The Players Sword Hits the back of the Enemy then the Player is guranteed to go First
                Debug.Log(gameObject.name + " Hit " + col.gameObject.name + " Player Advantage");
                playeradvantage = true;

                SceneManager.LoadScene("battle test");
            }
            else if (col.gameObject.tag == "Enemy" && col.gameObject.name != "Player") //If the blade touches the enemy not including its back then whoever has a high speed stat will go first
            {
                Debug.Log(gameObject.name + " OnCollisionEnter()" + col.gameObject.name + " Neutral");
                SceneManager.LoadScene("battle test");
            }
        }


    }

}
