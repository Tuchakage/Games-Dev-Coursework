using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Purpose of this Script is that it is attached to a trigger and after the battle ends any enemies that are in the radius of the trigger then that object will be deleted
//so that another battle wont start
public class battleend : MonoBehaviour
{
    GameManager gm;
    EnemySpawn espawn;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        espawn = GameObject.Find("GameManager").GetComponent<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.name != "Player") 
        {
            Debug.Log(gameObject.name + " Touching " + col.gameObject.name);
            Debug.Log(gm.battleend);
            if (gm.battleend)
            {
               Destroy(col.gameObject);
               espawn.maxenemies -= 1;
               gm.battleend = false;
            }
            
        }
            
    }

}
