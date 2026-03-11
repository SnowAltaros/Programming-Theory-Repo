using UnityEngine;

public class SelectVehicle : MonoBehaviour
{
    private GameObject selectedVehicle;

    [SerializeField]private GameObject[] vehicles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedVehicle = Instantiate(vehicles[0], transform.position, transform.rotation);
    }

    void Update()
    {
        if (GameManager.instance.currentState == GameState.StartingPlay)
        {
            selectedVehicle.transform.Translate(Vector3.forward * 25 * Time.deltaTime);
        }
    }
    public void SelectVehicleBYIndex(int index)
    {
        Destroy(selectedVehicle);
        selectedVehicle = Instantiate(vehicles[index], transform.position, transform.rotation);
        PlayersData.instance.playerVehicleIndex = index;
    }
}
