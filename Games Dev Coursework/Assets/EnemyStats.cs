using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public Dictionary<string, int> stats = null;
    //public string attacktype;
    void Start()
    {
        stats = new Dictionary<string, int>();
        stats.Add("Attack", 10);
        stats.Add("HP", 100);
        stats.Add("Defence", 5);
        stats.Add("Speed", Random.Range(10,40));
        Debug.Log("Speed Stat:" + stats["Speed"]);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
