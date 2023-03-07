using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Class for holding the grid that the units will interact on
public class GridSystem
{
    // Width of the grid
    private int width;
    // Height of the grid
    private int height;
    // Size of the tiles
    private float cellSize;
    // Array for holding all of the tiles
    private GridObject[,] gridObjectArray;

    //* Constructor will build the grid system that the units will move along the tiles in
    public GridSystem(int width, int height, float cellSize)
    {
        // Basic setters
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        // Goes through the Grid to create a line in the center of each of the tiles to show the tiles
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    //* Gets the world position of the object that will be the center of the grid tile
    // @param girdPosition the position that we are going to get the world position from
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    //* Gather the grid position of the chosen spot
    // @param worldPosition the position on the grid that we are going to gather the grid coordinates for
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    //* Allows for you to create debug objects for the grid if need be
    // @param debugPrefab the prefab that is used to generate the text for debugging
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // Creates the text that will display the grid coordinate
                GridPosition gridPosition = new GridPosition(x, z);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(gridObjectArray[x, z]);
            }
        }
    }

    //* Get the grid object.
    // @param gridPosition the position that we wish to get the position in the array of
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    //* Checks to make sure the tile is valid for movement
    // @param gridPosition the position that is being checked to see if its a valid position in the grid
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
                gridPosition.z >= 0 &&
                gridPosition.x < width &&
                gridPosition.z < height;
    }

    //* Gets the width of the grid
    public int GetWidth()
    {
        return width;
    }

    //* Gets the height of the grid
    public int GetHeight()
    {
        return height;
    }
}
