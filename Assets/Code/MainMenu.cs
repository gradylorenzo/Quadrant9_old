using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public enum menus
    {
        none,
        settings,
        about,
        quitconfirm
    }
    public enum menuStates
    {
        loadingSettings,
        playerNameEntry,
        mainMenu
    }

    public menuStates State;
    public menus currentMenu;

    public string playerName;

    public float logoScale;
    public Texture2D logo;
    public GUISkin gs;
    public Texture2D background;
    public Vector2 pos;
    public Vector2 settingsPos;
    public Vector2 aboutPos;

    private Settings.Volumes tempVol = new Settings.Volumes();

    private void Awake()
    {
        InitializeSettings();        
    }

    void InitializeSettings()
    {
        State = menuStates.loadingSettings;
    }

    private void OnGUI()
    {
        if (gs)
        {
            GUI.skin = gs;
        }

        //Loading settings
        if(State == menuStates.loadingSettings)
        {
            GUILayout.BeginArea(new Rect(5, 5, Screen.width / 4, 500));
            GUILayout.Label("Loading player settings..");
            GUILayout.EndArea();

            Settings.LoadSettings();
            if (Settings.playerName == "")
            {
                State = menuStates.playerNameEntry;
            }
            else
            {
                playerName = Settings.playerName;
                State = menuStates.mainMenu;
            }
        }

        //Player Name entry
        else if(State == menuStates.playerNameEntry)
        {
            GUILayout.BeginArea(new Rect(5, 5, Screen.width / 4, 500));
                GUILayout.BeginVertical();
                    GUILayout.Label("Enter Player Name..");
                    playerName = GUILayout.TextField(playerName);
                    if (GUILayout.Button("Start"))
                    {
                        if(playerName != "")
                        {
                    Settings.SaveSettings(playerName, Settings.Volume.sfx, Settings.Volume.music, Settings.Volume.UI, Settings.Volume.voice, Settings.credits);
                            InitializeSettings();
                        }
                        else
                        {
                            GUILayout.Label("Player Name cannot be empty");
                        }
                    }
                GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        #region Main Menu
        else if (State == menuStates.mainMenu)
        {
            if (background)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, 60), background);
                GUILayout.BeginArea(new Rect(12, 6, 500, 50));
                GUILayout.BeginVertical();
                GUILayout.Label("Welcome, " + playerName);
                GUILayout.Label("Credits: " + Settings.credits);
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
            
            GUILayout.BeginArea(new Rect(4, Screen.height - 64, (Screen.width/2) - (logo.width / 2), 60));
                GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Start", GUILayout.Height(60), GUILayout.Width((Screen.width / 4) - logo.width / 4)))
                    {
                        SceneManager.LoadSceneAsync("A1");
                    }
                    GUILayout.Space(4);
                    if (GUILayout.Button("Settings", GUILayout.Height(60), GUILayout.Width((Screen.width / 4) - logo.width / 4)))
                    {
                        currentMenu = menus.settings;
                        tempVol = Settings.Volume;
                    }
                GUILayout.EndHorizontal();
            GUILayout.EndArea();

            GUI.DrawTexture(new Rect((Screen.width / 2) - (logo.width/2), Screen.height - logo.height, logo.width, logo.height), logo);

            GUILayout.BeginArea(new Rect((Screen.width / 2) + (logo.width / 2) - 4, Screen.height - 64, (Screen.width / 2) - (logo.width / 2), 60));
                GUILayout.BeginHorizontal();
                    if (GUILayout.Button("About", GUILayout.Height(60), GUILayout.Width((Screen.width / 4) - logo.width / 4)))
                    {
                        currentMenu = menus.about;
                    }
                    GUILayout.Space(4);
                    if (GUILayout.Button("Quit", GUILayout.Height(60), GUILayout.Width((Screen.width / 4) - logo.width / 4)))
                    {
                        currentMenu = menus.quitconfirm;
                    }
                GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        #endregion

        #region Window calls
        if(currentMenu == menus.settings)
        {
            GUI.Window(0, new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), doSettingsWindow, "Settings");
        }
        else if (currentMenu == menus.about)
        {
            GUI.Window(0, new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400), doAboutWindow, "About");
        }
        else if (currentMenu == menus.quitconfirm)
        {
            GUI.Window(0, new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 104), doQuitWindow, "Confirm");
        }
#endregion
    }


    #region Window methods
    public void doSettingsWindow(int id)
    {
        GUILayout.BeginArea(new Rect(4, 20, 396, 400));
        GUILayout.BeginVertical();

        GUILayout.Label("Volume Settings");
        GUILayout.BeginHorizontal();
        GUILayout.Label("SFX", GUILayout.Width(150));
        tempVol.sfx = GUILayout.HorizontalSlider(tempVol.sfx, 0, 1, GUILayout.Width(200));
        GUILayout.Label(tempVol.sfx.ToString("0.#"), GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Music", GUILayout.Width(150));
        tempVol.music = GUILayout.HorizontalSlider(tempVol.music, 0, 1, GUILayout.Width(200));
        GUILayout.Label(tempVol.music.ToString("0.#"), GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("UI", GUILayout.Width(150));
        tempVol.UI = GUILayout.HorizontalSlider(tempVol.UI, 0, 1, GUILayout.Width(200));
        GUILayout.Label(tempVol.UI.ToString("0.#"), GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Voice", GUILayout.Width(150));
        tempVol.voice = GUILayout.HorizontalSlider(tempVol.voice, 0, 1, GUILayout.Width(200));
        GUILayout.Label(tempVol.voice.ToString("0.#"), GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.Space(30);

        if (GUILayout.Button("Save", GUILayout.Height(60)))
        {
            Settings.SaveSettings(Settings.playerName, tempVol.sfx, tempVol.music, tempVol.UI, tempVol.voice, Settings.credits);
            currentMenu = menus.none;
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();

        if (GUI.Button(new Rect(382, 4, 10, 10), "X"))
        {
            currentMenu = menus.none;
        }
    }

    public void doAboutWindow(int id)
    {
        string text = "";
        text = "Quadrant9 \nCopyright 2017 Grady Lorenzo, All Rights Reserved. \n\nSpecial thanks to Josh Hollandsworth, the most badass lore god of all time. \n\nGrover Baxley, great help in testing. \n\nSky Bettridge, my loving and supporting girlfriend < 3 \n\nYou can follow this project by going to \ngithub.com/gradyloreno/Quadrant9 \nThis project in its entirety, including the \nsource found on GitHub, is under an Apache 2.0 \nlicense.You can find that in the \naforementioned source on GitHub, or you \ncan Google it.I tried to copypaste it \ninto this about.txt file, but the formatting \nwas all wrong, and I'm not about to go through \nall of that.";
        GUILayout.BeginArea(new Rect(4, 20, 396, 400));
        aboutPos = GUILayout.BeginScrollView(aboutPos);
        GUILayout.BeginVertical();
        GUILayout.Label(text);
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
        if(GUI.Button(new Rect(382, 4, 10, 10), "X"))
        {
            currentMenu = menus.none;
        }
    }

    public void doQuitWindow(int id)
    {
        GUILayout.BeginArea(new Rect(4, 20, 396, 200));
        GUILayout.BeginVertical();
        GUILayout.Label("Are you sure?", gs.customStyles[0]);
        GUILayout.Space(4);
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("No", GUILayout.Width(196), GUILayout.Height(60)))
        {
            currentMenu = menus.none;
        }
        GUILayout.Space(4);
        if(GUILayout.Button("Yes", GUILayout.Width(196), GUILayout.Height(60)))
        {
            Application.Quit();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
#endregion
}
