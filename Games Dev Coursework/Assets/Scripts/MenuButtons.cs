using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene("hub");
        //This is here because when you press the play button after pressing the main menu button when you are actually in game, when you load in it is frozen like you are still paused
        Time.timeScale = 1f;
    }

    public void ExitGame() 
    {

    }
}
