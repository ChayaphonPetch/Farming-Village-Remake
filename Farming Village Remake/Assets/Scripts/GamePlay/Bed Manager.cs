using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviour
{
    private WorldTimeManager _WorldTimeManager;
    private SellStorageManager _SellStorageManager;
    private TileManager _tilemanager;
    private PlayerData _playerdata;
    private UIManager _uimanager;
    private int counts;

    void Start()
    {
        _WorldTimeManager = FindObjectOfType<WorldTimeManager>();
        _SellStorageManager = FindObjectOfType<SellStorageManager>();
        _tilemanager = FindObjectOfType<TileManager>();
        _playerdata = FindObjectOfType<PlayerData>();
        _uimanager = FindObjectOfType<UIManager>();

    }

    public void Sleep()
    {
        int limitNightMinutes = 24 * 60 + 59;
        int limitMorningMinutes = 6 * 60 + 29;

        int currentTotalMinutes = _WorldTimeManager.hours * 60 + _WorldTimeManager.minutes;

        if (currentTotalMinutes <= limitNightMinutes && currentTotalMinutes >= limitMorningMinutes)
        {
            _WorldTimeManager.day++;

            if (_WorldTimeManager.day > 31)
            {
                _WorldTimeManager.day = 1;
                _WorldTimeManager.ChangeSeason();
            }

            PlantGrowingManager[] allPlantManagers = FindObjectsOfType<PlantGrowingManager>();
            foreach (var plantManager in allPlantManagers)
            {
                plantManager.OnDayPassed();
            }

            _SellStorageManager.SellItems();
            _WorldTimeManager.ChangeWeather();
            _tilemanager.ConvertAllWetSoiltoSoil();
            _playerdata.current_stamina = (int)_uimanager.StaminaSlider.maxValue;
        }

        _WorldTimeManager.hours = 6;
        _WorldTimeManager.minutes = 30;
    }

}
