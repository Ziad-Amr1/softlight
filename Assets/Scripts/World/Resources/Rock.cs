// ./Assets/Scripts/World/Resources/Rock.cs
using UnityEngine;

public class Rock : MonoBehaviour, IInteractable
{
    public Vector2 Position => transform.position;
    public void Interact()
    {
        Debug.Log("Rock");
    }
}
