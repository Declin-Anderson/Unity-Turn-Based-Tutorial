using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

    // This will build the grid system that the units will move along the tiles in
    public GridSystem(int width, int height, float cellSize)
    {
        // Basic setters
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        // Goes through the Grid to create a line in the center of each of the tiles to show the tiles
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                gridObjectArray[x,z] = new GridObject(this, gridPosition);
            }
        }
    }

    // Gets the world position of the object that will be the center of the grid tile
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    // Gather the grid position of the chosen spot
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    // Allows for you to create debug objects for the grid if need be
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
               Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
               GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
               gridDebugObject.SetGridObject(gridObjectArray[x,z]);
            }
        }
    }

    // Get the grid object.
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    // checks to make sure the tile is valid for movement
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return  gridPosition.x >= 0 && 
                gridPosition.z >= 0 && 
                gridPosition.x < width && 
                gridPosition.z < height;
    }
}
