using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVehicle : Vehicle
{
    [SerializeField] private ParticleSystem explosionParticle;
    private InputSystem_Actions inputActions;
    public Vector2 input;
    

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetLaneIndex();
        Speed = 20.0f;
        TurningSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {   
        if (GameManager.instance.currentState == GameState.Playing)
        {
            MoveForward();

            if (inputActions.Player.MoveLeft.triggered)
            {
                ChangeLane(-1); // Move left
            }
            else if (inputActions.Player.MoveRight.triggered)
            {
                ChangeLane(1); // Move right
            }

            UpdateLanePosition(); // Update the lane position immediately after changing lanes
        }
    }

    public override void MoveForward()
    {
        input = inputActions.Player.Move.ReadValue<Vector2>();
        
        if (input.y > 0)
        {
            if (transform.position.z < -186)
            {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                TurningSpeed = 20.0f; // Increase turning speed when accelerating
                MenuAudioManager.instance.PlayFastSound();
            }
        }
        else if (input.y == 0)
        {
            if (transform.position.z > -187)
            {
                transform.Translate(Vector3.back * Speed * Time.deltaTime);
            }
            MenuAudioManager.instance.PlayLowSound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collided with an enemy vehicle!");
            GameManager.instance.EndGame();
            
            MenuAudioManager.instance.PlayCrashSound();
            
            Instantiate(explosionParticle, transform.position, Quaternion.identity);

            Destroy(gameObject); // Destroy the player vehicle on collision
        }
    }
}
