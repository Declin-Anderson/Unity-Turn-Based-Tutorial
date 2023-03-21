using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Handles the UI for turns
public class TurnSystemUI : MonoBehaviour
{
    // Reference to the button that will end turn
    [SerializeField] private Button endTurnBtn;
    // Reference to the text that displays the current turn
    [SerializeField] private TextMeshProUGUI turnNumberText;

    //* Start is called before the first frame update
    private void Start() 
    {
        endTurnBtn.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
    }

    //* Updates the text of the turn number to be the current turn
    private void UpdateTurnText() 
    {
        turnNumberText.text = "TURN: " + TurnSystem.Instance.GetTurnNumber();
    }

    //* Referencing the On Turn Changed Event
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e) 
    {
        UpdateTurnText();
    }
}
