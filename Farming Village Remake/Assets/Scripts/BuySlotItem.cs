using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BuySlotItem : MonoBehaviour
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI itemprice;

    public TextMeshProUGUI totalprice;
    public TMP_InputField amount;
    public TextMeshProUGUI Discount;
    public TextMeshProUGUI DayGrown;
    public Button buybutton;

    [HideInInspector] public Item item;
    private PlayerData playerdata;
    private InventoryManager inventoryManager;
    private NPCData npcdata;

    private void Start()
    {
        playerdata = FindObjectOfType<PlayerData>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        npcdata = FindObjectOfType<NPCData>();
        amount.contentType = TMP_InputField.ContentType.IntegerNumber;
        amount.text = "1";
        ValidateAmount();
        amount.onValueChanged.AddListener(delegate { ValidateAmount(); });
        amount.onEndEdit.AddListener(delegate { ValidateAmount(); });
        buybutton.onClick.AddListener(BuyItem);
    }

    public void InitialiseItem(Item ShopItem)
    {
        item = ShopItem;
        image.sprite = ShopItem.image;
        itemname.text = ShopItem.itemname;
        itemprice.text = ShopItem.price.ToString();
        DayGrown.text = "Day Grown " + ShopItem.DayGrown.ToString();
    }

    private void BuyItem()
    {
        if (int.TryParse(amount.text, out int value) && value > 0)
        {
            int itemPrice = npcdata.isOld_ManTotem ? item.price * 9 / 10 : item.price;
            int totalCost = itemPrice * value;  

            if (playerdata.current_money >= totalCost)
            {
                playerdata.current_money -= totalCost;

                bool result = false;
                for (int i = 0; i < value; i++)
                {
                    result = inventoryManager.AddItem(item);
                    if (!result)
                    {
                        Debug.Log("Inventory is full!!");
                        break;
                    }
                }

                if (result)
                {
                    Debug.Log("Item Added");
                    Debug.Log($"Purchased {value} of {item.itemname} for {totalCost}.");
                }
            }
            else
            {
                Debug.Log("Not enough money to complete the purchase.");
            }
        }
        else
        {
            Debug.Log("Invalid quantity to buy.");
        }
    }

    private void ValidateAmount()
    {
        if (int.TryParse(amount.text, out int value))
        {
            if (value > 64)
            {
                value = 64;
                amount.text = value.ToString();
            }
            else if (value < 1)
            {
                value = 1;
                amount.text = value.ToString();
            }
            UpdateTotalPrice(value);
        }
        else
        {
            amount.text = "1";
            UpdateTotalPrice(1);
        }
    }

    private void UpdateTotalPrice(int quantity)
    {
        int itemPrice = npcdata.isOld_ManTotem ? item.price * 9 / 10 : item.price;

        totalprice.text = (itemPrice * quantity).ToString();
    }
}
