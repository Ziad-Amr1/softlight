// ./Assets/Scripts/World/Resources/Ore.cs
using UnityEngine;

public class Ore : MonoBehaviour, IInteractable
{
    public Vector2 Position => transform.position;
    public void Interact()
    {
        Debug.Log("Ore");
    }
}
