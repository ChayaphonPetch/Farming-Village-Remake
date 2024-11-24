    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    private WorldTimeManager worldtimeManager;
    [SerializeField] private Light2D _light;
    [Header("Summer Light")]
    [SerializeField] private Gradient summer_gradient;
    [Header("Winter Light")]
    [SerializeField] private Gradient winter_gradient;

    public GameObject[] House_Light;
    void Start()
    {

        worldtimeManager = FindObjectOfType<WorldTimeManager>();

        if (worldtimeManager == null)
        {
            Debug.LogError("TimeManager not found in the scene.");
        }

        if (_light == null)
        {
            _light = GetComponent<Light2D>();
        }

        if (_light == null)
        {
            Debug.LogError("Light2D component not found. Please add a Light2D component to the GameObject.");
        }
    }

    void Update()
    {
        if (worldtimeManager != null && _light != null)
        {
            UpdateLight();
            UpdateHouseLights();
        }
    }

    void UpdateLight()
    {
        float normalizedTime = GetNormalizedTimeOfDay();

        Gradient currentGradient = worldtimeManager.Seasons == "Summer" ? summer_gradient : winter_gradient;

        _light.color = currentGradient.Evaluate(normalizedTime);
        _light.intensity = GetIntensityBasedOnTime(normalizedTime);
    }

    void UpdateHouseLights()
    {
        if ((worldtimeManager.hours > 20) ||
            (worldtimeManager.hours == 20 && worldtimeManager.minutes >= 30) ||
            (worldtimeManager.hours < 10))
        {
            SetHouseLightsActive(true);
        }
        else
        {
            SetHouseLightsActive(false);
        }
    }

    void SetHouseLightsActive(bool isActive)
    {
        foreach (GameObject houseLight in House_Light)
        {
            houseLight.SetActive(isActive);
        }
    }

    float GetNormalizedTimeOfDay()
    {
        float totalMinutes = worldtimeManager.hours * 60 + worldtimeManager.minutes;
        float dayDuration = 24 * 60; 
        return (totalMinutes / dayDuration);
    }

    float GetIntensityBasedOnTime(float normalizedTime)
    {
        if (normalizedTime >= 0.25f && normalizedTime < 0.5f)
        {
            return Mathf.Lerp(0.2f, 1.0f, (normalizedTime - 0.25f) * 4f);
        }
        else if (normalizedTime >= 0.5f && normalizedTime < 0.75f)
        {
            return 1.0f;
        }
        else if (normalizedTime >= 0.75f && normalizedTime < 1.0f)
        {
            return Mathf.Lerp(1.0f, 0.2f, (normalizedTime - 0.75f) * 4f);
        }
        else
        {
            return 0.2f; 
        }
    }
}
