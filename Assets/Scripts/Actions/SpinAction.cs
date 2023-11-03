using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Spins the unit that is running the Spin Action
public class SpinAction : BaseAction
{

    // The current amount of degrees that character has spun
    private float totalSpinAmount;
    // Delegate to notify the Unit Action System that a unit is done spinning
    /*
    * Alternate way where you can build the delegate versus using Action for a void function and Func for a return
    * public delegate void SpinCompleteDelegate();
    * private SpinCompleteDelegate onSpinComplete; 
    *
    *private Action onSpinComplete;
    */

    //* Update is called once per frame
    private void Update()
    {
        // Prevents from spinning unless the unit is told to
        if (!isActive)
            return;

        // Sets the spin speed that the unit will go at
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

        totalSpinAmount += spinAddAmount;

        // Once the Unit has spun a total of 360 degrees its stops it movement
        if (totalSpinAmount >= 360)
        {
            ActionComplete();
        }
    }

    //* Causes the unit that this is ran on to start spinning
    // @param gridPosition dead variable
    // @param onSpinComplete a delegate for keeping track when the action finishes
    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        totalSpinAmount = 0f;

        ActionStart(onActionComplete);
    }

    //* Returns the name of the action which is Spin
    public override string GetActionName()
    {
        return "Spin";
    }

    //* Gathers the valid position that the unit can spin at
    public override List<GridPosition> GetValidActionGridPositionList()
    {
        // Creating the list for the valid postion to be held in
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    //* Cost 2 action points for the unit to spin
    public override int GetActionPointsCost()
    {
        return 2;
    }
}
