using UnityEngine;


public enum GameState
{
    MainMenu,
    Playing,
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
    }

    public void EndGame()
    {
        currentState = GameState.GameOver; // Change the game state to GameOver when the game ends
        Time.timeScale = 0; // Pause the game by setting time scale to 0
    }
}
