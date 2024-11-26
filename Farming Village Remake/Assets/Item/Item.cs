using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Create Item/Item")]
public class Item : ScriptableObject {

    [Header("Only Gameplay")]
    public string itemname;
    public ItemType type;
    public ActionType actionType;
    public SeasonSell seasonsell;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;
    public bool sellable = false;
    public int price;

    [Header("both")]
    public Sprite image;

    public int Item_Id;

    [Header("Seed Specific")]
    public PlantData plantdata;
}

public enum ItemType
{
    None, Tool, Seed, Material
}   

public enum ActionType
{
    None, Digging, Cutting, Watering, Plowing, Plant, Gift
}

public enum SeasonSell
{
    None, Summer, Winter
}


