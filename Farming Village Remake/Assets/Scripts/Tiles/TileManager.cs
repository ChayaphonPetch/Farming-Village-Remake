using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] public Tilemap interactableMap;
    [SerializeField] public Tilemap PlowingMap;

    [SerializeField] public Tile Plowing_Soil;
    [SerializeField] public Tile WetPlowing_Soil;

    public GameObject PlantPrefab;

    private Collider2D playerCollider;

    public InventoryManager inventoryManager;
    void Start()
    {
        //HideAllTiles();
        if (Player != null)
        {
            playerCollider = Player.GetComponent<Collider2D>();
            if (playerCollider == null)
            {
                //Debug.LogError("Player object does not have a Collider component attached.");
            }
        }
    }

    public void HideAllTiles()
    {
        if (interactableMap != null)
        {
            BoundsInt bounds = interactableMap.cellBounds;

            foreach (Vector3Int position in bounds.allPositionsWithin)
            {
                TileBase tile = interactableMap.GetTile(position);
                if (tile != null)
                {
                    interactableMap.SetTile(position, null);
                }
            }

            //Debug.Log("All Interactable Plow tiles are now hidden.");
        }
    }

    public void HandlePlowingTile()
    {
        if (Player != null && playerCollider != null)
        {
            Vector3 playerPosition = playerCollider.bounds.center;

            Vector3Int cellPosition = interactableMap.WorldToCell(playerPosition);
            Debug.Log(playerPosition);
            Debug.Log(cellPosition);

            if (interactableMap.GetTile(cellPosition) != null)
            {
                TileBase currentTile = PlowingMap.GetTile(cellPosition);
                if (currentTile != Plowing_Soil && currentTile != WetPlowing_Soil)
                {
                    PlowingMap.SetTile(cellPosition, Plowing_Soil);
                    //Debug.Log("Plowing Soil tile placed at position: " + cellPosition);
                }
                else
                {
                    //Debug.Log("Tile already has Plowing_Soil or WetPlowing_Soil, no change made.");
                }
            }
        }
    }

    public void HandleRemovingTile()
    {
        if (Player != null && playerCollider != null)
        {
            Vector3 playerPosition = playerCollider.bounds.center;

            Vector3Int cellPosition = interactableMap.WorldToCell(playerPosition);
            Debug.Log(playerPosition);
            Debug.Log(cellPosition);

            if (interactableMap.GetTile(cellPosition) != null)
            {
                TileBase currentTile = PlowingMap.GetTile(cellPosition);
                if (currentTile == Plowing_Soil || currentTile == WetPlowing_Soil)
                {
                    PlowingMap.SetTile(cellPosition, null);
                    //Debug.Log("Tile removed at position: " + cellPosition);
                }
                else
                {
                    //Debug.Log("Tile is not Plowing_Soil or WetPlowing_Soil, no change made.");
                }
            }
        }
    }

    public void HandleWateringTile()
    {
        if (Player != null && playerCollider != null)
        {
            Vector3 playerPosition = playerCollider.bounds.center;
            Vector3Int cellPosition = interactableMap.WorldToCell(playerPosition);

           // Debug.Log("Player Position: " + playerPosition);
            //Debug.Log("Cell Position: " + cellPosition);

            TileBase currentTile = PlowingMap.GetTile(cellPosition);

            if (currentTile != null)
            {
                if (currentTile == Plowing_Soil || currentTile == WetPlowing_Soil)
                {
                    PlowingMap.SetTile(cellPosition, WetPlowing_Soil);
                    //Debug.Log("Watering at position: " + cellPosition);
                }
                else
                {
                    //Debug.Log("Tile is not Plowing_Soil or WetPlowing_Soil, no change made.");
                }
            }
            else
            {
                //Debug.Log("No tile found at the given position.");
            }
        }
    }

    public void SpawnPlantinTiles(GameObject plantPrefab)
    {
        if (Player != null && playerCollider != null)
        {
            Vector3 playerPosition = playerCollider.bounds.center;
            Vector3Int cellPosition = interactableMap.WorldToCell(playerPosition);

            TileBase currentTile = PlowingMap.GetTile(cellPosition);
            if (currentTile == Plowing_Soil || currentTile == WetPlowing_Soil)
            {
                Vector3 spawnPosition = PlowingMap.GetCellCenterWorld(cellPosition);

                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.1f);
                bool plantExists = false;

                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Plant"))
                    {
                        plantExists = true;
                        break;
                    }
                }

                if (!plantExists)
                {
                    Instantiate(plantPrefab, spawnPosition, Quaternion.identity);
                    inventoryManager.GetSelectedItem(true);
                }
                else
                {
                    inventoryManager.GetSelectedItem(false);
                }
            }
            else
            {
                inventoryManager.GetSelectedItem(false);
            }
        }
    }

    public void ConvertAllWetSoiltoSoil()
    {
        if (PlowingMap != null)
        {
            BoundsInt bounds = PlowingMap.cellBounds;

            foreach (Vector3Int position in bounds.allPositionsWithin)
            {
                TileBase tile = PlowingMap.GetTile(position);
                if (tile == WetPlowing_Soil)
                {
                    PlowingMap.SetTile(position, Plowing_Soil);
                }
            }

            //Debug.Log("All WetPlowing_Soil tiles have been converted to Plowing_Soil.");
        }
    }

    public void ConvertAllSoiltoWetSoil()
    {
        if (PlowingMap != null)
        {
            BoundsInt bounds = PlowingMap.cellBounds;

            foreach (Vector3Int position in bounds.allPositionsWithin)
            {
                TileBase tile = PlowingMap.GetTile(position);
                if (tile == Plowing_Soil)
                {
                    PlowingMap.SetTile(position, WetPlowing_Soil);
                }
            }

            //Debug.Log("All WetPlowing_Soil tiles have been converted to Plowing_Soil.");
        }
    }

}
