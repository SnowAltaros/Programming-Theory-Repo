using UnityEngine;

public class LayoutRepeating : MonoBehaviour
{
    [SerializeField] private GameObject layoutPrefab;
    private int numberOfLayouts = 2;
    private float layoutLength = 299.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -layoutLength)
        {
            transform.position += new Vector3(0, 0, layoutLength * 2);
        }
    }
}
