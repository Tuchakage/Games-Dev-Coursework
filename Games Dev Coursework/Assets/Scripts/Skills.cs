using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public Dictionary<string, int> skills = null;
    public bool thunderunlock = false; //The player can use this skill once it has been unlocked
    public int firedmg = 15;
    public int elecdmg = 20;
    //public string attacktype;
    // Start is called before the first frame update
    void Awake()
    {
        skills = new Dictionary<string, int>();
        skills.Add("Fire", firedmg);
        skills.Add("Thunder", elecdmg);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool UnlockThunderSkill() 
    {
        thunderunlock = true;
        return thunderunlock;
    }
}
