using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseCanvasAssignment : MonoBehaviour
{
    public string spoofName;        // The name of the child GameObject with the Spoof Toggle
    public string vineName;         // The name of the child GameObject with the Vine Toggle
    public string vineMasterName;   // The name of the parent GameObject for all the vines
    public string weightName;       // The name of the child GameObject with the weight input field
    public string spawnLimitName;   // The name of the child GameObject with the spawn limit input field
    public string targetScriptName; // The name of the target GameObject with the script
    public string resetName;        // The name of the target GameObject with the reset button
    public GameObject pauseController;

    // Start is called before the first frame update
    void Start()
    {
        BindSpoofToggle();
        BindVineSinkToggle();
        BindWeightInput();
        BindSpawnLimitInput();
        BindResetButton();
    }

    private void BindSpoofToggle()
    {
        // Find the child GameObject by name
        Transform childTransform = transform.Find(spoofName);
        if (childTransform != null)
        {
            // Get the Toggle component
            Toggle toggle = childTransform.GetComponent<Toggle>();
            if (toggle != null)
            {
                // Find the target GameObject with the specified script
                GameObject targetGameObject = GameObject.Find(targetScriptName);
                if (targetGameObject != null)
                {
                    // Get the target script component
                    SpawnerCodeV3 targetScript = targetGameObject.GetComponent<SpawnerCodeV3>();
                    if (targetScript != null)
                    {
                        // Bind the Toggle's onValueChanged event to the target function
                        toggle.onValueChanged.AddListener(targetScript.ToggleSpoofBool);
                        Debug.Log("Spoof Toggle assigned.");
                    }
                    else
                    {
                        Debug.LogWarning("TargetScript not found on the target GameObject.");
                    }
                }
                else
                {
                    Debug.LogWarning("Target GameObject not found.");
                }
            }
            else
            {
                Debug.LogWarning("Toggle component not found on child.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject not found.");
        }
    }

    private void BindVineSinkToggle()
    {
        // Find the child GameObject by name
        Transform childTransform = transform.Find(vineName);
        if (childTransform != null)
        {
            // Get the Toggle component
            Toggle toggle = childTransform.GetComponent<Toggle>();
            if (toggle != null)
            {
                // Find the target GameObject with the specified script
                GameObject targetGameObject = GameObject.Find(vineMasterName);
                if (targetGameObject != null)
                {
                    // Get the target BoxCollider component
                    BoxCollider targetCollider = targetGameObject.GetComponent<BoxCollider>();
                    if (targetCollider != null)
                    {
                        // Bind the Toggle's onValueChanged event to update the BoxCollider's isTrigger property
                        toggle.onValueChanged.AddListener((bool value) =>
                        {
                            targetCollider.isTrigger = value;
                            Debug.Log($"BoxCollider isTrigger set to: {value}");
                        });

                        Debug.Log("Vine Toggle assigned.");
                    }
                    else
                    {
                        Debug.LogWarning("BoxCollider not found on the target GameObject.");
                    }
                }
                else
                {
                    Debug.LogWarning("Target GameObject not found.");
                }
            }
            else
            {
                Debug.LogWarning("Toggle component not found on child.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject not found.");
        }
    }

    private void BindSpawnLimitInput()
    {
        // Find the child GameObject by name
        Transform childTransform = transform.Find(weightName);
        if (childTransform != null)
        {
            //Debug.Log("Child object " + childTransform.name + " found");
            // Get the Toggle component
            TMP_InputField input = childTransform.GetComponent<TMP_InputField>();
            if (input != null)
            {
                //Debug.Log("found input field");
                // Find the target GameObject with the specified script
                GameObject targetGameObject = GameObject.Find(targetScriptName);
                if (targetGameObject != null)
                {
                    // Get the target script component
                    SpawnerCodeV3 targetScript = targetGameObject.GetComponent<SpawnerCodeV3>();
                    if (targetScript != null)
                    {
                        // Bind the Toggle's onValueChanged event to the target function
                        input.onValueChanged.AddListener(targetScript.ChangeBlockWeight);
                        Debug.Log("Weight Input Field assigned.");
                    }
                    else
                    {
                        Debug.LogWarning("TargetScript not found on the target GameObject.");
                    }
                }
                else
                {
                    Debug.LogWarning("Target GameObject not found.");
                }
            }
            else
            {
                Debug.LogWarning("InputField component not found on child.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject not found.");
        }
    }
    private void BindWeightInput()
    {
        // Find the child GameObject by name
        Transform childTransform = transform.Find(spawnLimitName);
        if (childTransform != null)
        {
            //Debug.Log("Child object " + childTransform.name + " found");
            // Get the Toggle component
            TMP_InputField input = childTransform.GetComponent<TMP_InputField>();
            if (input != null)
            {
                //Debug.Log("found input field");
                // Find the target GameObject with the specified script
                GameObject targetGameObject = GameObject.Find(targetScriptName);
                if (targetGameObject != null)
                {
                    // Get the target script component
                    SpawnerCodeV3 targetScript = targetGameObject.GetComponent<SpawnerCodeV3>();
                    if (targetScript != null)
                    {
                        // Bind the Toggle's onValueChanged event to the target function
                        input.onValueChanged.AddListener(targetScript.ChangeSpawnLimit);
                        Debug.Log("SpawnLimit Input Field assigned.");
                    }
                    else
                    {
                        Debug.LogWarning("TargetScript not found on the target GameObject.");
                    }
                }
                else
                {
                    Debug.LogWarning("Target GameObject not found.");
                }
            }
            else
            {
                Debug.LogWarning("InputField component not found on child.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject not found.");
        }
    }

    private void BindResetButton()
    {
        // Find the child GameObject by name
        Transform childTransform = transform.Find(resetName);
        if (childTransform != null)
        {
            //Debug.Log("Child object " + childTransform.name + " found");
            // Get the Toggle component
            Button input = childTransform.GetComponent<Button>();
            if (input != null)
            {
                //Debug.Log("found button field");
                if (pauseController != null)
                {
                    //Debug.Log("Found pause controller object: " + pauseController.name);
                    // Get the target script component
                    ResetManager targetScript = pauseController.GetComponent<ResetManager>();
                    if (targetScript != null)
                    {
                        //Debug.Log("found reset manager");
                        // Bind the Button event to the target function
                        input.onClick.AddListener(targetScript.ResetScene);
                        Debug.Log("Reset field assigned.");
                    }
                    else
                    {
                        Debug.LogWarning("TargetScript not found on the target GameObject.");
                    }
                }
                else
                {
                    Debug.LogWarning("Target GameObject not found.");
                }
            }
            else
            {
                Debug.LogWarning("Button component not found on child.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject not found.");
        }
    }
}
