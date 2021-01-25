using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chesttrigger : MonoBehaviour
{
    Animator anim;
    GameManager gm;
    Skills sk;
    public string chesttype;
    public string skill; // Whatever skill is assigned to the chest is the skill you will unlock

    public bool keycollected = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInParent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sk = GameObject.Find("GameManager").GetComponent<Skills>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!keycollected && chesttype == "key") //If this is a chest that contains the key and you haven't collected the key yet then this statement will run
                {
                    anim.SetBool("open", true);
                    gm.keys += 1;
                    keycollected = true;
                }
                else if (chesttype == "skill") //If the chest is a chest that contains a skill this statement will run
                {
                    anim.SetBool("open", true);
                    sk.UnlockThunderSkill();
                }

            }

        }
    }
}
