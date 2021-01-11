using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerStats ps;
    EnemySpawn espawn;

    public GameObject destroyenemy;
    public Slider phealthslider;
    public GameObject player;
    public Slider spslider;
    public string enemy;
    public int pHealth;
    public int pSP; //Players Stamina Points

    //Gets The Players last position
    public Vector3 playerlastpos;
    int currentscene;
    //To Indicate if a battle has just ended
    public bool battleend = false;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static GameManager instance = null;

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
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        //pHealth is set to the HP Stat in the Player Stats Script
        pHealth = ps.stats["HP"];
        //SP is set to the SP(Stamina Points) Stat in the Player Stats Script
        pSP = ps.stats["SP"];
        espawn = GameObject.Find("GameManager").GetComponent<EnemySpawn>();

    }

    // Update is called once per frame
    void Update()
    {

        if (currentscene == 1)
        {
            //Updates The Health If In Battle Scene
            phealthslider.value = pHealth;
            spslider.value = pSP;
        }
        else 
        {
            //If Not In Battle Scene then it will keep record of the players last position
            if (!battleend) 
            {
                playerlastpos = player.transform.position;
            }
            
        }
        
        //When The Player Health is 0 Then the Object will be destroyed
        if (pHealth <= 0) 
        {
            Destroy(player);
        }

    }

    //Whenever the Game Object is enabled this function will run
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        //Adds OnSceneLoaded()
        SceneManager.sceneLoaded += OnSceneLoaded;

        //Will find the Enemy GameObject by name
        destroyenemy = GameObject.Find(enemy);
    }

    //When A New Scene Loads this function will be run
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);

        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().buildIndex;

        player = GameObject.Find("Player");

        if (currentscene == 1)
        {
            //Look For The Health Slider if the scene has changed to the Battle Scene
            phealthslider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
            spslider = GameObject.Find("PlayerSP").GetComponent<Slider>();
            spslider.maxValue = ps.stats["SP"];
        }

        if (battleend) 
        {
            //Set The Players Position to where he was before the battle
            player.transform.position = playerlastpos;           
            Destroy(destroyenemy);
            //Reduce the max Amount Of Enemies spawned by 1
            espawn.maxenemies -= 1;
            battleend = false;
        }


    }

    public string getEnemyObject() 
    {
        return enemy;
    }
    public void setEnemyObject(string go) 
    {
        enemy = go;
    }


}
