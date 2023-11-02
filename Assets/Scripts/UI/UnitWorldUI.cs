using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

//* Handles the UI for displyaing unit information
public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;


    private void Start()
    {
        Unit.OnAnyActionPointsChange += Unit_OnAnyActionPointsChange;
        healthSystem.OnDamaged += healthSystem_OnDamaged;

        UpdateActionPointsText();
        UpdateHealthBar();
    }

    //* Updates the Text to display the action points of the current unit
    private void UpdateActionPointsText()
    {
        actionPointsText.text = unit.GetActionPoints().ToString();
    }

    private void Unit_OnAnyActionPointsChange(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }

    private void healthSystem_OnDamaged(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
}
