using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public string _Name;
    public int _Money;
    public int _Stamina;
    public int _Days;
    public int _Hours;
    public int _Minutes;
    public string Season;
    public string Weather;
    public Vector3 playerPosition;
    public List<InventoryItemData> inventoryItems;
        
    public int MarinRelation;
    public int AliceRelation;
    public int AlexRelation;
    public int OldManRelation;

    public GameData()
    {
        this._Name = "Player";
        this._Stamina = 150;
        this._Money = 50;
        this._Days = 1;
        this._Hours = 6;
        this._Minutes = 30;
        this.Season = "Summer";
        this.Weather = "Clear";
        playerPosition = Vector3.zero;


        this.inventoryItems = new List<InventoryItemData>();

    }

}

[System.Serializable]
public class InventoryItemData
{
    public string itemName;
    public int count;
    public int slotIndex;

    public InventoryItemData(string itemName, int count, int slotIndex)
    {
        this.itemName = itemName;
        this.count = count;
        this.slotIndex = slotIndex;
    }
}