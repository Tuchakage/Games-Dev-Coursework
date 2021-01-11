using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chesttrigger : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player") 
        {

            anim.SetBool("open", true);
        }
    }
}
