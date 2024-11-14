using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    private GameObject lastSelectedButton;

    public void ButtonClicked(GameObject button)
    {
        if (lastSelectedButton != null)
        {
            // Optionally reset the visual state of the previously selected button
            // For example: lastSelectedButton.GetComponent<Image>().color = Color.white;
        }

        // Set the new button as the last selected one
        lastSelectedButton = button;

        // Change the visual state of the selected button
        // For example: button.GetComponent<Image>().color = Color.red;
    }
}
