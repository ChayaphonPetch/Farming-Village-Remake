using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WorldTimeManager : MonoBehaviour
{
    public int day;
    public int hours;
    public int minutes;
    public float timer;
    public string Seasons;
    public string Weathers;
    public float _gameminute = 12f;
    public VisualEffect[] Weathers_Effect;

    public DayNightData[] ResetTimeData;
    private TileManager _tilemanager;
    private ShopManager _shopmanager;
    private SellStorageManager _SellStorageManager;

    void Awake()
    {
        if (ResetTimeData != null && ResetTimeData.Length > 0)
        {
            DayNightData firstData = ResetTimeData[0];
            day = firstData.day;
            hours = firstData.hours;
            minutes = firstData.minutes;
            Seasons = firstData.Seasons.ToString();
            Weathers = firstData.Weathers.ToString();

            //Debug.Log($"Day: {firstData.day}, Hours: {firstData.hours}, Minutes: {firstData.minutes}");
            //Debug.Log($"Current Season: {firstData.Seasons}, Current Weather: {firstData.Weathers}");
        }
    }

    void Start()
    {
        _shopmanager = FindObjectOfType<ShopManager>();
        _SellStorageManager = FindObjectOfType<SellStorageManager>();
        _tilemanager = FindObjectOfType<TileManager>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= _gameminute)
        {
            TimeCycle(5);
            timer = 0f;
        }
    }

    void TimeCycle(int minuteAmount)
    {
        minutes += minuteAmount;

        if (minutes >= 60)
        {
            minutes = 0;
            hours++;

            if (hours >= 24)
            {
                hours = 0;
                day++;
                _SellStorageManager.SellItems();

                if (day > 31)
                {
                    day = 1;
                    ChangeSeason();
                    _shopmanager.SpawnBuySlots();
                }

                PlantGrowingManager[] allPlantManagers = FindObjectsOfType<PlantGrowingManager>();
                foreach (var plantManager in allPlantManagers)
                {
                    plantManager.OnDayPassed();
                }
                _tilemanager.ConvertAllWetSoiltoSoil();
                ChangeWeather();
            }
        }

        //Debug.Log($"Day: {day}, Hours: {hours}, Minutes: {minutes}");
    }

    void ChangeSeason()
    {
        Seasons = Seasons == "Summer" ? "Winter" : "Summer";
        //Debug.Log($"Season changed to: {Seasons}");
    }

    public void ChangeWeather()
    {
        int randomWeather = Random.Range(1, 101);
        if (Seasons == "Summer")
        {
            if (randomWeather <= 75)
            {
                Weathers = "Clear";
            }
            else if (randomWeather <= 85)
            {
                Weathers = "Foggy";
            }
            else
            {
                Weathers = "Rain";
                _tilemanager.ConvertAllSoiltoWetSoil();
            }
        }
        else if (Seasons == "Winter")
        {
            if (randomWeather <= 75)
            {
                Weathers = "Clear";
            }
            else if (randomWeather <= 85)
            {
                Weathers = "Foggy";
            }
            else
            {
                Weathers = "Snow Storm";
            }
        }

        UpdateWeatherEffects();
        //Debug.Log($"Weather changed to: {Weathers}");

    }

    void UpdateWeatherEffects()
    {
        foreach (var effect in Weathers_Effect)
        {
            if (effect != null)
            {
                effect.gameObject.SetActive(false);
            }
        }

        switch (Weathers)
        {
            case "Foggy":
                if (Weathers_Effect.Length > 0 && Weathers_Effect[0] != null)
                {
                    Weathers_Effect[0].gameObject.SetActive(true);
                }
                break;
            case "Rain":
                if (Weathers_Effect.Length > 1 && Weathers_Effect[1] != null)
                {
                    Weathers_Effect[1].gameObject.SetActive(true);
                }
                break;
            case "Snow Storm":
                if (Weathers_Effect.Length > 3 && Weathers_Effect[3] != null)
                {
                    // Activate the Snow Storm effect
                    Weathers_Effect[3].gameObject.SetActive(true);

                    // Deactivate Weathers_Effect[2] if Snow Storm is active
                    if (Weathers_Effect.Length > 2 && Weathers_Effect[2] != null)
                    {
                        Weathers_Effect[2].gameObject.SetActive(false);
                    }
                }
                break;
        }
        if (Seasons == "Winter")
        {
            if (Weathers_Effect.Length > 2 && Weathers_Effect[2] != null)
            {
                Weathers_Effect[2].gameObject.SetActive(true);
            }
        }
    }
}


    /*void UpdateDayNightIcon()
    {
        if (isAM && hours >= 6 && hours < 12)
        {
            DayNight_Icon.sprite = ResetTimeData[0].Day_Icon;
        }
        else if (!isAM && (hours >= 7 || hours < 6))
        {
            DayNight_Icon.sprite = ResetTimeData[0].Night_Icon;
        }
    }

    /*IEnumerator BlinkColon()
    {
        while (true)
        {
            colonVisible = !colonVisible;
            yield return new WaitForSeconds(1); 
        }
    }*/

