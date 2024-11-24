using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fainted : MonoBehaviour
{
    private PlayerData _playerData;
    public GameObject player;
    public GameObject Respawn;

    void Start()
    {
        _playerData = FindObjectOfType<PlayerData>();
        if (_playerData == null)
        {
            Debug.LogError("PlayerData component is missing!");
        }
    }

    void Update() // Changed to Update() to constantly check the player's stamina
    {
        CheckIfFainted();
    }

    void CheckIfFainted()
    {
        if (_playerData.current_stamina <= 0)
        {
            HandleFaint();
        }
    }

    void HandleFaint()
    {
        Debug.Log("Player has fainted!");
        _playerData.current_money = (int)(_playerData.current_money * 0.75f);
        _playerData.current_stamina = 50;

        if (Respawn != null)
        {
            player.transform.position = Respawn.transform.position;
        }
        else
        {
            Debug.LogWarning("Respawn point is not assigned!");
        }
    }
}
