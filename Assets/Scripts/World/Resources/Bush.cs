// ./Assets/Scripts/World/Resources/Bush.cs
using UnityEngine;

public class Bush : MonoBehaviour, IInteractable
{
    public Vector2 Position => transform.position;
    public void Interact()
    {
        Debug.Log("Bush");
    }
}
