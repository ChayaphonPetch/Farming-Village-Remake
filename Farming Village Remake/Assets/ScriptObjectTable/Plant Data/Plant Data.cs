using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Data/Plant")]
public class PlantData : ScriptableObject
{

    [Header("Plant")]
    public string Name;
    public int dayGrowing;
    public SeasonType Season;

    [Header("Plant Sprite Stages")]
    public List<Sprite> GrowthStages;

    public Item Product;

    public enum SeasonType { Spring, Summer, Fall, Winter }
}