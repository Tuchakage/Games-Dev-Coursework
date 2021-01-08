using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Purpose of this script is to keep hold of the enemies stats so it can be sent over to the battle scene
public class EnemyStatHolder : MonoBehaviour
{
    EnemyDetection ed;
    public int enemyspeed;
    // Start is called before the first frame update
    void Start()
    {
        ed = GameObject.Find("EnemyDetection").GetComponent<EnemyDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyspeed = ed.espeed;
    }

    public int getESpeed() 
    {
        return enemyspeed;
    }
}
