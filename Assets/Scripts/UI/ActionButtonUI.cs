using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Controls the UI for our Action Button
public class ActionButtonUI : MonoBehaviour
{
    // Reference to the text on the button
    [SerializeField] private TextMeshProUGUI textMeshPro;
    // Reference to the Button that this is attached to
    [SerializeField] private Button button;
    // Reference to the border for when a button is selected
    [SerializeField] private GameObject selectedGameObject;

    // Keeps track of the action attached to this button
    private BaseAction baseAction;

    //* Sets the UI elements for the button
    public void SetBaseAction(BaseAction baseAction)
    {
        this.baseAction = baseAction;
        textMeshPro.text = baseAction.GetActionName().ToUpper();

        // Creating the function of the button when it is generated
        button.onClick.AddListener(() => {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }

    //* Updates the border visual according to if the button was selected or deselected
    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedGameObject.SetActive(selectedBaseAction == baseAction);
    }
}
