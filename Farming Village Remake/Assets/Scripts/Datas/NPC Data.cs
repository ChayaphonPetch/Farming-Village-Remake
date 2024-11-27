using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class NPCData : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private UIManager uiManager;

    public NpcDatas[] NpcDefault_Data;

    [Header("Marin Data")]
    public int Marin_current_relationship;
    public int Marin_Max_relationship;
    public int Marin_gift_receive;
    public bool Marin_stamina;
    public bool isMarinTotem;
    public bool isMarinMarry;

    [Header("Alice Data")]
    public int Alice_current_relationship;
    public int Alice_Max_relationship;
    public int Alice_gift_receive;
    public bool Alice_stamina;
    public bool isAliceTotem;
    public bool isAliceMarry;

    [Header("Alex Data")]
    public int Alex_current_relationship;
    public int Alex_Max_relationship;
    public int Alex_gift_receive;
    public bool Alex_stamina;
    public bool isAlexTotem;
    public bool isAlexMarry;

    [Header("Old Man Data")]
    public int Old_Man_current_relationship;
    public int Old_Man_relationship;
    public int Old_Man_gift_receive;
    public bool Old_Man_stamina;
    public bool isOld_ManTotem;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        //IncreaseStaminaForAllNPCs();
        CheckAllRelationships();
        CheckGiftReceiveAndIncreaseRelationship();
    }

    void Awake()
    {
        if (NpcDefault_Data != null && NpcDefault_Data.Length > 0)
        {
            if (NpcDefault_Data.Length > 0)
                Marin_Max_relationship = NpcDefault_Data[0].default_max_relationship;
                Marin_stamina = NpcDefault_Data[0].isGift_stamina;
                isMarinTotem = NpcDefault_Data[0].isTotem;
                isMarinMarry = NpcDefault_Data[0].isMarry;

            if (NpcDefault_Data.Length > 1)
                Alice_Max_relationship = NpcDefault_Data[1].default_max_relationship;
                Alice_stamina = NpcDefault_Data[0].isGift_stamina;
                isAliceTotem = NpcDefault_Data[0].isTotem;
                isAliceMarry = NpcDefault_Data[0].isMarry;

            if (NpcDefault_Data.Length > 2)
                Alex_Max_relationship = NpcDefault_Data[2].default_max_relationship;
                Alex_stamina = NpcDefault_Data[0].isGift_stamina;
                isAlexTotem = NpcDefault_Data[0].isTotem;
                isAlexMarry = NpcDefault_Data[0].isMarry;

            if (NpcDefault_Data.Length > 3)
                Old_Man_relationship = NpcDefault_Data[3].default_max_relationship;
                Old_Man_stamina = NpcDefault_Data[0].isGift_stamina;
                isOld_ManTotem = NpcDefault_Data[0].isTotem;
        }
    }

    void IncreaseStaminaForAllNPCs()
    {
        if (uiManager != null)
        {
            if (Marin_stamina == true)
            {
                uiManager.IncreaseMaxStamina(25);
            }

            if (Alice_stamina == true)
            {
                uiManager.IncreaseMaxStamina(25);
            }

            if (Alex_stamina == true)
            {
                uiManager.IncreaseMaxStamina(25);
            }

            if (Old_Man_stamina == true)
            {
                uiManager.IncreaseMaxStamina(25);
            }
        }
        else
        {
            Debug.LogWarning("UIManager not found! Cannot increase stamina for NPCs.");
        }
    }

    
    void CheckRelationship(string characterName, int currentRelationship, ref bool stamina, ref bool totem, ref bool marry)
    {
        if (currentRelationship >= 3)
        {
            stamina = true;
        }

        if (currentRelationship >= 4)
        {
            totem = true;
        }

        if (characterName != "Old_Man" && currentRelationship >= 5)
        {
            marry = true;
            //EnforceSingleMarriage(characterName);
        }

        //Debug.Log($"{characterName} - Stamina: {stamina}, Totem: {totem}, Marry: {marry}");
    }

    void CheckAllRelationships()
    {
        CheckRelationship("Marin", Marin_current_relationship, ref Marin_stamina, ref isMarinTotem, ref isMarinMarry);

        CheckRelationship("Alice", Alice_current_relationship, ref Alice_stamina, ref isAliceTotem, ref isAliceMarry);

        CheckRelationship("Alex", Alex_current_relationship, ref Alex_stamina, ref isAlexTotem, ref isAlexMarry);

        bool dummyMarry = false;
        CheckRelationship("Old_Man", Old_Man_current_relationship, ref Old_Man_stamina, ref isOld_ManTotem, ref dummyMarry);
    }

    void EnforceSingleMarriage(string marriedNPC)
    {
        if (marriedNPC != "Marin") isMarinMarry = false;
        if (marriedNPC != "Alice") isAliceMarry = false;
        if (marriedNPC != "Alex") isAlexMarry = false;

        Debug.Log($"Marriage status updated. Only {marriedNPC} can be married.");
    }

    void CheckGiftReceiveAndIncreaseRelationship()
    {
        if (Marin_gift_receive >= 4)
        {
            if (Marin_current_relationship < 5)
            {
                Marin_current_relationship++;
                Marin_gift_receive = 0;
                Debug.Log("Marin's relationship increased by 1.");
            }
            else
            {
                Debug.Log("Marin's relationship is at maximum (5).");
            }
        }

        if (Alice_gift_receive >= 4)
        {
            if (Alice_current_relationship < 5)
            {
                Alice_current_relationship++;
                Alice_gift_receive = 0;
                Debug.Log("Alice's relationship increased by 1.");
            }
            else
            {
                Debug.Log("Alice's relationship is at maximum (5).");
            }
        }

        if (Alex_gift_receive >= 4)
        {
            if (Alex_current_relationship < 5)
            {
                Alex_current_relationship++;
                Alex_gift_receive = 0;
                Debug.Log("Alex's relationship increased by 1.");
            }
            else
            {
                Debug.Log("Alex's relationship is at maximum (5).");
            }
        }

        if (Old_Man_gift_receive >= 4)
        {
            if (Old_Man_current_relationship < 5)
            {
                Old_Man_current_relationship++;
                Old_Man_gift_receive = 0;
                Debug.Log("Old Man's relationship increased by 1.");
            }
            else
            {
                Debug.Log("Old Man's relationship is at maximum (5).");
            }
        }
    }

    public void AddReceivePoint(int NpcIndex)
    {
        switch (NpcIndex)
        {
            case 0:
                Marin_gift_receive++;
                break;
            case 1:
                Alice_gift_receive++;
                break;
            case 2:
                Alex_gift_receive++;
                break;
            case 3:
                Old_Man_gift_receive++;
                break;
            default:
                break;
        }
    }


}
