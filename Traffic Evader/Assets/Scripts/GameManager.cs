using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    MainMenu,
    Playing,
    StartingPlay,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentState;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the instance to this GameManager
            DontDestroyOnLoad(gameObject); // Make sure the GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate GameManager instances
        }
    }

    private void Start()
    {
        currentState = GameState.MainMenu; // Set the initial game state to MainMenu
    }

    public void StartGame()
    {
        currentState = GameState.Playing; // Change the game state to Playing when the game starts
        SceneManager.LoadScene(0);
        Time.timeScale = 1; // Ensure the game is running at normal speed when it starts
    }

    public void EndGame()
    {
        StartCoroutine(WaitSecond());
        Time.timeScale = 0; // Pause the game by setting time scale to 0
        PlayersData.instance.SavePlayerData(PlayersData.instance.newPlayerName, PlayersData.instance.newPlayerScore); // Save the player's data when the game ends
    }

    public void StartingGame()
    {
        currentState = GameState.StartingPlay;
        StartCoroutine(CountFiveSecconds());
    }

    IEnumerator CountFiveSecconds()
    {
        yield return new WaitForSeconds(5);
        StartGame();
    }

     IEnumerator WaitSecond()
    {
        yield return new WaitForSecondsRealtime(1);
        currentState = GameState.GameOver; // Change the game state to GameOver when the game ends
        Debug.Log(currentState);
    }
}
