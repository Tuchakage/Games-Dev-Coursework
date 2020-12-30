using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public Slider ehealthslider;

    public float ehealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        ehealthslider.value = ehealth;
    }

    // Update is called once per frame
    void Update()
    {
        ehealthslider.value = ehealth;

        if (ehealth <= 0) 
        {
            Destroy(gameObject);
        }
    }

    public void LoseHealth(float playerdamage)
    {
        ehealth -= playerdamage;
    }


}
