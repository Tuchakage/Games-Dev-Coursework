using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    EnemyHealth eh;

    //Disabling the player UI
    public GameObject pui;
    private void Start()
    {
        eh = GameObject.Find("Enemy").GetComponent<EnemyHealth>();
    }
    public void attackButton() 
    {
        Debug.Log("Attack Button");
        //Everytime i press button take away -10
        eh.LoseHealth(10);

        //pui.SetActive(false);
    }
}
