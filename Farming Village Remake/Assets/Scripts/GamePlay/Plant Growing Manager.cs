using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantGrowingManager : MonoBehaviour
{
    public TileManager _tileManager;
    public InventoryManager inventoryManager;
    public WorldTimeManager worldTimeManager;
    public PlantData plantData;
    private int currentGrowthDay = 0;
    private int currentStageIndex = 0;
    private SpriteRenderer spriteRenderer;

    private bool playerInRange = false;
    private Vector3Int plantCellPosition;

    void Start()
    {
        if (_tileManager == null)
        {
            _tileManager = FindObjectOfType<TileManager>();
        }

        if (inventoryManager == null)
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
        }

        if (worldTimeManager == null)
        {
            worldTimeManager = FindObjectOfType<WorldTimeManager>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        Tilemap plowingMap = _tileManager.PlowingMap;
        plantCellPosition = plowingMap.WorldToCell(transform.position);
        UpdatePlantStage();
    }

    public void GrowPlant()
    {
        TileBase currentTile = _tileManager.PlowingMap.GetTile(plantCellPosition);
        if (currentTile != _tileManager.WetPlowing_Soil)
        {
            Debug.Log("Plant cannot grow. Tile is not WetPlowing_Soil.");
            return;
        }

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
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
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
            Debug.Log("Plant harvested! Product: " + plantData.Product);
            AddProduct();
            Destroy(gameObject);
            _tileManager.HandleRemovingTile();
        }
    }

    void AddProduct()
    {
        bool result = inventoryManager.AddItem(plantData.Product);

        if (result)
        {
            Debug.Log("Item Added: " + plantData.Product);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }
}
