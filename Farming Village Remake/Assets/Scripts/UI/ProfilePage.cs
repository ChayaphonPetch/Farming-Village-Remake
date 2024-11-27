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

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    private void Update()
    {
        if (playerData != null)
        {
            playerData.playtime += Time.deltaTime;
        }
        UpdatePlayerProfile();
        UpdatePlayTime();
    }

    void UpdatePlayerProfile()
    {
        UpdateImageProfile();
    }

    void UpdatePlayTime()
    {
        if (PlayTime != null)
        {
            int hours = Mathf.FloorToInt(playerData.playtime / 3600);
            int minutes = Mathf.FloorToInt((playerData.playtime % 3600) / 60);
            int seconds = Mathf.FloorToInt(playerData.playtime % 60);

            PlayTime.text = string.Format("Play Time : {0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
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
