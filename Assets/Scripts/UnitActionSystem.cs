using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance {get; private set;}

    public event EventHandler OnSelectedUnitChanged;
    // Current unit selected by the player
    [SerializeField] private Unit selectedUnit;
    // The layer that the units are on
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake() 
    {
        if(Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update() 
    {
        // When the mouse is pressed it runs the HandleUnitSelection method and the Move method in the Unit script
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
            }
        }
    }
    
    // Checks to see if the mouse clicks on a unit to select them
    private bool TryHandleUnitSelection()
    {
        // Raytraces to the unit to make sure the mouse clicks on the unit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    // Sets the unit that is clicked on as the selected unit and invokes the event
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
