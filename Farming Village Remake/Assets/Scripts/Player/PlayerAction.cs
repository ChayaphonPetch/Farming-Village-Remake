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
    private PlayerData _playerdata;
    private UIManager _uimanager;

    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _sellstorage;
    [SerializeField] private GameObject _storage;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _player_ui;
    [SerializeField] private GameObject _toolbar_ui;
    [SerializeField] private GameObject _menu_ui;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _Lenten;

    public bool isNearSellStorage = false;
    public bool isNearStorage = false;
    public bool isNearShop = false;

    public InputActionReference inventoryKey;
    public InputActionReference move;
    public InputActionReference Sprint;
    public InputActionReference Menu;
    public InputActionReference LeftClick;
    public InputActionReference Interact;
    public InputActionReference Lenten;

    void Start()    
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1.0f;

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

        if (_playerdata == null)
        {
            _playerdata = FindObjectOfType<PlayerData>();
        }

        if (_uimanager == null)
        {
            _uimanager = FindObjectOfType<UIManager>();
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

        if (Menu.action.WasPressedThisFrame())
        {
            Resume();
        }
        
        if (Lenten.action.WasPressedThisFrame())
        {
            LentenOnOff();
        }

            if (Interact.action.IsPressed())
        {
            ToggleDialogue();
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
                move.action.Disable();
                Sprint.action.Disable();
                LeftClick.action.Disable();
                inventoryKey.action.Disable();
            }
            else
            {
                move.action.Enable();
                Sprint.action.Enable();
                LeftClick.action.Enable();
                inventoryKey.action.Enable();
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
                move.action.Disable();
                Sprint.action.Disable();
                LeftClick.action.Disable();
                inventoryKey.action.Disable();
            }
            else
            {
                move.action.Enable();
                Sprint.action.Enable();
                LeftClick.action.Enable();
                inventoryKey.action.Enable();
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
            _toolbar_ui.SetActive(false);
            _player_ui.SetActive(true);

            if (_shop.activeSelf)
            {
                move.action.Disable();
                Sprint.action.Disable();
                LeftClick.action.Disable();
                inventoryKey.action.Disable();
                Interact.action.Disable();
            }
            else
            {
                move.action.Enable();
                Sprint.action.Enable();
                LeftClick.action.Enable();
                inventoryKey.action.Enable();
                Interact.action.Enable();
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
            move.action.Disable();
            Sprint.action.Disable();
            LeftClick.action.Disable();
            inventoryKey.action.Disable();
            Interact.action.Disable();
        }
            else
            {
            move.action.Enable();
            Sprint.action.Enable();
            LeftClick.action.Enable();
            inventoryKey.action.Enable();
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
        Interact.action.Enable();
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

    public void EnableLock()
    {
        move.action.Enable();
        Sprint.action.Enable();
        LeftClick.action.Enable();
        inventoryKey.action.Enable();
        Interact.action.Enable();
    }

    public void Resume()
    {
            _menu_ui.SetActive(!_menu_ui.activeSelf);

            if (_menu_ui.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
    }

    public void LentenOnOff()
    {
        foreach (ItemUpgrade upgrade in _playerdata.purchasedUpgrades)
        {
            if (upgrade.name == "Lenten")
            {
                _Lenten.SetActive(!_Lenten.activeSelf);

                _uimanager.Lenten_Icon.gameObject.SetActive(_Lenten.activeSelf);

                return;
            }
        }

        _Lenten.SetActive(false);
        _uimanager.Lenten_Icon.gameObject.SetActive(false);
    }


}

