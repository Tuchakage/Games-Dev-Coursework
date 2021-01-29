using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    GameManager gm;
    PlayerStats ps;
    public bool isPaused = false;
    public Slider phealthslider;
    public Slider spslider;
    public GameObject pauseMenuUI;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused) 
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        //The Game will resume back to normal speed
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause() 
    {
        pauseMenuUI.SetActive(true);
        //Look for the Health and SP Sliders when paused
        phealthslider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
        spslider = GameObject.Find("PlayerSP").GetComponent<Slider>();

        //Updates the health and sp slider value to whatever value is stored in the GameManager
        phealthslider.value = gm.pHealth;
        //Setting Max Value to whatever is in the Player Stats script
        phealthslider.maxValue = ps.stats["HP"];
        spslider.value = gm.pSP;
        //Setting Max Value to whatever is in the Player Stats script
        spslider.maxValue = ps.stats["SP"];
        //Freeze Time in the game
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
