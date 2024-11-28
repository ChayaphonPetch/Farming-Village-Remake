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

    private void UpdateRelation(int currentRelationship, int maxRelationship, int giftReceive, Image[] relations)
    {
        maxRelationship = Mathf.Clamp(maxRelationship, 0, relations.Length);
        currentRelationship = Mathf.Clamp(currentRelationship, 0, maxRelationship);

        // Update hearts based on relationship level and gift received
        for (int i = 0; i < relations.Length; i++)
        {
            if (i < currentRelationship)
            {
                relations[i].sprite = FullHeart; // Full heart for current relationship level
            }
            else if (i == currentRelationship && giftReceive == 2)
            {
                relations[i].sprite = HalfHeart; // Half heart if gift received and it's the next level
            }
            else
            {
                relations[i].sprite = EmptyHeart; // You may want an EmptyHeart sprite for unused hearts
            }
        }

        // Set active hearts up to the maximum relationship level
        for (int i = 0; i < relations.Length; i++)
        {
            relations[i].gameObject.SetActive(i < maxRelationship);
        }
    }
}
