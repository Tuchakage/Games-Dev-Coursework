using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//The Purpose of this script is so that a battle can start when the players sword hits the enemy
public class StartBattle : MonoBehaviour
{
    GameObject player;
    GameManager gm;
    Advantage adv;

    string currentscene;
    public bool collision = false;// When this is set to true then the Advantage Script can find the name of the object 
    public GameObject enemyref; //Reference of the Enemy Game Object
    // Start is called before the first frame update
    void Start()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;
        player = GameObject.Find("Player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        adv = GameObject.Find("GameManager").GetComponent<Advantage>();
        adv.setPlayerAdvantage(false);
    }

    void OnTriggerEnter(Collider col) 
    {
        //The Trigger will only work if it is not at the end of the battle 
        if (!gm.battleend) 
        {
            //If The Blade touches the Enemy Back Then A Battle Will Start and the player is guranteed to go first
            if (col.gameObject.name == "EnemyBack" && currentscene != "final")
            {
                //When The Players Sword Hits the back of the Enemy then the Player is guranteed to go First
                Debug.Log(gameObject.name + " Hit " + col.gameObject.name + " Player Advantage");
                adv.setPlayerAdvantage(true);
                //Blade has touched the Enemy
                collision = true;
                //Sets the variable to whatever object the blade has collided with
                enemyref = col.gameObject;

                gm.setEnemyObject(col.gameObject.name);
                SceneManager.LoadScene("battle test");
            }
            else if (col.gameObject.tag == "Enemy" && currentscene != "final") //If the blade touches the enemy not including its back then whoever has a high speed stat will go first
            {
                collision = true;
                //Sets the variable to whatever object the blade has collided with
                enemyref = col.gameObject;
                Debug.Log(gameObject.name + " OnCollisionEnter()" + col.gameObject.name + " Neutral");

                gm.setEnemyObject(col.gameObject.name);
                SceneManager.LoadScene("battle test");
            }
            else if (col.gameObject.tag == "Enemy" && currentscene == "final") 
            {
                collision = true;
                //Sets the variable to whatever object the blade has collided with
                Debug.Log(gameObject.name + " OnCollisionEnter()" + col.gameObject.name + " Neutral");

                //Sets the variable to whatever object the blade has collided with
                enemyref = col.gameObject;
                gm.setEnemyObject(col.gameObject.name);
                SceneManager.LoadScene("finalbattle");
            }
        }


    }

    private void OnTriggerExit(Collider col)
    {
        collision = false;       
    }

}
