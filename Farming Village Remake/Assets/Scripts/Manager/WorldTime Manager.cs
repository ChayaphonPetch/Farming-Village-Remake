using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WorldTimeManager : MonoBehaviour
{
    public int day;
    public int hours;
    public int minutes;
    public bool isAM;
    public float timer;
    public string Seasons;
    public string Weathers;
    public float _gameminute = 12f;
    public VisualEffect[] Weathers_Effect;

    public DayNightData[] ResetTimeData;

    void Awake()
    {
        if (ResetTimeData != null && ResetTimeData.Length > 0)
        {
            DayNightData firstData = ResetTimeData[0];
            day = firstData.day;
            hours = firstData.hours;
            minutes = firstData.minutes;
            isAM = firstData.isAM;
            Seasons = firstData.Seasons.ToString();
            Weathers = firstData.Weathers.ToString();

            Debug.Log($"Day: {firstData.day}, Hours: {firstData.hours}, Minutes: {firstData.minutes}, AM/PM: {(firstData.isAM ? "AM" : "PM")}");
            Debug.Log($"Current Season: {firstData.Seasons}, Current Weather: {firstData.Weathers}");
        }
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

            if (hours == 12)
            {
                isAM = !isAM;
            }
            else if (hours > 12)
            {
                hours = 1;
            }

            if (isAM && hours == 12)
            {
                day++;

                if (day > 31)
                {
                    day = 1;
                    ChangeSeason();
                }
            }
            ChangeWeather();
        }

        Debug.Log($"Day: {day}, Hours: {hours}, Minutes: {minutes}, AM/PM: {(isAM ? "AM" : "PM")}");
    }

    void ChangeSeason()
    {
        Seasons = Seasons == "Summer" ? "Winter" : "Summer";
        Debug.Log($"Season changed to: {Seasons}");
    }

    void ChangeWeather()
    {
        int randomValue = Random.Range(1, 101);
        if (Seasons == "Summer")
        {
            if (randomValue <= 75)
            {
                Weathers = "Clear";
            }
            else if (randomValue <= 90)
            {
                Weathers = "Foggy";
            }
            else
            {
                Weathers = "Rain";
            }
        }
        else if (Seasons == "Winter")
        {
            if (randomValue <= 75)
            {
                Weathers = "Clear";
            }
            else if (randomValue <= 90)
            {
                Weathers = "Foggy";
            }
            else
            {
                Weathers = "Snow Storm";
            }
        }

        UpdateWeatherEffects();
        Debug.Log($"Weather changed to: {Weathers}");

    }

    void UpdateWeatherEffects()
    {
        // Disable all weather effects first
        foreach (var effect in Weathers_Effect)
        {
            if (effect != null)
            {
                effect.gameObject.SetActive(false);
            }
        }

        // Activate the correct weather effect based on the current weather
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
            /*case "Snow Storm":
                if (Weathers_Effect.Length > 2 && Weathers_Effect[2] != null)
                {
                    Weathers_Effect[2].gameObject.SetActive(true);
                }
                break*/
                // Add more cases if you have additional weather effects
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

