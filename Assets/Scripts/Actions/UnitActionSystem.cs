using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Handles the inputs to do unit actions
public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance { get; private set; }
    // Grabs the Event Handler
    public event EventHandler OnSelectedUnitChanged;
    // Current unit selected by the player
    [SerializeField] private Unit selectedUnit;
    // The layer that the units are on
    [SerializeField] private LayerMask unitLayerMask;

    //* Called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //* Update is called once per frame
    private void Update()
    {
        // When the left mouse is pressed it determines the current unit and then moves the unit to the clicked tile if valid
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }
        }

        // When the right mouse is pressed it spins the current unit
        if (Input.GetMouseButtonDown(1))
        {
            selectedUnit.GetSpinAction().Spin();
        }
    }

    //* Checks to see if the mouse clicks on a unit to select them
    private bool TryHandleUnitSelection()
    {
        // Raytraces to the unit to make sure the mouse clicks on the unit collider
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    //* Sets the unit that is clicked on as the selected unit and invokes the event
    // @param unit The unit that was selected by the player
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    //* Gets the unit that is currently selected by the player
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
