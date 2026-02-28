using UnityEngine;

public class SpawnPlayerVehicle : MonoBehaviour
{
    [SerializeField] private GameObject[] vehicles;
    private GameObject vehicle;
    private Vector3 targetPosition = new Vector3(0, 0, -187);
    private bool isOnPlace;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        int index = PlayersData.instance.playerVehicleIndex;
        vehicle = Instantiate(vehicles[index], transform.position, transform.rotation);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlace == false)
        {
            vehicle.transform.position = Vector3.MoveTowards(vehicle.transform.position, targetPosition, 20 * Time.deltaTime);
            if (vehicle.transform.position == targetPosition)
            {
                isOnPlace = true;
            }
        }
    }
}
