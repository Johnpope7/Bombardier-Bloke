using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Custom Functions
    public void PlayGame() //starts the game when called
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene in the build index
    }

    public void QuitGame() //quits the game when called
    {
        Application.Quit(); //closes the application
    }
    #endregion
}
