using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private TileManager _tileManager;
    public GameObject PlantPrefab;
    public Transform plantSpawnPoint;

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
            Debug.Log("Selected item is not suitable for the action or no item selected.");
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

    public void SowSeed()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (IsValidItem(receivedItem, ItemType.Seed, ActionType.Plant))
        {
            inventoryManager.GetSelectedItem(true);
            Debug.Log("Used Item: " + receivedItem.name);

            if (PlantPrefab != null && plantSpawnPoint != null)
            {
                GameObject newPlant = Instantiate(PlantPrefab, plantSpawnPoint.position, Quaternion.identity);
                PlantGrowingManager plantManager = newPlant.GetComponent<PlantGrowingManager>();

                if (plantManager != null && receivedItem.plantdata != null)
                {
                    plantManager.plantData = receivedItem.plantdata;
                    Debug.Log("Seed prefab planted successfully with data: " + receivedItem.plantdata.name);
                }
                else
                {
                    Debug.LogWarning("PlantGrowingManager or PlantData is missing on the prefab.");
                }
            }
            else
            {
                Debug.LogWarning("Seed prefab or plantSpawnPoint is not assigned.");
            }
        }
        else
        {
            Debug.Log("Selected item is not suitable for sowing or no item selected.");
        }
    }
}
