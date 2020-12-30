using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : MonoBehaviour
{

    public GameObject player;

    GameObject[] enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) 
    {
        Debug.Log(gameObject.name + " OnCollisionEnter()" + col.gameObject.name);
        SceneManager.LoadScene("battle test");

    }
}
