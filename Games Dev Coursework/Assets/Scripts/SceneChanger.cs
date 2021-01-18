using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//When the player touches the trigger it gets the string in HubNameLevel and depending on whats inside of it the scene will change to that level
public class SceneChanger : MonoBehaviour
{
    HubNameLevel hbl;
    chesttrigger ct;
    int currentscene;

    TMP_Text forgotkeytxt;
    // Start is called before the first frame update
    void Start()
    {
        hbl = GameObject.Find("GameManager").GetComponent<HubNameLevel>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("hew");
        if (col.gameObject.tag == "Player") 
        {
            //If you are in the Hub world and the getlevelname is set to Dungeon then you will be loaded into the Dungeon level
            if (currentscene == 0)
            {
                if (hbl.getLevelName() == "Dungeon")
                {
                    SceneManager.LoadScene("dungeon");
                }
            }
            else if ((currentscene == 2 && ct.keycollected) || currentscene == 3 && ct.keycollected) //If you are in the Dungeons Level
            {
                SceneManager.LoadScene("hub");
            }
            else if (currentscene != 0 && !ct.keycollected)
            {
                forgotkeytxt.text = "GET THE KEY FROM THE CHEST FIRST!";
            }
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (currentscene != 0) 
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
        currentscene = SceneManager.GetActiveScene().buildIndex;
        if (currentscene != 0) 
        {
            ct = GameObject.Find("Chest").GetComponentInChildren<chesttrigger>();
            forgotkeytxt = GameObject.Find("NeedKeyText").GetComponent<TMP_Text>();
        }
    }
}
