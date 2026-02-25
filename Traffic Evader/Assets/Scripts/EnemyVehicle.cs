using UnityEngine;

public class EnemyVehicle : Vehicle
{
    private float destroyZPosition = -220.0f; // Z position at which the enemy vehicle will be destroyed
    private bool hasChangedLane = false; // Flag to track if the enemy vehicle has already changed lanes
    private PlayerVehicle playerVehicle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerVehicle = GameObject.FindAnyObjectByType<PlayerVehicle>();
        Speed = 50.0f;
        TurningSpeed = 10.0f;
        GetLaneIndex();
        transform.Rotate(0, 180, 0); // Rotate the enemy vehicle to face the player
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        GetFastSpeed();

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

    void GetFastSpeed()
    {
        if(playerVehicle != null && playerVehicle.input.y > 0)
        {
            Speed = 100.0f; // Increase speed when the player is accelerating
            TurningSpeed = 20.0f; // Increase turning speed when the player is accelerating
        }
        else
        {
            Speed = 50.0f; // Reset to normal speed when the player is not accelerating
            TurningSpeed = 10.0f; // Reset to normal turning speed when the player is not accelerating
        }
    }
}
