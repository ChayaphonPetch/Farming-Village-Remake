using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterCreate : MonoBehaviour//, IDataPersistence
{
    public Button CreateButton;

    public TMP_InputField NameInput;

    private PlayerData playerData;

    private void Awake()
    {
        playerData = FindObjectOfType<PlayerData>();
        if (playerData == null)
        {
            Debug.LogError("PlayerData instance not found in the scene! Make sure there is a GameObject with the PlayerData script attached.");
        }
    }

    public void CreateCharacter()
    {
        string playerName = NameInput.text;
        Debug.Log(playerName);
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name cannot be empty!");
            return;
        }

        playerData.Player_name = playerName;

        Debug.Log("Character created with name: " + playerName);

        // You can add code here to save game data if necessary
        // DataPersistenceManager.Instance.SaveGame();
    }

    /*public void LoadData(GameData data)
    {
        this.Name = data._Name;
        NameInput.text = this.Name;
    }

    public void SaveData(ref GameData data)
    {
        data._Name = this.Name;
    }*/
}
