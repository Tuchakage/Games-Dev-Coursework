using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public Dictionary<string, int> skills = null;
    public int firedmg = 15;
    //public string attacktype;
    // Start is called before the first frame update
    void Awake()
    {
        skills = new Dictionary<string, int>();
        skills.Add("Fire", firedmg);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
