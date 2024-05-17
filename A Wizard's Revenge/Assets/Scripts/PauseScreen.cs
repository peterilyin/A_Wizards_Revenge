using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//we need to access other scenes
using UnityEngine.SceneManagement;
//we need access to the event system
using UnityEngine.EventSystems;

public class PauseScreen : MonoBehaviour
{
    //we want the string names of other levels
    //we want to be able to turn the pause image on and off
    //this image holds our buttons as well
    public string levelSelect;
    public string mainMenu;
    public GameObject pauseScreen;

    //we want to select menu options with the mouse on the pause screen
    public GameObject pauseScreenButton;

    //we need to know the current lives and games from the level manager
    private LevelManager levelManager;

    //we want the Player to access
    public PlayerController player;


    void Start()
    {
        //we need to get the level manager
        levelManager = FindObjectOfType<LevelManager>();

        //need to get the player
        player = FindObjectOfType<PlayerController>();
    }


    //void Update()
    //{
        //we want to test if the player presses the pause button
        //we will make it the escape key
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
            //we can delete the set active in here as it is in our pause method
            //we test to see if we are paused or not in time scale of 0 means we are
            //paused
    //        if (Time.timeScale == 0)
    //        {
    //            ResumeGame();
    //        }
    //        else
    //        {
    //            PauseGame();
    //        }
    //    }
    //}

    //we want out methods to link our buttons
    public void ResumeGame()
    {
        

        //we need to make time go back to normal speed
        Time.timeScale = 1f;

        //we need to allow the player to move
        //is the player being in the air when paused they will
        //fall straight down they do not kepe their momentum
        player.canMove = true;

        //we want to start the music again
        //levelManager.levelMusic.UnPause();

        //when the player wants to resume we should turn off the screen
        pauseScreen.SetActive(false);
    }

    public void RestartLevel()
    {
        //we want to load the level select
        //we want to keep our gem count and lives
        //PlayerPrefs.SetInt("GemAmount", levelManager.gemAmount);
        //PlayerPrefs.SetInt("PlayerLives", levelManager.currentLivesCount);

        //before we leave, we need to restart the time scale back to normal time
        Time.timeScale = 1f;

        //we want to load up the level select scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuLoad()
    {
        //again, before we leave we need to restart the time scale back to normal time
        Time.timeScale = 1f;
        //want to load up the main menu when selected
        SceneManager.LoadScene(mainMenu);
    }

    //we want a method that handles pausing the game for us
    public void PauseGame()
    {
        //we should get the current event system
        //and make the current object the pause screen button
        

        //the easiest way to "freeze" everything when paused 
        //is to change the Time scale of the game to 0
        Time.timeScale = 0;

        pauseScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(pauseScreenButton);
        //we want the player movement to stop so they
        //don't have extra momentum when unpausing
        player.canMove = false;

        //we should pause the music too
        //levelManager.levelMusic.Pause();
    }
}