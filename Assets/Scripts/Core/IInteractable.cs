// ./Assets/Scripts/Core/IInteractable.cs
using UnityEngine;

public interface IInteractable
{
    Vector2 Position { get; }
    void Interact();
}