using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour//, IDataPersistence
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
    }

    /*public void LoadData(GameData data)
    {
        this.Player_name = data._Name;
        this.current_money = data._Money;
        this.current_stamina = data._Stamina;
    }

    public void SaveData(ref GameData data) 
    {
        data._Name = this.Player_name;
        data._Money = this.current_money;
        data._Stamina = this.current_stamina;
    }
    */
}