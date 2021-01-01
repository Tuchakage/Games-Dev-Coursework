using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider phealthslider;
    public GameObject player;

    //Gets The Players last position
    public Vector3 playerlastpos;
    int currentscene;
    //Player Base Health
    public float phealth = 100;
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

    }

    // Update is called once per frame
    void Update()
    {

        if (currentscene == 1)
        {
            //Updates The Health If In Battle Scene
            phealthslider.value = phealth;
        }
        else 
        {
            //If Not In Battle Scene then it will keep record of the players last position
            if (!battleend) 
            {
                playerlastpos = player.transform.position;
            }
            
        }
        

        if (phealth <= 0) 
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
        }

        if (battleend) 
        {
            //Set The Players Position to where he was before the battle
            player.transform.position = playerlastpos;
            battleend = false;
        }
        


    }


}
