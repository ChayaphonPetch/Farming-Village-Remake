using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public int maxStackedItems = 64;
    public GameObject Holditemimage;
    public InventorySlot[] inventorySlots;
    public InventorySlot[] ToolbarSelect;
    public InputAction[] inventorySlots_key;
    public GameObject InventoryItemPrefab;

    int selectedSlot = 0;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            int direction = scroll > 0f ? -1 : 1;
            int newSlot = selectedSlot + direction;

            if (newSlot < 0)
            {
                newSlot = 8;
            }
            else if (newSlot > 8)
            {
                newSlot = 0;
            }
            ChangeSelectedSlot(newSlot);
        }

        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) || Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                //Debug.Log(i - 1);
                ChangeSelectedSlot(i - 1);
                break;
            }
        }
        ShowItemSlot();
    }

    void ChangeSelectedSlot(int newValue)
    {
        ToolbarSelect[selectedSlot].Deselect();

        ToolbarSelect[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }

        return null;
    }

    public void ShowItemSlot()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;

            if (item != null)
            {
                if (item.type != ItemType.Tool)
                {
                    Holditemimage.GetComponent<SpriteRenderer>().sprite = item.image;
                    Holditemimage.SetActive(true);
                }
                else
                {
                    Holditemimage.SetActive(false);
                }   
            }
        }
        else
        {
            Holditemimage.SetActive(false);
        }
    }

    /*public void LoadData(GameData gameData)
    {
        // Clear existing items in inventory
        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Destroy(itemInSlot.gameObject);
            }
        }

        // Load inventory items from GameData into their respective slots
        foreach (InventoryItemData itemData in gameData.inventoryItems)
        {
            // Use the ItemDatabase to retrieve the Item by its name
            Item itemToLoad = ItemDatabase.GetItemByName(itemData.itemName);
            if (itemToLoad != null)
            {
                // Find the correct slot by index and spawn the item
                int slotIndex = itemData.slotIndex; // Make sure this index is saved in InventoryItemData
                if (slotIndex >= 0 && slotIndex < inventorySlots.Length)
                {
                    InventorySlot slot = inventorySlots[slotIndex];
                    SpawnNewItem(itemToLoad, slot);
                    InventoryItem loadedItem = slot.GetComponentInChildren<InventoryItem>();
                    loadedItem.count = itemData.count;
                    loadedItem.RefreshCount();
                }
            }
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.inventoryItems.Clear(); // Clear the saved items before updating

        // Save current inventory items to GameData with their corresponding slot index
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                // Add the item with its index in the inventory
                gameData.inventoryItems.Add(new InventoryItemData(itemInSlot.item.name, itemInSlot.count, i));
            }
        }
    }*/
}