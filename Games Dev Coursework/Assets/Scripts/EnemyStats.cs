using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public Dictionary<string, int> stats = null;
    //public string attacktype;
    //All The stats should be loaded before any other script
    void Awake()
    {
        stats = new Dictionary<string, int>();
        stats.Add("Attack", 10);
        stats.Add("HP", 100);
        stats.Add("Defence", 5);
        stats.Add("Speed", Random.Range(10,40));
        Debug.Log("Enemy Speed Stat:" + stats["Speed"]);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
