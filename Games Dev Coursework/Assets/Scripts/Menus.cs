using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//The Purpose of this script is to have all the functions that have something to do with Menus e.g. Pause Menu, Main Menu
public class Menus : MonoBehaviour
{
    GameManager gm;
    PlayerStats ps;
    public bool isPaused = false;
    Slider phealthslider;
    Slider spslider;
    public GameObject pauseMenuUI;
    TMP_Text keyamount; //How many Keys you have

    string currentscene;
    void Start()
    {
        //Sets Current Scene variable 
        currentscene = SceneManager.GetActiveScene().name;
        //When in the roaming levels then you should look for all of the Objects inside the if statements
        if (currentscene != "MainMenu" && currentscene != "winning screen" && currentscene != "battle test" && currentscene != "finalbattle") 
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            ps = GameObject.Find("GameManager").GetComponent<PlayerStats>();
            phealthslider = GameObject.Find("PlayerHealth").GetComponent<Slider>();
            spslider = GameObject.Find("PlayerSP").GetComponent<Slider>();
            keyamount = GameObject.Find("Keys").GetComponent<TMP_Text>();
            
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (currentscene != "MainMenu" && currentscene != "winning screen" && currentscene != "battle test" && currentscene != "finalbattle") 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //When you press Escape and the game is already paused then it will be resumed
                if (isPaused)
                {
                    Resume();
                }
                else//If the Game is not paused already then it will be paused
                {
                    Pause();
                }
            }
            //The number of keys you have will be what shows up in the text
            keyamount.text = "Keys: " + gm.keys;

            //Updates the health and sp slider value to whatever value is stored in the GameManager
            phealthslider.value = gm.pHealth;
            //Setting Max Value to whatever is in the Player Stats script
            phealthslider.maxValue = ps.stats["HP"];
            spslider.value = gm.pSP;
            //Setting Max Value to whatever is in the Player Stats script
            spslider.maxValue = ps.stats["SP"];
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

        //Freeze Time in the game
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("hub");
        //This is here because when you press the play button after pressing the main menu button when you are actually in game, when you load in it is frozen like you are still paused
        Time.timeScale = 1f;
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
