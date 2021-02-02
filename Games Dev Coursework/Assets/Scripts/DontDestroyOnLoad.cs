using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    EnemySpawn es;
    string currentscene;
    public static DontDestroyOnLoad Instance
    {
        get
        {
            return instance;
        }
    }

    private static DontDestroyOnLoad instance = null;

    void Awake()
    {
        if (instance)
        {
            Debug.Log("already an instance so destroying new one");
            DestroyImmediate(gameObject);
            return;

        }

        instance = this;

        DontDestroyOnLoad(gameObject);

    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        //Adds OnSceneLoaded()
        SceneManager.sceneLoaded += OnSceneLoaded;
        es = GameObject.Find("GameManager").GetComponent<EnemySpawn>();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;

        if (currentscene == "hub") //Reset the First Spawn Variable and Clear everything from The spawnpoints list so that enemies can spawn back in
        {
            es.resetFirstSpawn();
            es.ResetSpawnList();
            Destroy(this.gameObject);           
        }
    }
}
