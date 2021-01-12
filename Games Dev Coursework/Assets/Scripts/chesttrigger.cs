using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chesttrigger : MonoBehaviour
{
    Animator anim;
    GameManager gm;

    bool keycollected = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInParent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
 
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!keycollected) 
                {
                    anim.SetBool("open", true);
                    gm.keys += 1;
                    keycollected = true;
                }

            }

        }
    }
}
