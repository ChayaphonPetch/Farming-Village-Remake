using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Create Data/Npc ")]
public class NpcDatas : ScriptableObject
{
    [Header("Default NPC")]
    public string npcName;
    public int default_max_relationship;
    public int currentRelationship;
    public bool isGift_stamina;
    public bool isTotem;
    public bool isMarry;

    public Image totem;

}


