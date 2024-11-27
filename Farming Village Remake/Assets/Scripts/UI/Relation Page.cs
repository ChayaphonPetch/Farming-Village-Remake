using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class RelationPage : MonoBehaviour
{
    [SerializeField] private Image[] npcImage;
    [SerializeField] private TextMeshProUGUI[] npcName;

    [SerializeField] private Image[] MarinRelation;

    [SerializeField] private Image[] AliceRelation;

    [SerializeField] private Image[] AlexRelation;

    [SerializeField] private Image[] Old_ManRelation;


    [SerializeField] private Sprite EmptyHeart, HalfHeart, FullHeart;
    private NPCData _npcdata;

    void Start()
    {
        _npcdata = FindObjectOfType<NPCData>();
    }

    void Update()
    {
        CharacterRelationUpdate();
    }

    public void CharacterUIUpdate(int index)
    {
        if (index >= 0 && index < npcImage.Length)
        {
            npcImage[index].color = Color.white;

            switch (index)
            {
                case 0:
                    npcName[0].text = "Umi Marin";
                    break;
                case 1:
                    npcName[1].text = "Alice Lovelace";
                    break;
                case 2:
                    npcName[2].text = "Alex Schwarz";
                    break;
                case 3:
                    npcName[3].text = "Old Man";
                    break;
            }
        }
    }

    public void CharacterRelationUpdate()
    {
        UpdateRelation(_npcdata.Marin_current_relationship, _npcdata.Marin_Max_relationship, _npcdata.Marin_gift_receive, MarinRelation);
        UpdateRelation(_npcdata.Alice_current_relationship, _npcdata.Alice_Max_relationship, _npcdata.Alice_gift_receive, AliceRelation);
        UpdateRelation(_npcdata.Alex_current_relationship, _npcdata.Alex_Max_relationship, _npcdata.Alex_gift_receive, AlexRelation);
        UpdateRelation(_npcdata.Old_Man_current_relationship, _npcdata.Old_Man_relationship, _npcdata.Old_Man_gift_receive, Old_ManRelation);
    }

    private void UpdateRelation(int currentRelationship, int maxRelationship, int giftreceive, Image[] relations)
    {
        maxRelationship = Mathf.Clamp(maxRelationship, 0, relations.Length);
        currentRelationship = Mathf.Clamp(currentRelationship, 0, maxRelationship);

        switch (currentRelationship)
        {
            case 0:
                if (giftreceive == 2)
                {
                    relations[0].sprite = HalfHeart;
                }
                break;

            case 1:
                if (giftreceive == 0)
                {
                    relations[0].sprite = FullHeart;
                }
                if (giftreceive == 2)
                {
                    relations[1].sprite = HalfHeart;
                }
                break;

            case 2:
                if (giftreceive == 0)
                {
                    relations[1].sprite = FullHeart;
                }
                if (giftreceive == 2)
                {
                    relations[2].sprite = HalfHeart;
                }
                break;

            case 3:
                if (giftreceive == 0)
                {
                    relations[2].sprite = FullHeart;
                }
                if (giftreceive == 2)
                {
                    relations[3].sprite = HalfHeart;
                }
                break;

            case 4:
                if (giftreceive == 0)
                {
                    relations[3].sprite = FullHeart;
                }
                if (giftreceive == 2)
                {
                    relations[4].sprite = HalfHeart;
                }
                break;

            case 5:
                relations[4].sprite = FullHeart;
                break;

            default:
                Debug.LogWarning("Invalid relationship level provided: " + currentRelationship);
                break;
        }

        for (int i = 0; i < relations.Length; i++)
        {
            relations[i].gameObject.SetActive(i < maxRelationship);
        }
    }
}
