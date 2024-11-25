using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellStorageManager : MonoBehaviour
{
    public InventorySlot[] sellslots;
    public int moneyget;
    public int Total;
    private PlayerData playerData;
    private WorldTimeManager worldTimeManager;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        worldTimeManager = FindObjectOfType<WorldTimeManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SellItems();
        }
    }

    public void SellItems()
    {
        for (int i = 0; i < sellslots.Length; i++)
        {
            InventorySlot currentSlot = sellslots[i];
            InventoryItem itemInSlot = currentSlot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null)
            {
                int itemTotal = itemInSlot.item.price * itemInSlot.count;

                if (itemInSlot.item.sellable)
                {
                    if (itemInSlot.item.seasonsell.ToString() == "Summer" && worldTimeManager.Seasons != "Summer")
                    {
                        itemTotal = Mathf.CeilToInt(itemTotal * 1.03f);
                    }

                    else if (itemInSlot.item.seasonsell.ToString() == "Winter" && worldTimeManager.Seasons != "Winter")
                    {
                        itemTotal = Mathf.CeilToInt(itemTotal * 1.05f); 
                    }

                    Debug.Log("Item Price: " + itemTotal);
                    Total += itemTotal;
                    Destroy(itemInSlot.gameObject);
                }
            }
        }

        Debug.Log("Grand Total: " + Total);
        playerData.current_money += Total;
        Total = 0;
    }

        /*if (currentSlot != null && currentSlot.item != null)
            {
                // Get the current item's price
                int itemPrice = currentSlot.item.price;

                // Sell the item based on the stack count
                if (currentSlot.item.stackable && currentSlot.stackCount > 1)
                {
                    // Sell the whole stack or part of it
                    int amountToSell = currentSlot.stackCount;
                    SellItem(currentSlot.item, amountToSell);

                    // Remove the sold items from the stack
                    currentSlot.stackCount -= amountToSell;

                    // If the stack becomes empty, remove the item reference
                    if (currentSlot.stackCount <= 0)
                    {
                        currentSlot.item = null;
                    }
                }
                else
                {
                    // Sell a single item if it's not stackable or if there's only 1 left
                    SellItem(currentSlot.item, 1);

                    // Remove the item from the slot
                    currentSlot.item = null;
                    currentSlot.stackCount = 0;
                }
            }*/
   }
    



