using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterCreate : MonoBehaviour//, IDataPersistence
{
    public Button CreateButton;

    public TMP_InputField NameInput;

    public string Name;
    private void Start()
    {

    }

    public void CreateCharacterClick()
    {
        Name = NameInput.text;
        //DataPersistenceManager.Instance.SaveGame();

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
