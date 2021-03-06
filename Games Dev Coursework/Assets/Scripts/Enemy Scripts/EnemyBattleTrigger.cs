using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//The Purpose of this script is for when the Enemy hits the Player first
public class EnemyBattleTrigger : MonoBehaviour
{
    Advantage adv;
    GameManager gm;

    string currentscene;
    public bool collision = false; // When this is set to true then the Advantage Script can find the name of the object 
    // Start is called before the first frame update
    void Start()
    {
        adv = GameObject.Find("GameManager").GetComponent<Advantage>();
        adv.setEnemyAdvantage(false);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentscene = SceneManager.GetActiveScene().name;
    }

    void OnTriggerEnter(Collider col)
    {
        //If Enemy Touches The Player Then A Battle Will Start
        if (col.gameObject.tag == "Player")
        {
            //The Game Object that touches the player will be sent over to the Game Object
            gm.setEnemyObject(this.gameObject.name);
            Debug.Log(gameObject.name + " Hit " + col.gameObject.name + " Enemy Advantage");
            //When The Enemy Hits The Player then the Enemy is guranteed to go first
            adv.setEnemyAdvantage(true);

            if (currentscene == "final")
            {
                SceneManager.LoadScene("finalbattle");
            }
            else 
            {
                SceneManager.LoadScene("battle test");
            }
        }
    }


    private void OnTriggerExit(Collider col)
    {
        collision = false;      
    }

}
