using UnityEngine;
using UnityEngine.InputSystem;

public class LayoutMoveForward : MonoBehaviour
{
    private PlayerVehicle playerVehicle;
    private float speed = 20.0f;
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
    private float fastSpeed = 40.0f;
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
    }

    void MoveForward()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (playerVehicle != null && playerVehicle.input.y > 0)
        {
            transform.Translate(Vector3.back * fastSpeed * Time.deltaTime);
        }
    }
}
