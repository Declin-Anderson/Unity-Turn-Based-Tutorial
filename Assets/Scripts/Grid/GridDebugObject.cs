using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Creates a grid coordinate on the floor for debugging the tile
public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    private GridObject gridObject;

    //* Sets a grid tile to have the debug text on it
    // @param gridObject pass the grid tile to be able to modify
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    //* Update is called once per frame
    private void Update()
    {
        textMeshPro.text = gridObject.ToString();
    }
}
