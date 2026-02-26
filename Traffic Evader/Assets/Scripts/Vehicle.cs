using System;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private int currentLane;
    [SerializeField]private float speed;
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

    [SerializeField]private float turningSpeed;
    public float TurningSpeed
    {
        get { return turningSpeed; }
        set
        {
            if (value < 0)
            {
                Debug.LogWarning("Turning speed cannot be negative. Setting turning speed to 0.");
                turningSpeed = 0;
            }
            else
            {
                turningSpeed = value;
            }
        }
    }
    

    public virtual void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void MoveBackward()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    public void GetLaneIndex()
    {
        switch (transform.position.x)
        {
            case -10.0f:
                currentLane = 0; // Left lane
                break;
            case 0.0f:
                currentLane = 1; // Middle lane
                break;
            case 10.0f:
                currentLane = 2; // Right lane
                break;
            default:
                Debug.LogWarning("Vehicle is not in a valid lane. Position: " + transform.position);
                break;
        }
    }

    public void ChangeLane(int dirrection)
    {
        // Math.Clamp(value, min, max) function to prevent the vehicle from moving outside of the lanes
        currentLane = Math.Clamp(currentLane + dirrection, 0, 2);
    }

    public void UpdateLanePosition()
    {
        float xPosition = (currentLane - 1) * 10.0f; // Calculate x position based on lane index
        Vector3 targetPosition = new Vector3(xPosition, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, turningSpeed * Time.deltaTime);
    }

}
