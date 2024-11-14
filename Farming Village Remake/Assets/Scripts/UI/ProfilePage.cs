using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePage : MonoBehaviour
{
    public Image playerImage;
    public GameObject playerObject;

    void Start()
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
