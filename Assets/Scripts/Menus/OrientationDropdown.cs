using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrientationDropdown : MonoBehaviour
{
    public Dropdown dropdown;



    void Start()
    {
        ArduinoGetter.SetOrientation(ArduinoGetter.Orientation.Left);

        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });
       
    }

    void DropdownValueChanged(Dropdown change)
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

}
