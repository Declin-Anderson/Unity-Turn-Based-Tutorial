using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Controls the UI for our busy bar when the player is locked out of acting
public class ActionBusyUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;

        Hide();
    }

    //* Shows the busy bar
    private void Show()
    {
        gameObject.SetActive(true);
    }

    //* Hides the busy bar
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    //* Referencing the On Busy Event
    private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
    {
        if (isBusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
