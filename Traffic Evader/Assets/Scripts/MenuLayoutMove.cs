using UnityEngine;

public class MenuLayoutMove : MonoBehaviour
{
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
