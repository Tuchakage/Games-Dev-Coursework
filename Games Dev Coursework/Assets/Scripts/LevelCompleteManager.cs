using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public Dictionary<string, bool> levelcomplete = null;
    HubNameLevel hbl;
    // Start is called before the first frame update
    void Start()
    {
        hbl = GameObject.Find("GameManager").GetComponent<HubNameLevel>();
        levelcomplete = new Dictionary<string, bool>();
        levelcomplete.Add("Dungeon", false);
        levelcomplete.Add("Desert", false);
        levelcomplete.Add("Bar", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
