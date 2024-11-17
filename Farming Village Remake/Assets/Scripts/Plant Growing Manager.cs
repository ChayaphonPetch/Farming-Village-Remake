using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowingManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public PlantData plantData;
    private int currentGrowthDay = 0;
    private int currentStageIndex = 0;
    private SpriteRenderer spriteRenderer;

    private bool playerInRange = false;

    void Start()
    {
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
        if (playerInRange && currentStageIndex == plantData.GrowthStages.Count - 1 && Input.GetKeyDown(KeyCode.E))
        {
            HarvestPlant();
        }
    }

    void HighlightSprite(bool enable)
    {
        if (enable)
        {
            spriteRenderer.material.SetFloat("_OutlineEnabled", 1f);
        }
        else
        {
            spriteRenderer.material.SetFloat("_OutlineEnabled", 0f);
        }
    }

    void HarvestPlant()
    {
        Debug.Log("Plant harvested!" + plantData.Product);
        AddProduct();
        Destroy(gameObject);
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
