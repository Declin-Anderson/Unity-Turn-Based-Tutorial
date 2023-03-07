using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Generates Visuals for individual tiles and the logic behind them
public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    // Shows the meshRenderer for a singluar tile
    public void Show()
    {
        this.meshRenderer.enabled = true;
    }

    // Hides the tile when it is not needed
    public void Hide()
    {
        this.meshRenderer.enabled = false;
    }
}
