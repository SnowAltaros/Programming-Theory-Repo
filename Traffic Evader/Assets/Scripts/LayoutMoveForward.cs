using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LayoutMoveForward : MonoBehaviour
{
    private PlayerVehicle playerVehicle;

    [SerializeField] private TextMeshProUGUI distanceText; // Reference to the TextMeshProUGUI component for displaying distance

    private float distanceTraveled = 0.0f; // Variable to track the distance traveled by the player
    private float speed = 40.0f;
    public float Speed
    {
        get { return speed; }
        set
        {
            if (value < 0)
            {
                Debug.LogWarning("Speed cannot be negative. Setting speed to 0.");
                speed = 0;
            }
            else
            {
                speed = value;
            }
        }
    }
    private float fastSpeed = 50.0f;
     public float FastSpeed
    {
        get { return fastSpeed; }
        set
        {
            if (value < speed)
            {
                Debug.LogWarning("Fast speed cannot be less than speed. Setting fast speed to: " + speed);
                fastSpeed = speed;
            }
            else
            {
                fastSpeed = value;
            }
        }
    }
    void Start()
    {
        playerVehicle = GameObject.FindAnyObjectByType<PlayerVehicle>();
    }
    // Update is called once per frame
    void Update()
    {
        MoveForward();
        distanceText.text = "Distance: " + distanceTraveled.ToString("F0") + "m"; // Update the distance text UI element
        PlayersData.instance.newPlayerScore = distanceTraveled; // Update the new player score in PlayersData with the distance traveled
    }

    void MoveForward()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        distanceTraveled += (speed * Time.deltaTime) / 10.0f; // Update the distance traveled based on the speed and time
        if (playerVehicle != null && playerVehicle.input.y > 0)
        {
            transform.Translate(Vector3.back * fastSpeed * Time.deltaTime);
            distanceTraveled += (fastSpeed * Time.deltaTime) / 10.0f; // Update the distance traveled based on the fast speed and time
        }
    }
}
