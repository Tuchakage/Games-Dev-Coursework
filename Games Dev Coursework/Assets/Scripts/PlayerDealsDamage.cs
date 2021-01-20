using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//The Purpose Of This Script is so that the Player deals damage but the reason why it is separate is because i have made it an animation event
public class PlayerDealsDamage : MonoBehaviour
{
    GameObject enemy;
    EnemyHealth eh;
    BattleEnemyAI bea;
    PlayerStats ps;

    int playerdamage;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        eh = enemy.GetComponent<EnemyHealth>();
        bea = enemy.GetComponent<BattleEnemyAI>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDealDamage() 
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
