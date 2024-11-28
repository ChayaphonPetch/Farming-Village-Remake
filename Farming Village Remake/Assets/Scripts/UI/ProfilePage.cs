using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfilePage : MonoBehaviour
{
    public Image playerImage;
    public GameObject playerObject;

    public TextMeshProUGUI PlayTime;

    private PlayerData playerData;
    private UIManager UIManager;
    private PlayerProfileManager playerProfileManager;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        UIManager = FindObjectOfType<UIManager>();
        playerProfileManager = PlayerProfileManager.Instance;
    }

    private void OnEnable()
    {
        UpdatePlayerProfile();
        if (playerProfileManager != null)
        {
            playerProfileManager.UpdatePlayTime(PlayTime);
        }
    }

    private void Update()
    {
        UpdatePlayerProfile();
        playerProfileManager.UpdatePlayTime(PlayTime);
    } 

    void UpdatePlayerProfile()
    {
        UpdateImageProfile();
    }

    void UpdateImageProfile()
    {
        if (playerImage != null && playerObject != null)
        {
            SpriteRenderer playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer != null)
            {
                playerImage.sprite = playerSpriteRenderer.sprite;
            }
            else
            {
                Debug.LogError("SpriteRenderer is not found on the assigned GameObject.");
            }
        }
        else
        {
            //Debug.LogError("Player Image or Player Object is not assigned.");
        }
    }
}
