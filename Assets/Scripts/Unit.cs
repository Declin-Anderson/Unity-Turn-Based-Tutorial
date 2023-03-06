using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition gridPosition;
    private MoveAction moveAction;

    private void Awake() 
    {
        moveAction = GetComponent<MoveAction>();
    }
    private void Start() 
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    // On update the charcter will move to a set position when T is pressed according to our movespeed
    private void Update()
    {

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition)
        {
            // Unit has changed position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
