using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This holds the level name which depends on the door you open in the hub world
public class HubNameLevel : MonoBehaviour
{
    string levelname;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getLevelName() 
    {
        return levelname;
    }
    public void SetLevelName(string name)
    {
        levelname = name;
    }
}
