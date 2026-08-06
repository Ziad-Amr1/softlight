// ./Assets/Scripts/DataFormat/SaveData.cs
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    // public string mapBoundary; // for future use, if we want add map boundary
    public List<InventorySlotSaveData> inventory = new();
}
