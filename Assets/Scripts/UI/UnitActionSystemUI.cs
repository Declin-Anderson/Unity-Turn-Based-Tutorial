using System;
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

//* Controls the UI for our Action Bar
public class UnitActionSystemUI : MonoBehaviour
{
    // Prefab for action buttons
    [SerializeField] private Transform actionButtonPrefab;
    // Reference to the Container of the action buttons
    [SerializeField] private Transform actionButtonContainerTransform;
    // Reference to the action points counter on screen
    [SerializeField] private TextMeshProUGUI actionPointsText;
    // List of all the buttons
    private List<ActionButtonUI> actionButtonUIList;

    //* Called when the script instance is being loaded
    private void Awake() 
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    //* Start is called before the first frame update
    private void Start() 
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        Unit.OnAnyActionPointsChange += Unit_OnAnyActionPointsChange;

        UpdateActionPoints();
        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }

    //* Instantiates the Button Prefabs for the Action Buttons and destroys old buttons
    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }

        actionButtonUIList.Clear();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        //* Create the buttons for the actions based off our prefabs
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);

            actionButtonUIList.Add(actionButtonUI);
        }
    }

    //* Referencing the Selected unit Event
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e) 
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    //* Referencing the Selected action Event
    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e) 
    {
        UpdateSelectedVisual();
    }

    //* Referencing the On Action Started Event
    private void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    //* Updates the visuals for the buttons
    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    //* Updates the text that tells the player how many action points they have
    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        actionPointsText.text = "Action Points: " + selectedUnit.GetActionPoints();
    }

    //* Referencing the On Turn Changed Event
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

     //* Referencing the On Any Action Points Change Event
    private void Unit_OnAnyActionPointsChange(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }
}
