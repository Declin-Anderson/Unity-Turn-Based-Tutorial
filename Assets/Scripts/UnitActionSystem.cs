using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    // Current unit selected by the player
    [SerializeField] private Unit selectedUnit;
    // The layer that the units are on
    [SerializeField] private LayerMask unitLayerMask;

    private void Update() 
    {
        // When the mouse is pressed it runs the HandleUnitSelection method and the Move method in the Unit script
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition());
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
                selectedUnit = unit;
                return true;
            }
        }
        return false;
    }
}
