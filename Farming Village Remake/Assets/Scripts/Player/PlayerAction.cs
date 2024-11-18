using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerAction : MonoBehaviour
{
    private TileManager _tileManager;
    private ItemManager _ItemManager;

    [SerializeField] private GameObject _inventory;

    public InputActionReference inventoryKey;
    public InputActionReference move;
    public InputActionReference Sprint;
    public InputActionReference Menu;
    public InputActionReference LeftClick;

    void Start()
    {
        if (_inventory != null)
            _inventory.SetActive(false);

        if (_tileManager == null)
        {
            _tileManager = FindObjectOfType<TileManager>();
        }

        if (_ItemManager == null)
        {
            _ItemManager = FindObjectOfType<ItemManager>();
        }
    }
    private void OnEnable()
    {
        if (inventoryKey != null)
            inventoryKey.action.performed += ToggleInventory;

        if (LeftClick != null)
            LeftClick.action.performed += HandleLeftClick;
    }

    private void OnDisable()
    {
        if (inventoryKey != null)
            inventoryKey.action.performed -= ToggleInventory;

        if (LeftClick != null)
            LeftClick.action.performed -= HandleLeftClick;
    }

    void Update()
    {
        if (LeftClick.action.IsPressed())
        {   
            _ItemManager.PerformAction(ActionType.Plowing);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            _ItemManager.PerformAction(ActionType.Plowing);
            _ItemManager.PerformAction(ActionType.Digging);
            _ItemManager.PerformAction(ActionType.Watering);
            _ItemManager.SowingSeed();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _ItemManager.SowingSeed();
        }
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        if (_inventory != null)
            _inventory.SetActive(!_inventory.activeSelf);

        if (_inventory.activeSelf)
        {
            move.action.Disable();
            Sprint.action.Disable();
            LeftClick.action.Disable();
        }
        else
        {
            move.action.Enable();
            Sprint.action.Enable();
            LeftClick.action.Enable();
        }
    }

    private void HandleLeftClick(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _ItemManager.PerformAction(ActionType.Plowing);
        }
    }
}

