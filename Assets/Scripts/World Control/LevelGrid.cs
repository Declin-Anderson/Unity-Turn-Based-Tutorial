using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Creates the Level Grid that the grid will be on with the units and objects
public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    // Debug prefab for the grid
    [SerializeField] private Transform gridDebugObjectPrefab;
    // Gridsystem being used
    private GridSystem gridSystem;

    //* Called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelGrid!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    //* Setting a tile so that it marks that a unit is there
    // @param gridPosition where the unit is being added to position wise
    // @param unit the unit that is being added
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    //* Retriving a tile to see if you a unit is there or not
    // @param gridPosition the position that will be checked for units
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    //* Removing that a Unit was in a tile
    // @param gridPosition the position that a unit will be removed from
    // @param unit the unit that is being removed
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    //* Called for when a unit is moving to a new position
    // @param unit the unit that is moving
    // @param fromGridPosition the position that the unit is starting in
    // @param toGridPosition the position that the unit is moving to
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }

    //* Gets the grid position when a world position is given
    // @param worldPosition the world position that will be converted to a grid position
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    //* Gets the world position when a gird position is given
    // @param gridPosition the grid postion that will be converted to a world position
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    //* Gets the width of the level grid
    public int GetWidth() => gridSystem.GetWidth();

    //* Gets the height of the level grid
    public int GetHeight() => gridSystem.GetHeight();

    //* Checks to see if the a grid position is valid
    // @param gridPosition the grid position that will be checked if it is valid to move to
    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    //* Checks to see if a grid position has a unit in it
    // @param gridPosition the grid position that is being checked to see if it has a unit in
    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }
}
