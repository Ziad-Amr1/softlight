// Assets/Scripts/Systems/GameDatabase.cs
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemDatabase itemDatabase;

    public ItemDatabase ItemDatabase => itemDatabase;
}
