using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuUIHandler : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Load the first scene (index 0) to start the game
        GameManager.instance.StartGame(); // Set the game state to Playing when the game starts
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stop play mode in the Unity Editor
#else
        Application.Quit(); // Quit the application in a built version
#endif
    }
}
