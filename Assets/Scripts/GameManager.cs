using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager Instance { get; private set; } //getter and setter for my manager

    public GameObject[] players; //a list of players
    #endregion

    #region BuiltIn Functions
    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject); //makes sure there is only one game manager
        }
        else
        {
            Instance = this; //sets the game manager
        }
    }

    private void OnDestroy() //unsets the game manager
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); //finds all the players
    }
    #endregion

    #region Custom Functions
    public void CheckWinState()
    {
        int aliveCount = 0;

        //this goes through and checks to see who is alive and who is dead
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].activeSelf)
            {
                aliveCount++;
            }
        }

        if (aliveCount <= 1)
        {
            Invoke(nameof(NewRound), 3f); //calls the new round when only one person is alive
        }
    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reloads the seen when one player is left alive
    }
    #endregion
}
