// ./Assets/Scripts/World/Resources/Tree.cs
using UnityEngine;

public class Tree : MonoBehaviour, IInteractable
{
    public Vector2 Position => transform.position;
    public void Interact()
    {
        // هنا ضع الكود الذي يحدث عند التفاعل مع الشجرة
        Debug.Log("Interacted with the tree!");
    }
}