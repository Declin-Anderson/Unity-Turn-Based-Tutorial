using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* This is the parent class that all of the unit actions will expand from
public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;

    //* Called when the script instance is being loaded
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    //* Parent function the the children will implement to gather the name of the action
    public abstract string GetActionName();

    //* Parent function that the children will implement to do their action
    // @param gridPosition the position that the action will take place
    // @param onActionComplete the variable keeping track if the function is complete
    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    //* Checks to see if the grid position being clicked is valid for movement
    // @param gridPosition the grid position currently being clicked
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        // Gets the list of valid grid positions to cross reference
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    //* Gathers the valid position that the unit the unit will be able to use for actions
    public abstract List<GridPosition> GetValidActionGridPositionList();

    //* Returns the action point cost of an action with a default of 1 for their cost
    public virtual int GetActionPointsCost()
    {
        return 1;
    }
}
