using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    GameManager gm;
    EnemyStats es;

    public Slider ehealthslider;
    public int ehealth;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        es = GameObject.Find("GameManager").GetComponent<EnemyStats>();

        //ehealth is set to the value in Enemy Stats for HP
        ehealth = es.stats["HP"];
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
            SceneManager.LoadScene("test");
        }
    }

    public void LoseHealth(int playerdamage)
    {
        ehealth -= playerdamage;
    }


}
