using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        //Updates the health and sp slider value to whatever value is stored in the GameManager
        phealthslider.value = gm.pHealth;
        //Setting Max Value to whatever is in the Player Stats script
        phealthslider.maxValue = ps.stats["HP"];
        spslider.value = gm.pSP;
        //Setting Max Value to whatever is in the Player Stats script
        spslider.maxValue = ps.stats["SP"];
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

        //Freeze Time in the game
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
