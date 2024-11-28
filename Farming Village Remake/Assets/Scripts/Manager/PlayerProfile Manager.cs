using UnityEngine;
using TMPro;

public class PlayerProfileManager : MonoBehaviour
{
    public static PlayerProfileManager Instance;

    public float playtime { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        playtime += Time.deltaTime;
    }

    public void UpdatePlayTime(TextMeshProUGUI PlayTime)
    {
        if (PlayTime != null)
        {
            int hours = Mathf.FloorToInt(playtime / 3600);
            int minutes = Mathf.FloorToInt((playtime % 3600) / 60);
            int seconds = Mathf.FloorToInt(playtime % 60);

            PlayTime.text = string.Format("Play Time : {0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }
}
