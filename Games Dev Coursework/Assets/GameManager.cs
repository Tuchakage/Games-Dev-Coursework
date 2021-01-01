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
        //Updates The Health If In Battle Scene
        if (currentscene == 1) 
        {
            phealthslider.value = phealth;
        }
        

        if (phealth <= 0) 
        {
            Destroy(player);
        }

        playerlastpos = player.transform.position;
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
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);

        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().buildIndex;

        //Everytime a Scene is loaded it will try to find the Player Health
        phealthslider = GameObject.Find("PlayerHealth").GetComponent<Slider>();

        player = GameObject.Find("Player");


    }


}
