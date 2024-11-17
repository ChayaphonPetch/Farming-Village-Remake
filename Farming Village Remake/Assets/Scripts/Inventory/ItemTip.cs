using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTip : MonoBehaviour
{
    [HideInInspector] public Item item;

    public TextMeshProUGUI itemTipName;       
    public TextMeshProUGUI itemTipPrice;      
    public TextMeshProUGUI itemTipDescription;
    public Image itemTipImage;                

    public GameObject tooltipUI;

    public Vector3 offset = new Vector3(150f, -100f, 0f);

    public void ShowItemTip(Item item)
    {
        if (item != null)
        {
            tooltipUI.SetActive(true);

            itemTipName.text = item.name;
            itemTipPrice.text = item.sellable ? "Price: " + item.price.ToString() + " Coins" : "Not Sellable";
            itemTipDescription.text = $"Type: {item.type}\nAction: {item.actionType}";

            if (itemTipImage != null && item.image != null)
            {
                itemTipImage.sprite = item.image;
            }
        }
    }

    public void HideItemTip()
    {
        tooltipUI.SetActive(false);

        itemTipName.text = "";
        itemTipPrice.text = "";
        itemTipDescription.text = "";
        if (itemTipImage != null)
        {
            itemTipImage.sprite = null;
        }
    }

    private void Update()
    {
        if (tooltipUI.activeSelf)
        {
            Vector3 tooltipPosition = Input.mousePosition + offset;
            tooltipPosition.z = 0f;
            tooltipUI.transform.position = tooltipPosition;
        }
    }
}
