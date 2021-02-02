using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//This is where the Sound Effects function will be stored, other scripts will come to this script to play the Sound Effects
public class AudioManager : MonoBehaviour
{
    AudioSource soundfx;//Audio Source For Sound Effects
    AudioSource bgm; //Audio Source For Background
    AudioSource fs; //Audio Source For FootSteps
    //Sound Effects
    public AudioClip swordswing;
    public AudioClip sandfootsteps;
    public AudioClip grassfootsteps;
    public AudioClip woodfootsteps;
    public AudioClip stonefootsteps;
    public AudioClip slash;
    public AudioClip punch;

    //Background Music
    public AudioClip desertbgm;
    public AudioClip hubbgm;
    public AudioClip dungeonbgm;
    public AudioClip barbgm;
    public AudioClip finalbgm;
    public AudioClip bossbgm;
    public AudioClip battlebgm;
    string currentscene;

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static AudioManager instance = null;

    void Awake()
    {
        if (instance)
        {
            Debug.Log("already an instance so destroying new one");
            DestroyImmediate(gameObject);
            return;

        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        //Adds OnSceneLoaded()
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        //Sets the current scene variable to the scene name
        currentscene = SceneManager.GetActiveScene().name;

        //Look for the Audio Sources for Background Music, Footsteps and Sound FX
        soundfx = GetComponent<AudioSource>();
        bgm = gameObject.transform.Find("BGM").GetComponent<AudioSource>();
        fs = gameObject.transform.Find("Footsteps").GetComponent<AudioSource>();

        //OnSceneLoad set the bgm clip to null
        bgm.clip = null;

        //Depending on the current scene different Background Music will play
        if (currentscene == "hub")
        {
            bgm.clip = hubbgm;
            bgm.Play();
        }
        else if (currentscene == "battle test") 
        {
            bgm.clip = battlebgm;
            //Change the volume Of the music for this scene
            bgm.volume = 0.7f;
            bgm.Play();
        }
        else if (currentscene == "bar")
        {
            bgm.clip = barbgm;
            //Change the volume Of the music for this scene
            bgm.volume = 0.2f;
            bgm.Play();
        }
        else if (currentscene == "dungeon")
        {
            bgm.clip = dungeonbgm;
            //Change the volume Of the music for this scene
            bgm.volume = 0.2f;
            bgm.Play();
        }
        else if (currentscene == "desert")
        {
            bgm.clip = desertbgm;
            bgm.Play();
        }
        else if (currentscene == "final")
        {
            bgm.clip = finalbgm;
            bgm.Play();
        }
        else if (currentscene == "finalbattle")
        {
            bgm.clip = bossbgm;
            bgm.Play();
        }
    }

    public void SwordSwingSound() 
    {
        soundfx.clip = swordswing;
        soundfx.Play();
    }

    //This is the function that will play the Footsteps
    public void FootSteps()
    {
        if (currentscene == "desert") //If you are in the Desert Level then the Sand Footsteps will play
        {
            fs.clip = sandfootsteps;
            fs.Play();
        }
        else if (currentscene == "bar")//If you are in the Bar Level then the Wood Footsteps will play
        {
            fs.clip = woodfootsteps;
            fs.Play();
        }
        else if (currentscene == "dungeon")//If you are in the Dungeon level then Stone Footsteps will play
        {
            fs.clip = stonefootsteps;
            fs.Play();
        }
        else if (currentscene == "hub" || currentscene == "final")//If you are in the Hub Level Or the Final level then Grass Footsteps will play
        {
            fs.clip = grassfootsteps;
            fs.Play();
        }

    }

    public void SlashSFX() 
    {
        soundfx.clip = slash;
        soundfx.Play();
    }

    public void PunchSFX() 
    {
        soundfx.clip = punch;
        soundfx.Play();
    }
}
