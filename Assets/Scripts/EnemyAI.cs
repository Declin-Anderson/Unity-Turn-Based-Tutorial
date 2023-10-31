using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Amount of time for the enemy turn
    private float timer;

    //* Start is called before the first frame update
    private void Start() 
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;    
    }

    // Update is called once per frame
    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TurnSystem.Instance.NextTurn();
        }
    }

    //* Referencing the On Turn Changed Event
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e) 
    {
        timer = 2f;
    }
}
