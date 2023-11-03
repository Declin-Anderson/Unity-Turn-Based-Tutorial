using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Handles the movement of a unit when the move action is taken by them
public class MoveAction : BaseAction
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;
    // Max movement range of a the unit
    [SerializeField] private int maxMoveDistance = 4;
    // Target Spot for the model to move to
    private Vector3 targetPosition;

    //* Called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }

    //* Update is called once per frame
    private void Update()
    {
        // Prevents from moving unless the unit is told to
        if (!isActive)
            return;

        // How close the unit needs to be to the end position of its movement
        float stoppingDistance = .1f;

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Checks to see if the distance from the unit to its targets spot is greater than 0
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            // Determining the direction and the speed that the unit shall reach its end goal
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            OnStopMoving?.Invoke(this, EventArgs.Empty);
            ActionComplete();
        }

        // Rotation of the model when it moves to create a smooth transtation between animation states
        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    //* Changes the target vector for the model to move to
    // @param gridPosition the target position that the unit is going to move to
    // @param onActionComplete a delegate for letting the system know that a unit is currently moving
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        // Sets the target position
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        
        OnStartMoving?.Invoke(this, EventArgs.Empty);

        ActionStart(onActionComplete);
    }

    //* Gathers the valid position that the unit can move to
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        // Creating the list for the valid postion to be held in
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        // Looping through the grid positions to check if they:
        //      Are inside the movement range
        //      Aren't the own units grid position
        //      If the tile isn't currently occupied
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    // Outside of the grid
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    // Same Position
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid Position has a unit in it
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    //* Returns the name of the action which is Move
    public override string GetActionName()
    {
        return "Move";
    }
}
