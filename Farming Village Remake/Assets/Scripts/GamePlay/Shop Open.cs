using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    private PlayerAction _playeraction;

    void Start()
    {
        if (_playeraction == null)
        {
            _playeraction = FindObjectOfType<PlayerAction>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playeraction != null)
            {
                _playeraction.isNearShop = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playeraction != null)
            {
                _playeraction.isNearShop = false;
            }
        }
    }
}
