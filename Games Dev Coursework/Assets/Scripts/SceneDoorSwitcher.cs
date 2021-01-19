using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The Purpose of this switch is that when you collide with the door the animation will be played apart from for the Desert level
public class SceneDoorSwitcher : MonoBehaviour
{
    Animator dunganim; //Animator For Dungeon Door
    Animator bardoorRanim;
    Animator bardoorLanim;
    public string doorname; //Since this is a public string you have to manually put the Door name on each object that has this script
    LevelCompleteManager lcm;

    HubNameLevel hbl;
    // Start is called before the first frame update
    void Start()
    {
        dunganim = GameObject.Find("Door").GetComponent<Animator>();
        hbl = GameObject.Find("GameManager").GetComponent<HubNameLevel>();
        bardoorLanim = GameObject.Find("bardoorL").GetComponent<Animator>();
        bardoorRanim = GameObject.Find("bardoorR").GetComponent<Animator>();
        lcm = GameObject.Find("GameManager").GetComponent<LevelCompleteManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player") 
        {
            if (Input.GetKey(KeyCode.E)) 
            {
                //If you enter the trigger and the door name is Dungeon then the dungeon door will open
                if (doorname == "Dungeon" && !lcm.levelcomplete["Dungeon"]) 
                {
                    dunganim.SetTrigger("opendoor");
                    hbl.SetLevelName(doorname);
                }               
                else if (doorname == "Bar"&& !lcm.levelcomplete["Bar"]) //If the Door name is bar then open the Bar doors
                {
                    bardoorLanim.SetTrigger("opendoor");
                    bardoorRanim.SetTrigger("opendoor");
                    hbl.SetLevelName(doorname);
                }                
            }
            if (doorname == "Desert" && !lcm.levelcomplete["Desert"]) //There is no Door you just walk into the passage way
            {
                hbl.SetLevelName(doorname);
            }
            else if (doorname == "Desert" && lcm.levelcomplete["Desert"]) 
            {
                doorname = " ";
                hbl.SetLevelName(doorname);
            }
        }
    }
}
