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

    public int maxenemies;
    int currentscene;
    bool regpos = false; //This is to make it so that all the positions of the enemies have already been stored only once

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    private void Update()
    {

        //Destroy(be.enemyref);

        //This code will loop through the enemies list and if any object in the list has been destroyed then it will destroy the list as well
        for (int i = 0; i < enemies.Count; i++) 
        {
            if (enemies[i] == null) 
            {
                enemies.RemoveAt(i);
                enemylastpos.RemoveAt(i);
            }
        }

        if (currentscene == 0) 
        {
            for (int i = 0; i < maxenemies; i++)
            {
                enemylastpos[i] = enemies[i].transform.position;
            }
        }


    }
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        //Adds OnSceneLoaded()
        SceneManager.sceneLoaded += OnSceneLoaded;
        maxenemies = 2;

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
                enemylastpos.Add(enemies[i].transform.position);
           
            }
        }
        regpos = true;


    }
}
