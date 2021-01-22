using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBattleSpawner : MonoBehaviour
{
    public List<GameObject> enemyspawners = null;
    //public GameObject enemyspawner;
    public GameObject espawnpoint;

    int currentscene;
    // Start is called before the first frame update
    void Awake()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().buildIndex;
        if (currentscene == 1)
        {
            Instantiate(enemyspawners[0], espawnpoint.transform.position, Quaternion.identity);
        }
        else if (currentscene == 6) 
        {
            Instantiate(enemyspawners[1], espawnpoint.transform.position, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
