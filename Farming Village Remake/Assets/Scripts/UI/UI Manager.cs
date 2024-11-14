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

    [Header("Starina")]
    public Slider StaminaSlider;

    private WorldTimeManager worldtimeManager;
    private PlayerStats playerStats;

    void Start()
    {
        worldtimeManager = FindObjectOfType<WorldTimeManager>();

        if (worldtimeManager == null)
        {
            Debug.LogError("TimeManager not found in the scene.");  
        }

        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats not found in the scene.");
        }

    }

    void Update()
    {
        if (worldtimeManager != null)
        {
            UpdateTimeDisplay();
        }
    }

    void UpdateTimeDisplay()
    {
        string formattedMinutes = worldtimeManager.minutes < 10 ? "0" + worldtimeManager.minutes : worldtimeManager.minutes.ToString();
        string period = worldtimeManager.isAM ? "AM" : "PM";

        TimeText.text = $"{worldtimeManager.hours} : {formattedMinutes} {period}";
        DayText.text = $"Day {worldtimeManager.day}";
        SeasonText.text = worldtimeManager.Seasons;
        WeatherText.text = worldtimeManager.Weathers;
        //DayNight_Icon.Image = worldtimeManager.DayNight_Icon;   

    }

}
