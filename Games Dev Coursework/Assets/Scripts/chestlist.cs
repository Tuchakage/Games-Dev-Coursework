using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The Purpose of this script is so that when a chest is open when you come out of the battle scene the chest will appear open still
public class chestlist : MonoBehaviour
{
    public Dictionary<int, bool> chests = null;
    // Start is called before the first frame update
    void Start()
    {
        chests = new Dictionary<int, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (KeyValuePair<int, bool> pair in chests) 
        //{
        //    Debug.Log("id " + pair.Key +" has value " + pair.Value);
        //} 

    }

    //This will be used in the chesttrigger script, it adds the chest id and if it was opened to the Dictionary
    public void AddChest(int id, bool isopen) 
    {
        chests.Add(id, isopen);
    }
}
