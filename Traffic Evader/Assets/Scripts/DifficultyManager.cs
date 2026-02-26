using System.Collections;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private PlayerVehicle playerVehicle;
    private LayoutMoveForward[] layoutMoveForward;
    private SpawnManager spawnManager;
    private float speedIncreaseInterval = 10.0f; // Time interval in seconds for increasing speed
    private float speedIncreaseAmount = 5.0f; // Amount to increase speed
    private float turningSpeedIncreaseAmount = 1.0f; // Amount to increase turning speed
    public float enemySpeed = 50.0f;
    public float enemyTurningSpeed = 10.0f;
    public float decreaseSpawnIntervalAmount = 0.1f; // Amount to decrease spawn interval
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerVehicle = GameObject.FindAnyObjectByType<PlayerVehicle>();
        layoutMoveForward = GameObject.FindObjectsByType<LayoutMoveForward>(FindObjectsSortMode.None);
        spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        StartCoroutine(IncreaseDifficulty());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentState == GameState.GameOver)
        {
            StopAllCoroutines(); // Stop the difficulty increase when the game is not in the playing state
        }
    }

    IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            if (playerVehicle != null)
            {
                playerVehicle.TurningSpeed += turningSpeedIncreaseAmount;
            }
            if (layoutMoveForward != null)
            {
                foreach (var layout in layoutMoveForward)
                {
                    layout.Speed += speedIncreaseAmount;
                    layout.FastSpeed += speedIncreaseAmount;
                }
            }
            enemySpeed += speedIncreaseAmount;
            enemyTurningSpeed += turningSpeedIncreaseAmount;
            
            if (spawnManager != null)
            {
                if (spawnManager.spawnInterval > 0.5f)
                {
                    spawnManager.spawnInterval -= decreaseSpawnIntervalAmount;
                }
            }
        }
    }
}
