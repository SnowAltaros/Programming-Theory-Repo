using UnityEngine;

public class EnemyVehicle : Vehicle
{
    private float destroyZPosition = -220.0f; // Z position at which the enemy vehicle will be destroyed
    private bool hasChangedLane = false; // Flag to track if the enemy vehicle has already changed lanes
    private DifficultyManager difficultyManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        difficultyManager = GameObject.FindAnyObjectByType<DifficultyManager>();
        if (difficultyManager != null)
        {
            Speed = difficultyManager.enemySpeed;
            TurningSpeed = difficultyManager.enemyTurningSpeed;
        }
        GetLaneIndex();
        transform.Rotate(0, 180, 0); // Rotate the enemy vehicle to face the player
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();

        if (transform.position.z < -120.0f && !hasChangedLane)
        {
            RandomChangeLane();
        }
        UpdateLanePosition();
        DestroyGameObject();
        
    }

    private void DestroyGameObject()
    {
        if (transform.position.z < destroyZPosition)
        {
            Destroy(gameObject);
        }
    }

    private void RandomChangeLane()
    {
        float randomValue = Random.value; // Get a random value between 0 and 1
        float laneChangeProbability = 0.3f; // Set the probability for changing lanes - 30%
        if (randomValue < laneChangeProbability)
        {
            int direction = Random.Range(-1, 2);
            ChangeLane(direction);
        }

        hasChangedLane = true; // Set the flag to indicate that the enemy vehicle has changed lanes
    }
}
