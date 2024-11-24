using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Localization;
//using UnityEngine.Localization.Settings;

public class SettingMenu : MonoBehaviour
{
    public TMP_Dropdown ResolutionDropdown;

    List<Resolution> uniqueResolutions = new List<Resolution>();

    private void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        HashSet<string> resolutionStrings = new HashSet<string>();

        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        // Sort resolutions by width, then height to ensure consistency
        System.Array.Sort(resolutions, (res1, res2) =>
        {
            if (res1.width == res2.width)
                return res1.height.CompareTo(res2.height);
            return res1.width.CompareTo(res2.width);
        });

        for (int i = 0; i < resolutions.Length; i++)
        {
            // Create a string for the resolution without considering refresh rates
            string resString = resolutions[i].width + " x " + resolutions[i].height;

            // Only add the resolution if it hasn't been added yet
            if (!resolutionStrings.Contains(resString))
            {
                uniqueResolutions.Add(resolutions[i]);
                resolutionStrings.Add(resString);

                // Add both windowed and fullscreen options for each unique resolution
                string windowedOption = resString + " Windowed";
                string fullscreenOption = resString + " Fullscreen";

                options.Add(windowedOption);
                options.Add(fullscreenOption);

                // Determine the current resolution index based on screen resolution and fullscreen state
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = options.Count - 2; // Windowed option index
                    if (Screen.fullScreen)
                    {
                        currentResolutionIndex = options.Count - 1; // Fullscreen option index
                    }
                }
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        // Set the initial dropdown text based on the current locale
        //UpdateDropdownLanguage();
    }

    public void SetResolution(int resolutionIndex)
    {
        bool isFullscreen = ResolutionDropdown.options[resolutionIndex].text.Contains("Fullscreen") ||
                            ResolutionDropdown.options[resolutionIndex].text.Contains("เต็มจอ");
        int actualResolutionIndex = resolutionIndex / 2;

        Resolution resolution = uniqueResolutions[actualResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen);
    }

    /*public void UpdateDropdownLanguage()
    {
        for (int i = 0; i < ResolutionDropdown.options.Count; i++)
        {
            string optionText = ResolutionDropdown.options[i].text;

            if (optionText.Contains("Fullscreen") || optionText.Contains("เต็มจอ"))
            {
                if (LocalizationSettings.SelectedLocale.Identifier.Code == "th")
                {
                    ResolutionDropdown.options[i].text = optionText.Replace("Fullscreen", "เต็มจอ");
                }
                else
                {
                    ResolutionDropdown.options[i].text = optionText.Replace("เต็มจอ", "Fullscreen");
                }
            }
        }

        ResolutionDropdown.RefreshShownValue();
    }*/



}