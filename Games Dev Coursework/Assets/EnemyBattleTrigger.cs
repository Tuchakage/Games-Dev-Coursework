using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBattleTrigger : MonoBehaviour
{
    public bool enemyadvantage = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        //If Enemy Touches The Player Then A Battle Will Start
        if (col.gameObject.name != "EnemyBack")
        {
            Debug.Log(gameObject.name + " OnCollisionEnter()" + col.gameObject.name);
            //When The Enemy Hits The Player then the Enemy is guranteed to go first
            enemyadvantage = true;
            SceneManager.LoadScene("battle test");

        }

    }
}
