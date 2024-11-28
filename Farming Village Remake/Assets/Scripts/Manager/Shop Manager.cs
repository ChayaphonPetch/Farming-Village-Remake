using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public WorldTimeManager worldTimeManager;

    public GameObject shopslots;
    public GameObject UpgradPage;
    public GameObject buyslot;
    public GameObject upgradeslot;
    public Image Merchant_Image;
    public TextMeshProUGUI Merchant_Name;

    public Item[] itemseeds;
    public ItemUpgrade[] upgradeitem;

    private PlayerData playerdata;


    void Start()
    {
        playerdata = FindObjectOfType<PlayerData>();
        SpawnBuySlots();
        SpawnUpgradeSlots();
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

    public void SpawnUpgradeSlots()
    {
        foreach (Transform child in UpgradPage.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < upgradeitem.Length; i++)
        {
            if (!playerdata.purchasedUpgrades.Contains(upgradeitem[i])) // Check if item is already purchased
            {
                GameObject newSlot = Instantiate(upgradeslot, UpgradPage.transform);
                newSlot.transform.localScale = Vector3.one;

                UpgradeSlot upgradeslotitem = newSlot.GetComponent<UpgradeSlot>();
                upgradeslotitem.InitialiseItem(upgradeitem[i]);
            }
        }
    }
}

