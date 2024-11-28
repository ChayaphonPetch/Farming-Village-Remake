using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Create Item/UpgradeItem")]
public class ItemUpgrade : ScriptableObject
{

    [Header("Only Gameplay")]
    public string itemname;
    public SeasonforSell seasonsell;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public string itemdetail;
    public int price;

    [Header("both")]
    public Sprite image;
}

public enum SeasonforSell
{
    None, Summer, Winter
}


