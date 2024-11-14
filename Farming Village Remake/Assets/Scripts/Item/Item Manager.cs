using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private TileManager _tileManager;
    public PlayerStats playerStats;

    public InventoryManager inventoryManager;
    public Item[] itemsTools;
    public Item[] itemsSeeds;

    public float staminaCost = 2f;
    void Start()
    {
        if (_tileManager == null)
        {
            _tileManager = FindObjectOfType<TileManager>();
        }

        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
    }
    public void doPlowing()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            if (receivedItem.type == ItemType.Tool && receivedItem.actionType == ActionType.Plowing)
            {
                _tileManager.HandlePlowingTile();
                Debug.Log("Plowing by: " + receivedItem.name);
            }
            else
            {
                Debug.Log("Selected item is not suitable for plowing.");
            }
        }
        else
        {
            Debug.Log("No item selected.");
        }
    }
    public void wateringPlowing()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            if (receivedItem.type == ItemType.Tool && receivedItem.actionType == ActionType.Watering)
            {
                _tileManager.HandleWateringTile();
                Debug.Log("Watering Plowing by: " + receivedItem.name);
            }
            else
            {
                Debug.Log("Selected item is not suitable for Watering plowing.");
            }
        }
        else
        {
            Debug.Log("No item selected.");
        }
    }

    public void destoryPlowing()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            if (receivedItem.type == ItemType.Tool && receivedItem.actionType == ActionType.Digging)
            {
                _tileManager.HandleRemovingTile();
                Debug.Log("Remove Plowing by: " + receivedItem.name);
            }
            else
            {
                Debug.Log("Selected item is not suitable for remove plowing.");
            }
        }
        else
        {
            Debug.Log("No item selected.");
        }
    }
}
