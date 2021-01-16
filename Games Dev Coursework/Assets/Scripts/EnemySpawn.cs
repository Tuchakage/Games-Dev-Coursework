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
    public List<Vector3> enemylastpos = null;
    public List<string> see = null;

    public int maxenemies;
    int maxspawnpoints;
    int currentscene;
    bool firstspawn = false; //This will make the enemies spawn in at first at their spawnpoints, only to be done once
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (currentscene != 0 || currentscene != 1) 
        {
            
            for (int i = 0; i < enemies.Count; i++)
            {
                // If anything is null in the enemies gameObject then this code will run which will remove that null gameObject from the list this should be checked throughout the game
                if (enemies[i] == null) 
                {         
                    enemies.RemoveAt(i);
                }
                enemylastpos[i] = enemies[i].transform.position;
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

        //When in the Dungeon Level
        if (currentscene == 2)
        {
            if (!firstspawn)
            {
                //If in Dungeon Level then there will be 3 spawnpoints that need to be found
                maxspawnpoints = 3;
                //Find the Spawnpoint GameObjects and put it into the spawnpoints list
                for (int i = 1; i < maxspawnpoints + 1; i++)
                {
                    GameObject spawnpoint = GameObject.Find("Spawnpoint" + i);
                    Debug.Log("Found " + spawnpoint);
                    spawnpoints.Add(spawnpoint);
                }

                for (int i = 0; i < maxenemies; i++)
                {
                    //Enemy GameObjects will be spawned in using the enemy prefab and the list of spawnpoints
                    enemy = Instantiate(enemyspawner, spawnpoints[i].transform.position, Quaternion.identity);
                    //Change The Name For Each Object
                    enemy.name = "Enemy " + i;
                    //The Enemy GameObject Will be added to the enemy list
                    enemies.Add(enemy);
                    //If Any GameObject is null then it will destroy the last position of that gameObject, this should be done every scene load otherwise it will delete everything in the lastpos list
                    enemylastpos.Add(enemies[i].transform.position);
                    see.Add(enemies[i].gameObject.name);
                }
                Debug.Log("First Time Spawning Enemies");
                firstspawn = true;
            }
        }
    }

    public void SpawnEnemiesToLastPos()
    {
        for (int i = 0; i < maxenemies; i++)
        {
            //2 Enemy GameObjects will be spawned in using the enemy prefab and the list of spawnpoints
            enemy = Instantiate(enemyspawner, enemylastpos[i], Quaternion.identity);
            //Change The Name For Each Object
            enemy.name = "Enemy " + i;
            //The Enemy GameObject Will be added to the enemy list
            enemies.Add(enemy);

            //If Any GameObject is null then it will destroy the last position of that gameObject, this should be done every scene load otherwise it will delete everything in the lastpos list
            if (enemies[i] == null)
            {
                enemylastpos.RemoveAt(i);
                see.RemoveAt(i);
            }
            Debug.Log("SpawnEnemiesToLastPos() Called");

        }
    }

}
