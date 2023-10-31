using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* This is a grid tile that the units transverse on
public class GridObject
{
    // Position relative to the entire system
    private GridPosition gridPosition;
    // The grid that the tile belongs to
    private GridSystem gridSystem;
    // Units currently on the tile
    private List<Unit> unitList;

    //* Constructor
    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    //* Overriding the ToString function to be able to print the grid position and any potential units in it
    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }

    //* Adds a unit to the list for the tile
    // @param unit the unit that is going to be added to the list
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    //* Removes the unit from the list connected to the tile
    // @param unit Unit that is going to be removed from the list
    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    //* Returns the list of units on the tile
    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    //* Checks to see if the tile has any units
    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    //* Returns the unit on the grid object
    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            return unitList[0];
        }
        else
        {
            return null;
        }
    }
}
