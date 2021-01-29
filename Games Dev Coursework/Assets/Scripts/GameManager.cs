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
    GameObject finallevelportal; // The Portal to the final level will be disabled and enabled with this variable 
    public GameObject failscreen;

    public string enemy;
    public int pHealth;
    public int pSP; //Players Stamina Points
    public int keys = 0;

    //Gets The Players last position
    public Vector3 playerlastpos;
    string currentscene;
    //To Indicate if a battle has just ended
    public bool battleend = false;
    public bool hasplayerlost = false; //To Indicate whether the Players Health is 0 Or Less
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

        if (currentscene == "battle test" || currentscene == "finalbattle")
        {
            //Updates The Health If In Battle Scene
            phealthslider.value = pHealth;
            //Setting Max Value to whatever is in the Player Stats script
            phealthslider.maxValue = ps.stats["HP"];
            spslider.value = pSP;
            //Setting Max Value to whatever is in the Player Stats script
            spslider.maxValue = ps.stats["SP"];

            //When The Player Health is 0 Then the Object will be destroyed
            if (pHealth <= 0)
            {
                failscreen.SetActive(true);
                hasplayerlost = true;
                Destroy(player);
            }
        }
        else
        {
            //If Not In Battle Scene then it will keep record of the players last position
            if (!battleend) 
            {
                playerlastpos = player.transform.position;
            }
            
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

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    //When A New Scene Loads this function will be run
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;

        //Destroy The GameManager when in the level complete scene which means we can restart the game
        if (currentscene == "winning screen")
        {
            Destroy(this.gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
        if (currentscene == "hub") 
        {
            finallevelportal = GameObject.Find("FinalLevelPortal");
            if (keys < 3) 
            {
                //Disable The Portal until you have 3 keys 
                finallevelportal.SetActive(false);
            }
        }
        else if (currentscene == "battle test" || currentscene == "finalbattle")
        {
            //Look For The Health Slider if the scene has changed to the Battle Scene
            phealthslider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
            spslider = GameObject.Find("PlayerSP").GetComponent<Slider>();
            failscreen = GameObject.Find("FailPanel");
            failscreen.SetActive(false);
        }

        if (battleend && !hasplayerlost)
        {
            //Set The Players Position to where he was before the battle
            player.transform.position = playerlastpos;
            Debug.Log("Player Last Position " + player.transform.position);
            Destroy(destroyenemy);
            //Reduce the max Amount Of Enemies spawned by 1
            espawn.maxenemies -= 1;
            espawn.SpawnEnemies();
            battleend = false;
            Debug.Log("Battle End Has Been Set To " + battleend);
        }
        else if (battleend && hasplayerlost) //If The Player loses then it means the battle has ended 
        {
            //Respawn all the enemies including the one you just battled
            espawn.FirstTimeSpawn();
            battleend = false;
            hasplayerlost = false;
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
