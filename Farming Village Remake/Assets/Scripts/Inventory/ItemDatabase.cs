using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }

    [SerializeField] private List<Item> allItems;   

    private Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one ItemDatabase in the scene.");
        }
        Instance = this;

        // Fill the dictionary with item names as keys and their item objects as values
        foreach (Item item in allItems)
        {
            itemDictionary.Add(item.name, item);
        }
    }

    public static Item GetItemByName(string itemName)
    {
        if (Instance.itemDictionary.TryGetValue(itemName, out Item item))
        {
            return item;
        }

        Debug.LogError("Item with name " + itemName + " not found in the database.");
        return null;
    }
}
