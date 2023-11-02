using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Handles the Shoot Action for Units
public class ShootAction : BaseAction
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit;
    }
    // State Machine for phases of Shooting
    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }
    // Holds the current state of the unit
    private State state;
    // Max amount of Tiles away the unit can shoot
    private int maxShootDistance = 7;
    // Timer for states when swapping
    private float stateTimer;
    // The unit being targeted
    private Unit targetUnit;
    // Handles if the unit can shoot
    private bool canShootBullet;

    //* Update is called once per frame
    private void Update()
    {
        // Prevents from spinning unless the unit is told to
        if (!isActive)
            return;

        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case State.Aiming:
                // Rotation of the model when it aims to create a smooth transtation between animation states
                Vector3 aimDir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;

                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed);
                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooloff:
                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }
    }

    // Handles the changing of states
    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                state = State.Cooloff;
                float coolOffStateTime = 0.5f;
                stateTimer = coolOffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }
    }

    // Performs the shoot
    private void Shoot()
    {
        OnShoot?.Invoke(this, new OnShootEventArgs{
            targetUnit = targetUnit,
            shootingUnit = unit
        });
        targetUnit.Damage(40);
    }

    //* Gets the name of the action
    public override string GetActionName()
    {
        return "Shoot";
    }

    //* Gets the valid grid position the action can be taken in
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        // Creating the list for the valid postion to be held in
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        // Looping through the grid positions to check if they:
        //      Are inside the movement range
        //      Aren't the own units grid position
        //      If the tile isn't currently occupied
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    // Outside of the grid
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance)
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid Position is empty, no Unit
                    continue;
                }

                Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    // Both Units on same
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    //* Performs the Shoot Action
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        
        Debug.Log("Aiming");
        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canShootBullet = true;
    }
}
