using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public Dictionary<string, int> stats = null;

    public int ehp;
    public int esp;
    //public string attacktype;
    //All The stats should be loaded before any other script
    void Awake()
    {
        stats = new Dictionary<string, int>();
        stats.Add("Attack", 10);
        stats.Add("HP", ehp);
        stats.Add("Defence", 5);
        stats.Add("Speed", Random.Range(10,40));
        stats.Add("SP", esp);
        Debug.Log("Enemy Speed Stat:" + stats["Speed"]);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
