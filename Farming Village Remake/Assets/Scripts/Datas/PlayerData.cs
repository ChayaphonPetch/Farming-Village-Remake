using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Player Data")]
    public string Player_name;
    public int current_money;
    public int current_stamina;

    [Header("Player Statistics")]
    public int MoneyTotal;
    public float playtime;

    private int previous_money;

    void Start()
    {
        previous_money = current_money;
    }

    void Update()
    {
        if (current_money != previous_money)
        {
            MoneyTotal += current_money - previous_money;
            previous_money = current_money;
        }

        playtime += Time.deltaTime;
    }

    void DisplayPlaytime(float totalSeconds)
    {
        int days = Mathf.FloorToInt(totalSeconds / 86400);
        int hours = Mathf.FloorToInt((totalSeconds % 86400) / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
    }
}