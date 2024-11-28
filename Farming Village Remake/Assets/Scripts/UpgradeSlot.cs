using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UpgradeSlot : MonoBehaviour
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI itemdetail;
    public TextMeshProUGUI itemprice;
    private int itemPrice;

    public Button buybutton;

    [HideInInspector] public ItemUpgrade Upgradeitem;
    private PlayerData playerdata;
    void Start()
    {
        playerdata = FindObjectOfType<PlayerData>();
        buybutton.onClick.AddListener(BuyItem);

    }

    public void InitialiseItem(ItemUpgrade Upgrade)
    {
        Upgradeitem = Upgrade;
        image.sprite = Upgrade.image;
        itemname.text = Upgrade.itemname;
        itemdetail.text = Upgrade.itemdetail;
        itemprice.text = Upgrade.price.ToString();
        itemPrice = Upgrade.price;
    }

    void Update()
    {

    }

    private void BuyItem()
    {
        int totalCost = itemPrice;

        if (playerdata.current_money >= totalCost)
        {
            playerdata.current_money -= totalCost;
            playerdata.purchasedUpgrades.Add(Upgradeitem);
            Destroy(gameObject);

            Debug.Log($"Purchased 1 of {Upgradeitem.itemname} for {totalCost}.");
        }
        else
        {
            Debug.Log("Not enough money to complete the purchase.");
        }
    }
}