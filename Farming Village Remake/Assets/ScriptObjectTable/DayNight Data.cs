    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.InputSystem;

    [CreateAssetMenu(menuName = "Create Data/Time")]
    public class DayNightData : ScriptableObject
    {
        [Header("Seasons/Weathers")]
        public Seasons_Cycle Seasons;
        public Weathers Weathers;

        [Header("Reset Time Data")]
        public int day;
        public int hours;
        public int minutes;
        public float timer;

       [Header("Day/Night Icon")]
        public Image Day_Icon;
        public Image Night_Icon;
    }

    public enum Seasons_Cycle
    {
        Summer, Winter
    }

    public enum Weathers
    {
        Clear, Rain, Foggy, Snow_Storm
    }


