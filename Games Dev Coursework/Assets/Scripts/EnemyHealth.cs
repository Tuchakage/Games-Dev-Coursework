using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    GameManager gm;
    EnemyStats es;
    BattleLevelChanger blc;

    public Slider ehealthslider;
    private int ehealth;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        es = GetComponent<EnemyStats>();
        ehealthslider = GameObject.Find("EnemyHealth").GetComponent<Slider>();
        blc = GameObject.Find("GameManager").GetComponent<BattleLevelChanger>();
        //ehealth is set to the value in Enemy Stats for HP
        ehealth = es.stats["HP"];
        ehealthslider.maxValue = ehealth;
    }

    // Update is called once per frame
    void Update()
    {
        ehealthslider.value = ehealth;
        
        

        //When Enemy Health is 0 or below 0 then the Enemy Object will be destroyed battleend will be set to true and you will be put back to the scene before the battle
        if (ehealth <= 0) 
        {
            Destroy(gameObject);
            gm.battleend = true;
            //These If Statements make it so that when you press the Escape button depending on the scene you were just in, it will spawn you back in
            if (blc.GetLevelName() == "Dungeon")
            {
                SceneManager.LoadScene("dungeon");
            }
            else if (blc.GetLevelName() == "Desert")
            {
                SceneManager.LoadScene("desert");
            }
            else if (blc.GetLevelName() == "Bar")
            {
                SceneManager.LoadScene("bar");
            }
        }
    }

    public void LoseHealth(int playerdamage)
    {
        ehealth -= playerdamage;
    }


}
