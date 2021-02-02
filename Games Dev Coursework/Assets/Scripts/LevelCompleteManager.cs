using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public Dictionary<string, bool> levelcomplete = null;

    // Start is called before the first frame update
    void Start()
    {
        levelcomplete = new Dictionary<string, bool>();
        levelcomplete.Add("Dungeon", false);
        levelcomplete.Add("Desert", false);
        levelcomplete.Add("Bar", false);
    }
}
