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
    }

    public void ExitGame() 
    {

    }
}
