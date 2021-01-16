using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoorSwitcher : MonoBehaviour
{
    Animator anim;
    GameObject dungeondoor;
    public string doorname;

    HubNameLevel hbl;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Door").GetComponent<Animator>();
        hbl = GameObject.Find("GameManager").GetComponent<HubNameLevel>();
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
                anim.SetTrigger("opendoor");
                hbl.SetLevelName(doorname);
            }
            
        }
    }

}
