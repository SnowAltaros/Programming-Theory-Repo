using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyVehiclePrefab;
    public float spawnInterval = 1.5f; // Time interval between spawns
    private float spawnDelay = 1.0f; // Initial delay before the first spawn
    private float spawnZ = -100.0f; // Z position where enemy vehicles will spawn
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemyVehicles()); // Start the coroutine to spawn enemy vehicles
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemyVehicle()
    {
        int laneIndex = Random.Range(0, 3); // Randomly select a lane (0, 1, or 2)
        float spawnX = -10.0f + laneIndex * 10.0f; // Calculate the X position based on the lane index
        Vector3 spawnPosition = new Vector3(spawnX, 0, spawnZ); // Set the spawn position (Z is set to 200 for spawning ahead of the player)
        int vehicleIndex = Random.Range(0, enemyVehiclePrefab.Length); // Randomly select an enemy vehicle prefab
        Instantiate(enemyVehiclePrefab[vehicleIndex], spawnPosition, Quaternion.identity); // Spawn the enemy vehicle at the calculated position
    }

    IEnumerator SpawnEnemyVehicles()
    {
        yield return new WaitForSeconds(spawnDelay); // Wait for the initial delay before starting to spawn
        while (true)
        {
            SpawnEnemyVehicle(); // Spawn an enemy vehicle
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before spawning the next vehicle
        }
    }
}