using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro.Examples;

public class PlayerAction : MonoBehaviour
{
    private TileManager _tileManager;
    private ItemManager _ItemManager;
    private PlayerMovement _playerMovement;

    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _sellstorage;
    [SerializeField] private GameObject _storage;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _player_ui;
    [SerializeField] private GameObject _toolbar_ui;
    [SerializeField] private GameObject _player;

    public bool isNearSellStorage = false;
    public bool isNearStorage = false;
    public bool isNearShop = false;

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

        if (_playerMovement == null)
        {
            _playerMovement = FindObjectOfType<PlayerMovement>();
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

        if (_ItemManager == null)
        {

        }

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleSellStorage();
            ToggleStorage();
            ToggleDialogue();
        }
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {   
        if (_inventory != null)
            _inventory.SetActive(!_inventory.activeSelf);
            _player_ui.SetActive(!_player_ui.activeSelf);

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

    private void ToggleSellStorage()
    {
        if (_sellstorage != null && isNearSellStorage == true)
        {
            _sellstorage.SetActive(!_sellstorage.activeSelf);
            _player_ui.SetActive(!_player_ui.activeSelf);

            if (_sellstorage.activeSelf)
            {
                SetInputState(false);
            }
            else
            {
                SetInputState(true);
            }
        }
        else
        {
            //Debug.Log("Sell storage is null or player is not near sell storage.");
        }
    }

    private void ToggleStorage()
    {
        if (_storage != null && isNearStorage == true)
        {
            _storage.SetActive(!_storage.activeSelf);
            _player_ui.SetActive(!_player_ui.activeSelf);

            if (_storage.activeSelf)
            {
                SetInputState(false);
            }
            else
            {
                SetInputState(true);
            }
        }
        else
        {
            //Debug.Log("Sell storage is null or player is not near sell storage.");
        }
    }

    public void ToggleShop()
    {
        if (_shop != null && isNearShop == true)
        {
            _shop.SetActive(!_shop.activeSelf);
            _toolbar_ui.SetActive(!_toolbar_ui.activeSelf);
            //_player_ui.SetActive(!_player_ui.activeSelf);

            if (_shop.activeSelf)
            {
                SetInputState(false);
            }
            else
            {
                SetInputState(true);
            }
        }
        else
        {
            //Debug.Log("Shop is null or player is not near Shop.");
        }
    }

    private void ToggleDialogue()
    {
            _playerMovement.InteractDialogue();

        if (_dialogueBox.activeSelf)
            {
                _player_ui.SetActive(!_player_ui.activeSelf);
                _toolbar_ui.SetActive(!_toolbar_ui.activeSelf);
                SetInputState(false);
            }
            else
            {
                SetInputState(true);
            }
    }


    private void HandleLeftClick(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _ItemManager.PerformAction(ActionType.Plowing);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sell Storage"))
        {
            isNearSellStorage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sell Storage"))
        {
            isNearSellStorage = false;
        }
    }

    public void EnableKey()
    {
        move.action.Enable();
        Sprint.action.Enable();
        LeftClick.action.Enable();
        inventoryKey.action.Enable();
        _player_ui.SetActive(!_player_ui.activeSelf);
    }

    public void SetInputState(bool enabled)
    {
        if (enabled)
        {
            move.action.Enable();
            Sprint.action.Enable();
            LeftClick.action.Enable();
            inventoryKey.action.Enable();
        }
        else
        {
            move.action.Disable();
            Sprint.action.Disable();
            LeftClick.action.Disable();
            inventoryKey.action.Disable();
        }
    }

    public void ActiveUI()
    {
        _player_ui.SetActive(!_player_ui.activeSelf);
        _toolbar_ui.SetActive(!_toolbar_ui.activeSelf);
    }
}

