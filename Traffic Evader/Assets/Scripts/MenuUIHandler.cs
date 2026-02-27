using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;



#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInputField; // Reference to the InputField for player name input
    [SerializeField] private GameObject bestScoresPanel;
    [SerializeField] private TextMeshProUGUI bestScoresText; // Reference to the TextMeshProUGUI component for displaying best scores

    private bool CheckEmptyInput()
    {
        if (string.IsNullOrEmpty(playerNameInputField.text) || playerNameInputField.text.Trim() == "")
        {
            playerNameInputField.image.color = Color.red; // Change the InputField background color to red to indicate an error
            return false;
        }
        else
        {
            playerNameInputField.image.color = Color.white; // Reset the InputField background color to white if the input is valid
            return true;
        }
    }
    public void StartGame()
    {
        if (CheckEmptyInput())
        {
            PlayersData.instance.newPlayerName = playerNameInputField.text; // Set the new player name in PlayersData to the input from the InputField
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Load the first scene (index 0) to start the game
            GameManager.instance.StartGame(); // Set the game state to Playing when the game starts
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stop play mode in the Unity Editor
#else
        Application.Quit(); // Quit the application in a built version
#endif
    }

    public void ShowBestScores()
    {
        bestScoresPanel.SetActive(true); // Show the best scores panel
        bestScoresText.text = "Best Scores:\n"; // Initialize the best scores text

        // Loop through the top players and append their names and scores to the best scores text
        foreach (PlayerEntry player in PlayersData.instance.topPlayers)
        {
            bestScoresText.text += player.playerName + ": " + player.distance.ToString("F0") + "m\n";
        }
    }

    public void HideBestScores()
    {
        bestScoresPanel.SetActive(false); // Hide the best scores panel
    }
}
