using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);

        if (result == true)
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Inventory has full!!");
        }
    }

    public void RandomItem()
    {
        int id = Random.Range(3, itemsToPickup.Length);

        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Inventory is full!!");
        }
    }

    public void GetSelectItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Use Item: " + receivedItem.name);
        }
        else
        {
            Debug.Log("No Item");
        }
    }

    public void UseGetSelectItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used Item: " + receivedItem.name);
        }
        else
        {
            Debug.Log("No Item");
        }
    }

    public void UseToolItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);

        if (receivedItem != null)
        {
            bool isTool = (receivedItem.type == ItemType.Tool);

            receivedItem = inventoryManager.GetSelectedItem(!isTool);

            if (receivedItem != null)
            {
                Debug.Log("Used Item: " + receivedItem.name);
            }
            else
            {
                Debug.Log("No Item");
            }
        }
        else
        {
            Debug.Log("No Item");
        }
    }

}