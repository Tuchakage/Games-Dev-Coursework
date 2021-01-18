using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//This is the script that will change the environment of the battle scene
public class BattleLevelChanger : MonoBehaviour
{
    public string levelname;
    public GameObject dung;//The Dungeon GameObject that will be disabled/ enabled on the battle scene depending on the level
    public GameObject desert;//The Desert GameObject that will be disabled/ enabled on the battle scene depending on the level
    int currentscene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetLevelName() 
    {
        return levelname;
    }

    public void SetLevelName(string name) 
    {
        levelname = name;
    }

    //Whenever the Game Object is enabled this function will run
    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        //Adds OnSceneLoaded()
        SceneManager.sceneLoaded += OnSceneLoaded;




    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //When A New Scene Loads this function will be run
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);

        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().buildIndex;

        if (currentscene == 1) 
        {
            //Set all the battle environments to false after finding them
            dung = GameObject.Find("Dungeon");
            dung.SetActive(false);

            desert = GameObject.Find("Desert");
            desert.SetActive(false);

            if (levelname == "Dungeon")
            {
                dung.SetActive(true);
            }
            else if (levelname == "Desert") 
            {
                desert.SetActive(true);
            }
        }

    }
}
