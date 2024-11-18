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
            BuySlotItem BuyInSlot = slot.GetComponentInChildren<BuySlotItem>();
            if (BuyInSlot == null && BuyInSlot.item == item && BuyInSlot.item.seasonsell == worldTimeManager.Seasons)
            {
                SpawnBuySlots(item);
            }
        }
    }*/

    void SpawnBuySlots()
    {
        for (int i = 0; i < itemseeds.Length; i++)
        {
            GameObject newSlot = Instantiate(buyslot, shopslots.transform);
            newSlot.transform.localScale = Vector3.one;

            BuySlotItem buyslotItem = newSlot.GetComponent<BuySlotItem>();
            buyslotItem.InitialiseItem(itemseeds[i]);
        }
    }

}
