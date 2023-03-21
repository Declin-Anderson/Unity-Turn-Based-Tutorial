using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Handles the Visual Indicator for when a unit is selected
public class UnitSelectedVisual : MonoBehaviour
{
    // Unit that the indicator is attached to
    [SerializeField] private Unit unit;

    // Mesh Renderer of the visual indicator
    private MeshRenderer meshRenderer;

    //* Called when the script instance is being loaded
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    //* Start is called before the first frame update
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

        UpdateVisual();
    }

    //* Referencing the On Selected Unit Changed Event
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    //* Enables or disables the visual indicator depending on if the character is selected
    private void UpdateVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
