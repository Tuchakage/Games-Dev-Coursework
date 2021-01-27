using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//When the player touches the trigger in the Hub Level it gets the string in HubNameLevel and depending on whats inside of it the scene will change to that level
public class SceneChanger : MonoBehaviour
{
    HubNameLevel hbl;
    LevelCompleteManager lcm;
    chesttrigger ct;
    GameManager gm;
    PlayerStats ps;
    string currentscene;

    TMP_Text forgotkeytxt;
    // Start is called before the first frame update
    void Start()
    {
        hbl = GameObject.Find("GameManager").GetComponent<HubNameLevel>();
        lcm = GameObject.Find("GameManager").GetComponent<LevelCompleteManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") 
        {
            //If you are in the Hub world and the getlevelname is set to Dungeon then you will be loaded into the Dungeon level
            if (currentscene == "hub")
            {
                if (hbl.getLevelName() == "Dungeon")
                {
                    SceneManager.LoadScene("dungeon");
                }
                else if (hbl.getLevelName() == "Desert")
                {
                    SceneManager.LoadScene("desert");
                }
                else if (hbl.getLevelName() == "Bar")
                {
                    SceneManager.LoadScene("bar");
                }
                else if (hbl.getLevelName() == "FinalLevel")
                {
                    //Give the Player SP when going into the Final level
                    gm.pSP += ps.stats["SP"] / 2;
                    SceneManager.LoadScene("final");
                }
            }
            else if ((currentscene == "dungeon" || currentscene == "desert" || currentscene == "bar") && ct.keycollected) //If you are not in the hub or battle level then when you collide with this object you will go back to the hub
            {
                SceneManager.LoadScene("hub");
                //Depending on the level you are on, the levelcomplete will be set to true
                if (hbl.getLevelName() == "Dungeon")
                {
                    lcm.levelcomplete["Dungeon"] = true;
                }
                else if (hbl.getLevelName() == "Desert")
                {
                    lcm.levelcomplete["Desert"] = true;
                }
                else if (hbl.getLevelName() == "Bar")
                {
                    lcm.levelcomplete["Bar"] = true;
                }
            }
            else if (currentscene != "hub" && !ct.keycollected)
            {
                forgotkeytxt.text = "GET THE KEY FROM THE CHEST FIRST!";
            }
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (currentscene != "hub") 
        {
            forgotkeytxt.text = " ";
        }
        
    }

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
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;
        if (currentscene != "hub") 
        {
            ct = GameObject.Find("Chest").GetComponentInChildren<chesttrigger>();
            forgotkeytxt = GameObject.Find("NeedKeyText").GetComponent<TMP_Text>();
        }
    }
}
