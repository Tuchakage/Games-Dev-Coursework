using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Purpose of this script is to keep hold of the enemies stats so it can be sent over to the battle scene
public class EnemyStatHolder : MonoBehaviour
{
    public int enemyspeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public int getESpeed() 
    {
        return enemyspeed;
    }

    public void setSpeed(int speed) 
    {
        enemyspeed = speed;
        Debug.Log("Setted the Enemy Speed");
    }
}
