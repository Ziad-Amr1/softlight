// ./Assets/Scripts/UI/TabController.cs
// This script is used to control the tabs in the menu
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages; // Array to hold references to the tab images
    public GameObject[] pages; // Array to hold references to the pages
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(0); // Activate the first tab by default
    }

    public void SwitchTab(int tabIndex)
    {
        // Set the active state of the tab images
        for (int i = 0; i < tabImages.Length; i++)
        {
            tabImages[i].gameObject.SetActive(false);
        }
        tabImages[tabIndex].gameObject.SetActive(true);

        // Set the active state of the pages
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        pages[tabIndex].SetActive(true);
    }

    public void ActivateTab(int tabIndex)
    {
        // Set the active state of the tab images
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.gray; // Set inactive tab color to gray
        }
        pages[tabIndex].SetActive(true);
        tabImages[tabIndex].color = Color.white; // Set active tab color to white
    }
}
