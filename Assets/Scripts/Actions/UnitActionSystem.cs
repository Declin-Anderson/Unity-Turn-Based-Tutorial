using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Handles the inputs to do unit actions
public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance { get; private set; }
    // Grabs the Event Handler for when we select units
    public event EventHandler OnSelectedUnitChanged;
    // Grabs the Event Handler for when we select actions
    public event EventHandler OnSelectedActionChanged;
    // Grabs the Event Handler that will handle showing and hiding the busy bar
    public event EventHandler<bool> OnBusyChanged;
    // Grabs the Event Handler that will handle when an action is done
    public event EventHandler OnActionStarted;
    // Current unit selected by the player
    [SerializeField] private Unit selectedUnit;
    // The layer that the units are on
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;

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

    //* Start is called before the first frame update
    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    //* Update is called once per frame
    private void Update()
    {
        if(isBusy)
        {
            return;
        }

        
        // When the left mouse is pressed it determines if it is a valid click
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (TryHandleUnitSelection()) return;

        HandleSelectedAction();
    }

    //* Handles the mouse presses and determine what action was taken from the mouse click
    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            // Checks to see if the grid position is valid
            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                return;
            }

            // Checks to see if there is action points to spend
            if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
            {
                return;
            }

            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);

            OnActionStarted?.Invoke(this, EventArgs.Empty);

            /**
            ** Using switch cases to determine what the proper action is when one is selected
                switch (selectedAction)
                {
                    case MoveAction moveAction:
                        if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                        {
                            SetBusy();
                            moveAction.Move(mouseGridPosition, ClearBusy);
                        }
                        break;
                    case SpinAction spinAction:
                        SetBusy();
                        spinAction.Spin(ClearBusy);
                        break;
                    default:
                        break;
                }
            */
        }
    }

    //* Checks to see if the mouse clicks on a unit to select them
    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Raytraces to the unit to make sure the mouse clicks on the unit collider
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if(unit == selectedUnit)
                    {
                        // Unit is already selected
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    }

    //* Sets the unit that is clicked on as the selected unit and invokes the event
    // @param unit The unit that was selected by the player
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        SetSelectedAction(unit.GetMoveAction());

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    //* Sets the selectedAction variable to the action that the player choose
    // @param baseAction The action that the player has chosen
    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;

        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    //* Gets the unit that is currently selected by the player
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    //* Sets the status of the unit that it is currently doing an action
    private void SetBusy()
    {
        isBusy = true;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    //* Sets the status of the unit to not doing anything currently
    private void ClearBusy()
    {
        isBusy = false;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    //* Returns the currently selected action
    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
