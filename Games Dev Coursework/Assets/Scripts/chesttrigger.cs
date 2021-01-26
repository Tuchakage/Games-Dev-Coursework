using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chesttrigger : MonoBehaviour
{
    Animator anim;
    GameManager gm;
    Skills sk;
    chestlist cl;
    public string chesttype;
    public string skill; // Whatever skill is assigned to the chest is the skill you will unlock
    public int chestid;

    public bool keycollected = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInParent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sk = GameObject.Find("GameManager").GetComponent<Skills>();
        cl = GameObject.Find("GameManager").GetComponent<chestlist>();
        //If The Chest list is not empty
        if (cl.chests != null) 
        {
            //Check if the id of the chest is set to true
            if (cl.chests[chestid] == true)
            {
                //If it is true then the chest will stay open
                anim.SetBool("open", true);
            }
            else 
            {
                Debug.Log("This Chest hasnt been opened " + chestid);
            }
        }
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
                    //Gives the chestlist script the id of the chest being opened and sets the bool to true which is added to the dictionary
                    cl.AddChest(chestid, true);
                }

            }

        }
    }
}
