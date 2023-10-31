using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Class for the units that will hold the actions that a unit can do and their movement information
public class Unit : MonoBehaviour
{
    // Max Action Points
    private const int ACTION_POINTS_MAX = 2;

    public static event EventHandler OnAnyActionPointsChange;

    // Determines whether this unit is a enemy
    [SerializeField] private bool isEnemy;

    // Position of the unit
    private GridPosition gridPosition;
    // Movement action of the unit
    private MoveAction moveAction;
    // Spin action of the unit
    private SpinAction spinAction;
    // Holds the actions that the unit can do
    private BaseAction[] baseActionArray;
    // Number of Action Points an unit has
    private int actionPoints = ACTION_POINTS_MAX;

    //* Called when the script instance is being loaded
    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
    }

    //* Start is called before the first frame update
    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    //* Update is called once per frame
    private void Update()
    {
        // Checking the position of the unit
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            // Unit has changed position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    //* Gets the movement action of the unit
    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    //* Gets the spin action of the unit
    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    //* Gets the grid position that the unit is currently in
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    //* Gets the world position that the unit is currently at
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    //* Gets the list of actions that this unit can do
    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    //* Checks to see if the action points can be spent and then spends if possible
    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    //* Returns if you have action points to spend
    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (actionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }

        return false;
    }

    //* Spends action points according to the amount that is given
    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
    }

    //* Gets the action points remaining on this unit
    public int GetActionPoints()
    {
        return actionPoints;
    }

    //* Referencing the On Turn Changed Event
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn() || (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoints = ACTION_POINTS_MAX;

            OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
        }
    }

    //* Returns the isEnemy variable
    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void Damage()
    {
        Debug.Log(transform + " damaged");
    }
}
