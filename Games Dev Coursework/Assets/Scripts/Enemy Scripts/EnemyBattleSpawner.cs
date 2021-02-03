using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//The Purpose of this script is so that in the battle scene only 1 enemy will spawn in when the Scene is loaded
public class EnemyBattleSpawner : MonoBehaviour
{
    public List<GameObject> enemyspawners = null;
    //public GameObject enemyspawner;
    public GameObject espawnpoint;

    string currentscene;
    // Start is called before the first frame update
    void Awake()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;
        if (currentscene == "battle test")
        {
            Instantiate(enemyspawners[0], espawnpoint.transform.position, Quaternion.identity);
        }
        else if (currentscene == "finalbattle") 
        {
            Instantiate(enemyspawners[1], espawnpoint.transform.position, Quaternion.identity);
        }
    }
}
