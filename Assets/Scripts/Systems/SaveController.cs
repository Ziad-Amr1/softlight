// ./Assets/Scripts/Systems/SaveController.cs
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private InventoryController inventoryController;
    [SerializeField] private GameDatabase gameDatabase;
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = player.position,
            // mapBoundary = FindObjectOfType<CinemachineVirtualCamera>().m_BoundingShape2D.gameObject.name;    // Cinemachine
            inventory = inventoryController.Data.ExportSaveData()
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            player.position = saveData.playerPosition;
            // FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();
            inventoryController.Data.ImportSaveData(
                saveData.inventory,
                gameDatabase.ItemDatabase);
            inventoryController.RefreshUI();
        }
        else
        {
            SaveGame();
        }
    }

}
