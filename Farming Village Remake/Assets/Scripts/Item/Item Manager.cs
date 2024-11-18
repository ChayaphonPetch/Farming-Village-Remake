using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private TileManager _tileManager;
    public GameObject PlantPrefab;

    public InventoryManager inventoryManager;
    public Item[] itemsTools;
    public Item[] itemsSeeds;

    void Start()
    {
        _tileManager = _tileManager ?? FindObjectOfType<TileManager>();
    }

    private bool IsValidItem(Item item, ItemType type, ActionType action)
    {
        return item != null && item.type == type && item.actionType == action;
    }

    public void PerformAction(ActionType actionType)
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (IsValidItem(receivedItem, ItemType.Tool, actionType))
        {
            HandleToolAction(actionType, receivedItem);
        }
        else
        {
            //Debug.Log("Selected item is not suitable for the action or no item selected.");
        }
    }

    private void HandleToolAction(ActionType actionType, Item receivedItem)
    {
        switch (actionType)
        {
            case ActionType.Plowing:
                _tileManager.HandlePlowingTile();
                Debug.Log("Plowing by: " + receivedItem.name);
                break;
            case ActionType.Watering:
                _tileManager.HandleWateringTile();
                Debug.Log("Watering by: " + receivedItem.name);
                break;
            case ActionType.Digging:
                _tileManager.HandleRemovingTile();
                Debug.Log("Removing by: " + receivedItem.name);
                break;
        }
    }

    public void SowingSeed()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        PlantGrowingManager plantManager = PlantPrefab.GetComponent<PlantGrowingManager>();
        if (IsValidItem(receivedItem, ItemType.Seed, ActionType.Plant))
        {
            Debug.Log(receivedItem.name);
            if (receivedItem.plantdata != null)
            {
                plantManager.plantData = receivedItem.plantdata;
                _tileManager.SpawnPlantinTiles(plantManager.gameObject);
                Debug.Log("Used Item: " + receivedItem.name);
            }
            else
            {
                //Debug.LogWarning("PlantData is missing for the selected item.");
            }
        }
        else
        {
            //Debug.Log("Selected item is not suitable for sowing or no item selected.");
        }
    }
}
