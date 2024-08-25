using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Variables
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI; //holds reference to Pause UI
    #endregion

    #region BuiltIn Functions
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //logic in the update to make sure I can pause
        {
            if (GameIsPaused) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    #endregion

    #region Custom Functions
    public void Resume() //function for resuming the game
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //plays the game at normal rate the game
        GameIsPaused = false;
    }

    void Pause() //function for pausing the game
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //freezes the game
        GameIsPaused = true;
    }

    public void MainMenuReturn() //returns to the main menu
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //needs to unload Game scene
    }

    public void QuitGame() //quits the game when called
    {
        Application.Quit(); //closes the application
    }
    #endregion
}
