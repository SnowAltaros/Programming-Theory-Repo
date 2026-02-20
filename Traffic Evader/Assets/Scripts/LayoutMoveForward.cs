using UnityEngine;
using UnityEngine.InputSystem;

public class LayoutMoveForward : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float fastSpeed = 40.0f;
    private InputSystem_Actions inputActions;
    [SerializeField ]private Vector2 moveForward;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        moveForward = inputActions.Player.Move.ReadValue<Vector2>();
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if (moveForward.y > 0)
        {
            transform.Translate(Vector3.back * fastSpeed * Time.deltaTime);
        }
    }
}
