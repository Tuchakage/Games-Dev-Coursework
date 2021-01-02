using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update

    public Dictionary<string, int> stats = null;
    //public string attacktype;
    void Start()
    {
        stats = new Dictionary<string, int>();
        stats.Add("Attack", 10);
        stats.Add("HP", 100);
        stats.Add("Defence", 20);
        stats.Add("Speed", 30);
        stats.Add("SP", 50);
        //Debug.Log("Attack Stat:" + stats["Attack"]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
