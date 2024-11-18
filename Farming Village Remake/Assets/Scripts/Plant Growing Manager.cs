using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowingManager : MonoBehaviour
{
    public TileManager _tileManager;
    public InventoryManager inventoryManager;
    public PlantData plantData;
    private int currentGrowthDay = 0;
    private int currentStageIndex = 0;
    private SpriteRenderer spriteRenderer;

    private bool playerInRange = false;

    void Start()
    {
        if (inventoryManager == null)
        {
            _tileManager = FindObjectOfType<TileManager>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (inventoryManager == null)
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
        }
        UpdatePlantStage();
    }

    public void GrowPlant()
    {
        currentGrowthDay++;

        if (currentGrowthDay >= plantData.dayGrowing * (currentStageIndex + 1) && currentStageIndex < plantData.GrowthStages.Count - 1)
        {
            currentStageIndex++;
            UpdatePlantStage();
        }
    }

    void UpdatePlantStage()
    {
        if (plantData.GrowthStages != null && plantData.GrowthStages.Count > 0 && currentStageIndex < plantData.GrowthStages.Count)
        {
            spriteRenderer.sprite = plantData.GrowthStages[currentStageIndex];
        }
        else
        {
            Debug.LogWarning("Growth Stages are not properly assigned in PlantData.");
        }
    }

    public void OnDayPassed()
    {
        GrowPlant();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentStageIndex == plantData.GrowthStages.Count - 1)
        {
            playerInRange = true;
            HighlightSprite(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerInRange)
        {
            playerInRange = false;
            HighlightSprite(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HarvestPlant();
        }
    }

    void HighlightSprite(bool enable)
    {
        spriteRenderer.material.SetFloat("_OutlineEnabled", enable ? 1f : 0f);
    }

    public void HarvestPlant()
    {
        if (playerInRange && currentStageIndex == plantData.GrowthStages.Count - 1)
        {
            Debug.Log("Plant harvested!" + plantData.Product);
            AddProduct();
            Destroy(gameObject);
            _tileManager.HandleRemovingTile();
        }
    }

    void AddProduct()
    {
        bool result = inventoryManager.AddItem(plantData.Product);

        if (result == true)
        {
            Debug.Log("Item Added: " + plantData.Product);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }
}
