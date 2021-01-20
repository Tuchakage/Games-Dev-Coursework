using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleSpawner : MonoBehaviour
{
    public List<GameObject> enemyspawners = null;
    //public GameObject enemyspawner;
    public GameObject espawnpoint;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(enemyspawners[1], espawnpoint.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
