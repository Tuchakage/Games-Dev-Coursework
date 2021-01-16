using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//When the player touches the trigger it gets the string in HubNameLevel and depending on whats inside of it the scene will change to that level
public class HubSceneChanger : MonoBehaviour
{
    HubNameLevel hbl;
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
        if (col.gameObject.tag == "Player") 
        {
            if (hbl.getLevelName() == "Dungeon")
            {
                SceneManager.LoadScene("test");
            }
        }

    }
}
