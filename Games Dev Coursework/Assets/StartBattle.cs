using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : MonoBehaviour
{
    GameObject player;
    public bool playeradvantage = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) 
    {
        //If The Blade touches the Enemy Back Then A Battle Will Start
        if (col.gameObject.name == "EnemyBack" && col.gameObject.name != "Player")
        {
            //When The Players Sword Hits the back of the Enemy then the Player is guranteed to go First
            Debug.Log(gameObject.name + " OnCollisionEnter()" + col.gameObject.name);
            playeradvantage = true;
            SceneManager.LoadScene("battle test");
        }
        else if (col.gameObject.name == "EnemyCube" && col.gameObject.name != "Player") 
        {
            Debug.Log(gameObject.name + " OnCollisionEnter()" + col.gameObject.name);
            SceneManager.LoadScene("battle test");
        }

    }

}
