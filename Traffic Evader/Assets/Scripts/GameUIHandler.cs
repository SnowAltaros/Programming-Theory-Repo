using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI; // Reference to the Game Over UI panel
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI playerInfoText; // Reference to the TextMeshProUGUI component for displaying distance
    [SerializeField] private GameObject topPlayerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverUI.SetActive(false); // Hide the Game Over UI panel at the start of the game
        bestScoreText.text = "Best Score: " + PlayersData.instance.topPlayers[0].distance.ToString("F0") + "m"; // Display the best score from PlayersData on the UI
        topPlayerText.SetActive(false); // Hide the "New Top Player!" text at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentState == GameState.GameOver)
        {
            gameOverUI.SetActive(true); // Show the Game Over UI panel when the game is over
            scoreUI.SetActive(false); // Hide the score UI when the game is over
            playerInfoText.text = PlayersData.instance.newPlayerName + "'s Score: " + PlayersData.instance.newPlayerScore.ToString("F0") + "m"; // Display the player's score on the Game Over UI
            if (PlayersData.instance.isInTopPlayers)
            {
                topPlayerText.SetActive(true); // Show the "New Top Player!" text if the player is in the top players list
            }
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Load the first scene (index 0) to restart the game
        GameManager.instance.StartGame(); // Set the game state to Playing when the game restarts
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // Load the main menu scene (index 1)
        GameManager.instance.currentState = GameState.MainMenu; // Set the game state to MainMenu when returning to the main menu
    }
}
