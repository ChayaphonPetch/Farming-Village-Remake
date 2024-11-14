using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPageManage : MonoBehaviour
{
    public List<GameObject> PageList = new List<GameObject>();
    public List<Button> ButtonList = new List<Button>();

    void Start()
    {
        for (int i = 0; i < ButtonList.Count; i++)
        {
            int index = i;
            ButtonList[i].onClick.AddListener(() => SetPage(index));
        }
    }

    void SetPage(int index)
    {
        for (int i = 0; i < PageList.Count; i++)
        {
            PageList[i].SetActive(false);
        }

        if (index >= 0 && index < PageList.Count)
        {
            PageList[index].SetActive(true);
        }
    }
}
