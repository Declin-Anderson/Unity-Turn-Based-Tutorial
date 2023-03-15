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
    // Position of the unit
    private GridPosition gridPosition;
    // Movement action of the unit
    private MoveAction moveAction;
    // Spin action of the unit
    private SpinAction spinAction;
    // Holds the actions that the unit can do
    private BaseAction[] baseActionArray;

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

    //* Gets the list of actions that this unit can do
    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }
}
