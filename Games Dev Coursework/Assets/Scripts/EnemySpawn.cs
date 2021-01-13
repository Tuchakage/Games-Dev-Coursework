using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> enemies = null;
    public List<GameObject> spawnpoints = null;
    public GameObject enemyspawner;//This is where the prefab is stored
    public GameObject enemy;//This is where the spawned in Enemies will be put

    public int maxenemies;
    int currentscene;

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (currentscene == 0) 
        {
            
            for (int i = 0; i < enemies.Count; i++)
            {
                // If anything is null in the enemies gameObject then this code will run which will remove that null gameObject from the list this should be checked throughout the game
                if (enemies[i] == null) 
                {         
                    enemies.RemoveAt(i);
                }             
            }
        }


    }
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        //Adds OnSceneLoaded()
        SceneManager.sceneLoaded += OnSceneLoaded;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    //When A New Scene Loads this function will be run
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().buildIndex;

        if (currentscene == 0)
        {
            for (int i = 0; i < maxenemies; i++)
            {
                //2 Enemy GameObjects will be spawned in using the enemy prefab and the list of spawnpoints
                enemy = Instantiate(enemyspawner, spawnpoints[i].transform.position, Quaternion.identity);
                //Change The Name For Each Object
                enemy.name = "Enemy " + i;
                //The Enemy GameObject Will be added to the enemy list
                enemies.Add(enemy);

            }
        }
    }

}
