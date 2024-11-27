using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private NPCData _data;
    private InventoryManager _inventorymanager;
    private DialogueActivator _dialogueActivator;
    private ItemManager _itemmanager;
    private DialogueUI _dialogueUI;
    private PlayerAction _playeraction;

    [SerializeField] private DialogueObject dialogueObject;

    private void Start()
    {
        _inventorymanager = FindObjectOfType<InventoryManager>();
         
        _dialogueActivator = FindObjectOfType<DialogueActivator>();
        _itemmanager = FindObjectOfType<ItemManager>();
        _dialogueUI = FindObjectOfType<DialogueUI>();
        _playeraction = FindObjectOfType<PlayerAction>();

        if (_inventorymanager == null) Debug.LogError("InventoryManager not found!");
        if (_data == null) Debug.LogError("NPCData not found!");
        if (_dialogueActivator == null) Debug.LogError("DialogueActivator not found!");
        if (_itemmanager == null) Debug.LogError("ItemManager not found!");
    }

    public void GiftCheck(DialogueObject dialogueObject)
    {
        Item receivedItem = _inventorymanager.GetSelectedItem(false);
        if (_itemmanager.IsValidItem(receivedItem, ItemType.Material, ActionType.Gift))
        {
            _inventorymanager.GetSelectedItem(true);
        }
        else 
        {
            _dialogueUI.CloseDialogueBox();
            return;
        }

    }

    public void IncreaseReceivePoint(int npcindex)
    {
        Item receivedItem = _inventorymanager.GetSelectedItem(false);
        if (_itemmanager.IsValidItem(receivedItem, ItemType.Material, ActionType.Gift))
        {
            _data.AddReceivePoint(npcindex);
        }
    }
}
