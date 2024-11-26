using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollactionPage : MonoBehaviour
{
    private NPCData _npcdata;

    public GameObject[] CollactionItem;

    void Start()
    {
        _npcdata = FindObjectOfType<NPCData>();
    }

    void Update()
    {
        CollactionCheck();
    }

    void CollactionCheck()
    {
        if (_npcdata != null)
        {
            CollactionItem[0].GetComponent<Image>().color = _npcdata.isMarinTotem ? Color.white : Color.black;
            CollactionItem[1].GetComponent<Image>().color = _npcdata.isAliceTotem ? Color.white : Color.black;
            CollactionItem[2].GetComponent<Image>().color = _npcdata.isAlexTotem ? Color.white : Color.black;
            CollactionItem[3].GetComponent<Image>().color = _npcdata.isOld_ManTotem ? Color.white : Color.black;
        }
    }
}
