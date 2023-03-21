using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Handles the turns for players and ai
public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

    // Grabs the Event Handler for when we change turns
    public event EventHandler OnTurnChanged;
    // Current turn the game is on
    private int turnNumber = 1;

    //* Called when the script instance is being loaded
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one TurnSystem!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    //* Goes to the next turn
    public void NextTurn()
    {
        turnNumber++;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    //* Gets the current turn number
    public int GetTurnNumber()
    {
        return turnNumber;
    }
}
