using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{

    public Dropdown dropdown;  // Your dropdown component
    private static int savedDropdownValue = -1;  // Static variable to store value

    private void Start()
    {
        // If there's a saved value, use it to set the dropdown
        if (savedDropdownValue != -1)
        {
            dropdown.value = savedDropdownValue;
        }

        // Add listener to update the static variable when the value changes
        dropdown.onValueChanged.AddListener(delegate { SaveDropdownValue(); });
    }

    public void SaveDropdownValue()
    {
        // Store the dropdown's current value in a static variable
        savedDropdownValue = dropdown.value;
    }

}

