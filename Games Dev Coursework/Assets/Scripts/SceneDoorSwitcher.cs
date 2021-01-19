using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoorSwitcher : MonoBehaviour
{
    Animator dunganim; //Animator For Dungeon Door
    Animator bardoorRanim;
    Animator bardoorLanim;
    GameObject dungeondoor;
    public string doorname;

    HubNameLevel hbl;
    // Start is called before the first frame update
    void Start()
    {
        dunganim = GameObject.Find("Door").GetComponent<Animator>();
        hbl = GameObject.Find("GameManager").GetComponent<HubNameLevel>();
        bardoorLanim = GameObject.Find("bardoorL").GetComponent<Animator>();
        bardoorRanim = GameObject.Find("bardoorR").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        Debug.Log(gameObject.name);
        if (col.gameObject.tag == "Player") 
        {
            if (Input.GetKey(KeyCode.E)) 
            {
                //If you enter the trigger and the door name is Dungeon then the dungeon door will open
                if (doorname == "Dungeon") 
                {
                    dunganim.SetTrigger("opendoor");
                }               
                else if (doorname == "Bar") //If the Door name is bar then open the Bar doors
                {
                    bardoorLanim.SetTrigger("opendoor");
                    bardoorRanim.SetTrigger("opendoor");
                }

                hbl.SetLevelName(doorname);
            }
            
        }
    }

}
