using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHealth : MonoBehaviour
{
    GameManager gm;
    PlayerStats ps;
    // Start is called before the first frame update
    void Start()
    {
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
            if (gm.pHealth < ps.stats["HP"])
            {
                gm.pHealth = ps.stats["HP"];
            }
            else 
            {
                Debug.Log("You Dont need To Heal");
            }
            
            
        }
    }
}
