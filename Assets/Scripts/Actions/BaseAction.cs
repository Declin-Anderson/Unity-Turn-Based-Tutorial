using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* This is the parent class that all of the unit actions will expand from
public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;

    //* Called when the script instance is being loaded
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }
}
