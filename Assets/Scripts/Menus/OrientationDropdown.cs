using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrientationDropdown : MonoBehaviour
{
    public Dropdown dropdown;
    public SpawnerRotationManager spawner;


    void Start()
    {
        if (spawner == null)
        {
            ArduinoGetter.SetOrientation(ArduinoGetter.Orientation.Left);

            dropdown.onValueChanged.AddListener(delegate
            {
                DropdownValueChanged(dropdown);
            });
        }
        else
        {
            ArduinoGetter.Orientation storedOrientation = ResetManager.ApplyStoredRotation(); // Assuming this method exists
            ArduinoGetter.SetOrientation(storedOrientation);
            spawner.SetOrientation(storedOrientation);
        }
    }

    public void DropdownValueChanged(Dropdown change)
    {
        switch (change.value)
        {
            case 0:
                ArduinoGetter.SetOrientation(ArduinoGetter.Orientation.Left);
                break;
            case 1:
                ArduinoGetter.SetOrientation(ArduinoGetter.Orientation.Up);
                break;
            case 2:
                ArduinoGetter.SetOrientation(ArduinoGetter.Orientation.Right);
                break;
            case 3:
                ArduinoGetter.SetOrientation(ArduinoGetter.Orientation.Down);
                break;

        }
    }
    void SetInitialOrientation()
    {
        // Set the initial value of the dropdown based on ArduinoGetter's orientation
        ArduinoGetter.Orientation currentOrientation = ArduinoGetter.GetOrientation();
        switch (currentOrientation)
        {
            case ArduinoGetter.Orientation.Left:
                dropdown.value = 0;
                break;
            case ArduinoGetter.Orientation.Up:
                dropdown.value = 1;
                break;
            case ArduinoGetter.Orientation.Right:
                dropdown.value = 2;
                break;
            case ArduinoGetter.Orientation.Down:
                dropdown.value = 3;
                break;
        }
    }

}
