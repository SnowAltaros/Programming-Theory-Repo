using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PlayersData : MonoBehaviour
{
    public static PlayersData instance;
    public List<PlayerEntry> topPlayers = new List<PlayerEntry>();
    public string newPlayerName;
    public float newPlayerScore;
    public bool isInTopPlayers = false;
    public int playerVehicleIndex;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayersData();
    }

    public void SavePlayerData(string playerName, float score)
    {
        PlayerEntry newPlayer = new PlayerEntry();
        newPlayer.playerName = playerName;
        newPlayer.distance = score; // Convert the float score to a string before saving

        topPlayers.Sort((x, y) => y.distance.CompareTo(x.distance));

        if (topPlayers.Count < 5)
        {
            topPlayers.Add(newPlayer);
            isInTopPlayers = true;
        }
        else
        {
            for (int i = 0; i < topPlayers.Count; i++)
            {
                if (score > topPlayers[i].distance)
                {
                    topPlayers.Insert(i, newPlayer);
                    topPlayers.RemoveAt(topPlayers.Count - 1);
                    
                    isInTopPlayers = true;
                    break;
                }
            }
        }

        topPlayers.Sort((x, y) => y.distance.CompareTo(x.distance));

        SavePlayersData();
    }

    public void SavePlayersData()
    {
        SaveTopPlayersData data = new SaveTopPlayersData();
        data.topPlayers = topPlayers;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/save-players-file.json", json);
    }

    public void LoadPlayersData()
    {
        string path = Application.persistentDataPath + "/save-players-file.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveTopPlayersData data = JsonUtility.FromJson<SaveTopPlayersData>(json);

            topPlayers = data.topPlayers;
        }
    }
    
}

[System.Serializable]
public class PlayerEntry
{
    public string playerName;
    public float distance;
}

[System.Serializable]
public class SaveTopPlayersData
{
    public List<PlayerEntry> topPlayers = new List<PlayerEntry>();
}
