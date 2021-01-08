using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The Purpose of this script is so that the trigger will get the enemy that is in range, then it will get the EnemyStats script that is on the object and it will get the stats which will then
//be sent over to the EnemyStatHolder which is on the GameManager object so that it wont be destroyed
public class EnemyDetection : MonoBehaviour
{
    EnemyStats es;

    public int espeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && col.gameObject.name != "Player") 
        {
            es = col.gameObject.GetComponent<EnemyStats>();
            espeed = es.stats["Speed"];
        }
    }
}
