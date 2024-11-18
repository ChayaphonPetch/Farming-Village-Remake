using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Time Text")]
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI DayText;
    public TextMeshProUGUI SeasonText;
    public TextMeshProUGUI WeatherText;

    [Header("Money Text")]
    public TextMeshProUGUI CoinText;

    [Header("Sprite")]
    public Image DayNight_Icon;

    [Header("Stamina")]
    public Slider StaminaSlider;
    public TextMeshProUGUI StaminaText;

    private WorldTimeManager worldtimeManager;
    private PlayerData playerData;

    void Start()
    {
        worldtimeManager = FindObjectOfType<WorldTimeManager>();
        playerData = FindObjectOfType<PlayerData>();

        if (worldtimeManager == null)
        {
            Debug.LogError("TimeManager not found in the scene.");  
        }
        if (playerData == null)
        {
            Debug.LogError("TimeManager not found in the scene.");
        }
    }

    void Update()
    {
        if (worldtimeManager != null)
        {
            UpdateTimeDisplay();
        }

        if (playerData != null)
        {
            UpdateMoneyDisplay();
        }
        UpdateStarminaDisplay();
    }

    void UpdateTimeDisplay()
    {
        string formattedMinutes = worldtimeManager.minutes < 10 ? "0" + worldtimeManager.minutes : worldtimeManager.minutes.ToString();

        TimeText.text = $"{worldtimeManager.hours} : {formattedMinutes}";
        DayText.text = $"Day {worldtimeManager.day}";
        SeasonText.text = worldtimeManager.Seasons;
        WeatherText.text = worldtimeManager.Weathers;
        //DayNight_Icon.Image = worldtimeManager.DayNight_Icon;   

    }

    void UpdateMoneyDisplay()
    {
        CoinText.text = $"{ playerData.current_money}";
    }

    void UpdateStarminaDisplay()
    {
        StaminaSlider.value = playerData.current_stamina;
        StaminaText.text = $"{playerData.current_stamina}";
    }

}
