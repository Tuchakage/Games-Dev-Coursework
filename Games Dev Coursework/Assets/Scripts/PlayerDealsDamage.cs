using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//The Purpose Of This Script is so that the Player deals damage but the reason why it is separate is because i have made it as an animation event
public class PlayerDealsDamage : MonoBehaviour
{
    EnemyHealth eh;
    BattleEnemyAI bea;
    PlayerStats ps;
    GameManager gm;
    Skills sk;
    EnemyStats es;
    ButtonHandler bh;

    GameObject enemy;
    public GameObject lightning;
    public GameObject fire;

    string currentscene;
    string attacktype;

    int playerdamage;
    // Start is called before the first frame update
    void Start()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        eh = enemy.GetComponent<EnemyHealth>();
        bea = enemy.GetComponent<BattleEnemyAI>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sk = GameObject.Find("GameManager").GetComponent<Skills>();
        es = enemy.GetComponent<EnemyStats>();
        bh = GameObject.Find("ButtonHandler").GetComponent<ButtonHandler>();
    }

    public void PlayerDealDamage() 
    {
        if (currentscene == "battle test" || currentscene == "finalbattle") 
        {
            playerdamage = ps.stats["Attack"];
            if (!bea.block)
            {
                //Enemy Takes Damage
                eh.LoseHealth(playerdamage);
                //Enemy Animation for when he gets hit plays
                bea.eanim.SetTrigger("hit");
            }
            else
            {
                //Damage To Enemy Reduced By 30%
                eh.LoseHealth(playerdamage * 30 / 100);
            }
        }
    }

    //At a certain point of the cast animation Thunder will spawn and the enemy will take Damage
    public void Skills() 
    {
        if (bh.fireused)
        {
            int firedamage = sk.skills["Fire"];
            //You lose SP When doing a Skill
            gm.pSP -= 5;

            attacktype = "Fire";
            if (!bea.block)
            {
                //Check the enemy weakness
                if (es.Weakness == attacktype)
                {
                    //Enemy Takes Double Damage
                    eh.LoseHealth(firedamage * 2);
                }
                else
                {
                    //Enemy Takes Damage
                    eh.LoseHealth(firedamage);
                }
                //Enemy Animation for when he gets hit plays
                bea.eanim.SetTrigger("hit");
            }
            else
            {
                //Damage To Enemy Reduced By 30%
                eh.LoseHealth(firedamage * 30 / 100);
            }


            GameObject fireprefab = Instantiate(fire, enemy.transform.position, Quaternion.Euler(-90, 0, 0));
            //Destroy the Fire Prefab after 2 seconds
            Destroy(fireprefab, 2);
            //Fire Skill has finished
            bh.fireused = false;
        }

        //This animation event will only be triggered if the Thunder Skill button is pressed
        else if (bh.thunderused)
        {
            int electricdamage = sk.skills["Thunder"];
            //You lose SP When doing a Skill
            gm.pSP -= 5;

            attacktype = "Electric";
            if (!bea.block)
            {
                //Check the enemy weakness
                if (es.Weakness == attacktype)
                {
                    //Enemy Takes Double Damage
                    eh.LoseHealth(electricdamage * 2);
                }
                else
                {
                    //Enemy Takes Damage
                    eh.LoseHealth(electricdamage);
                }
                //Enemy Animation for when he gets hit plays
                bea.eanim.SetTrigger("hit");
            }
            else
            {
                //Damage To Enemy Reduced By 30%
                eh.LoseHealth(electricdamage * 30 / 100);
            }

            //Spawns the Lightning in
            GameObject thunderprefab = Instantiate(lightning, enemy.transform.position, enemy.transform.rotation);
            //Thunder skill has finished
            bh.thunderused = false;
        }

    }
}
