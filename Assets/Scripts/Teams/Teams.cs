using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    [SerializeField] private Team[] _teams;
    [SerializeField] private MultiColorSlider _multiColorSlider;

    private bool isSwaped;

    private void Awake()
    {
        _multiColorSlider.CreateBlank();
        ReCalculate();

    }

    public void ReCalculate()
    {
        int totalAmount = 0;

        foreach (var team in _teams)
        {
            int amount = Random.Range(0, 26);
            totalAmount += amount;

            team.SetPoint(amount);

        }

        int totalPercent = 0;

        foreach (var team in _teams)
        {
            team.SetTotalAmount(totalAmount);
            totalPercent += team.Percent;
        }

        _multiColorSlider.Colorize(_teams);
    }

    public void ChangeFirstTeam()
    {
        _teams[3].TakePoints(1, out int points);
        _teams[0].AddPoints(points);

        _multiColorSlider.Colorize(_teams);
    }
}

public enum TeamId
{
    First,
    Second,
    Third,
    Fourth
}