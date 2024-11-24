using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public WorldTimeManager worldTimeManager;

    public GameObject shopslots;
    public GameObject buyslot;
    public Transform content;

    public Item[] itemseeds;


    void Start()
    {
        SpawnBuySlots();
    }

    /*public void AddBuySlot(Item item)
    {
        for (int i = 0; i < buyslots.Length; i++)
        {
            Buyslot slot = buyslots[i];
            BuySlotItem buyInSlot = slot.GetComponentInChildren<BuySlotItem>();

            if (buyInSlot == null)
            {
                GameObject newSlot = Instantiate(buyslot, shopslots.transform);
                newSlot.transform.localScale = Vector3.one;

                BuySlotItem buyslotItem = newSlot.GetComponent<BuySlotItem>();
                buyslotItem.InitialiseItem(item);
                return;
            }
        }
    }*/


    public void SpawnBuySlots()
    {
        foreach (Transform child in shopslots.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < itemseeds.Length; i++)
        {
            // Check if the item's valid seasons include the current season
            if (itemseeds[i].seasonsell.ToString() == worldTimeManager.Seasons)
            {
                GameObject newSlot = Instantiate(buyslot, shopslots.transform);
                newSlot.transform.localScale = Vector3.one;

                BuySlotItem buyslotItem = newSlot.GetComponent<BuySlotItem>();
                buyslotItem.InitialiseItem(itemseeds[i]);
            }
        }
    }


}
