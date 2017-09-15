using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static int defaultCredits = 10000;
    public enum volumeTypes
    {
        sfx,
        music,
        UI,
        voice,
    }

    public static bool settingsOK = false;

    public static string playerName;
    public static int credits;

    public class Volumes
    {
        public float sfx = 0.5f;
        public float music = 0.5f;
        public float UI = 0.5f;
        public float voice = 0.5f;
    }

    public static Volumes Volume = new Volumes();

    public static void LoadSettings()
    {
        bool needToReload = false;
        if (PlayerPrefs.HasKey("playerName"))
        {
            playerName = PlayerPrefs.GetString("playerName");

            if (PlayerPrefs.HasKey("credits"))
            {
                credits = PlayerPrefs.GetInt("credits");
            }
            else
            {
                PlayerPrefs.SetInt("credits", defaultCredits);
                needToReload = true;
            }

            if (PlayerPrefs.HasKey("sfxVolume"))
            {
                Volume.sfx = PlayerPrefs.GetFloat("sfxVolume");
            }
            else
            {
                PlayerPrefs.SetFloat("sfxVolume", 0.5f);
                needToReload = true;
            }

            if (PlayerPrefs.HasKey("musicVolume"))
            {
                Volume.music = PlayerPrefs.GetFloat("musicVolume");
            }
            else
            {
                PlayerPrefs.SetFloat("musicVolume", 0.5f);
                needToReload = true;
            }

            if (PlayerPrefs.HasKey("UIVolume"))
            {
                Volume.UI = PlayerPrefs.GetFloat("UIVolume");
            }
            else
            {
                PlayerPrefs.SetFloat("UIVolume", 0.5f);
                needToReload = true;
            }

            if (PlayerPrefs.HasKey("voiceVolume"))
            {
                Volume.voice = PlayerPrefs.GetFloat("voiceVolume");
            }
            else
            {
                PlayerPrefs.SetFloat("voiceVolume", 0.5f);
            }

            if (needToReload)
            {
                LoadSettings();
            }
        }
        else
        {
            SaveDefaultSettings();
        }
    }
	
    public static void SaveSettings(string pName, float sfxVol, float musicVol, float UIVol, float voiceVol, int cred)
    {
        PlayerPrefs.SetString("playerName", pName);
        PlayerPrefs.SetFloat("sfxVolume", sfxVol);
        PlayerPrefs.SetFloat("musicVolume", musicVol);
        PlayerPrefs.SetFloat("UIVolume", UIVol);
        PlayerPrefs.SetFloat("voiceVolume", voiceVol);
        PlayerPrefs.SetInt("credits", cred);
        LoadSettings();
    }

    public static void SaveDefaultSettings()
    {
        SaveSettings("", 0.5f, 0.5f, 0.5f, 0.5f, defaultCredits);
    }
}
