// ./Assets/ScriptableObjects/TreeData.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Tree", menuName = "Data/Tree")]
public class TreeData : ScriptableObject
{
    public string displayName;
    public string description;
    public int health = 5;
    public float respawnTime = 30f;
    public List<LootEntry> lootTable = new();
}