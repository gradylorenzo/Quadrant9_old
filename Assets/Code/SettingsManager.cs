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

    public class Volumes
    {
        public float sfx = 0.5f;
        public float music = 0.5f;
        public float UI = 0.5f;
        public float voice = 0.5f;
    }

    public class ProfileData
    {
        public string playerName;
        public int credits;

        public uniqueLocation homeStation = new uniqueLocation();
    }

    public static Volumes Volume = new Volumes();
    public static ProfileData Profile = new ProfileData();
    

    public static void LoadSettings()
    {
        bool needToReload = false;
        if (PlayerPrefs.HasKey("playerName"))
        {
            Profile.playerName = PlayerPrefs.GetString("playerName");

            if (PlayerPrefs.HasKey("credits"))
            {
                Profile.credits = PlayerPrefs.GetInt("credits");
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

    public static void LoadHomeStation()
    {
        string sys;
        Vector3 pos;
        string sce;

        bool hsys, hx, hy, hz, hsce;

        hsys = PlayerPrefs.HasKey("homeStationSystem");
        hx = PlayerPrefs.HasKey("homeStationX");
        hy = PlayerPrefs.HasKey("homeStationY");
        hz = PlayerPrefs.HasKey("homeStationZ");
        hsce = PlayerPrefs.HasKey("homeStationScene");

        if(hsys && hx && hy && hz && hsce)
        {
            sys = PlayerPrefs.GetString("homeStationSystem");
            pos = new Vector3
                (
                    PlayerPrefs.GetFloat("homeStationX"),
                    PlayerPrefs.GetFloat("homeStationY"),
                    PlayerPrefs.GetFloat("homeStationZ")
                );
            sce = PlayerPrefs.GetString("homeStationScene");

            Profile.homeStation = new uniqueLocation(sys, pos, sce);
            GameManager.homeStation = Profile.homeStation;
        }
        else
        {
            SetDefaultHomeStation();
        }
    }

    public static void SaveHomeStation(uniqueLocation loc)
    {
        PlayerPrefs.SetString("homeStationSystem", loc.systemName);
        PlayerPrefs.SetFloat("homeStationX", loc.point.x);
        PlayerPrefs.SetFloat("homeStationY", loc.point.y);
        PlayerPrefs.SetFloat("homeStationZ", loc.point.z);
        PlayerPrefs.SetString("homeStationScene", loc.sceneName);

        LoadHomeStation();
    }

    public static void SetDefaultHomeStation()
    {
        PlayerPrefs.SetString("homeStationSystem", "A1");
        PlayerPrefs.SetFloat("homeStationX", -28.4f);
        PlayerPrefs.SetFloat("homeStationY", 0.0f);
        PlayerPrefs.SetFloat("homeStationZ", -12.0f);
        PlayerPrefs.SetString("homeStationScene", "player_station_1");

        LoadHomeStation();
    }
}
