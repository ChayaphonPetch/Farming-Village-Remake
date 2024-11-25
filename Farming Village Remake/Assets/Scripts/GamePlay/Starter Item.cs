using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] starter_item;
    public InventorySlot[] inventoryitemspawn;

    private bool itemsSpawned = false;

    void Start()
    {
        Starteritemspawn();
    }

    public void Starteritemspawn()
    {
        if (!itemsSpawned)
        {
            foreach (var item in starter_item)
            {
                bool result = AddItemStarter(item);
                if (result)
                {
                    Debug.Log($"Item {item.name} Added");
                }
                else
                {
                    Debug.Log("Inventory is full!!");
                }
            }
            itemsSpawned = true;
        }
        else
        {
            Debug.Log("Items have already been spawned!");
        }
    }

    public bool AddItemStarter(Item item)
    {
        for (int i = 0; i < inventoryitemspawn.Length; i++)
        {
            InventorySlot slot = inventoryitemspawn[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < inventoryManager.maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventoryitemspawn.Length; i++)
        {
            InventorySlot slot = inventoryitemspawn[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                inventoryManager.SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }
}
