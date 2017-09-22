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
        eula,
        loadingSaves,
        listingSaves,
        playerNameEntry,
        mainMenu
    }

    public menuStates State;
    public menus currentMenu;

    public float logoScale;
    public Texture2D logo;
    public GUISkin gs;
    public Texture2D background;
    public Vector2 pos;
    public Vector2 settingsPos;
    public Vector2 aboutPos;
    public Vector2 savesPos;
    public Vector2 eulaPos;
    public fadingbg fadeBG;

    private Settings.Volumes tempVol = new Settings.Volumes();

    private bool allowLoad = true;

    private string tempname = "";
    private bool firstRun = false;


    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        if (Settings.activeProfileXML != null && Settings.activeProfileXML != "")
        {
            State = menuStates.mainMenu;
        }
        else
        {
            if (PlayerPrefs.HasKey("eulaagreed"))
            {
                if (PlayerPrefs.GetInt("eulaagreed") == 1)
                {
                    firstRun = false;
                }
                else
                {
                    firstRun = true;
                }
            }
            else
            {
                PlayerPrefs.SetInt("eulaagreed", 0);
                firstRun = true;
            }

            if (firstRun)
            {
                State = menuStates.eula;
            }
            else
            {
                State = menuStates.loadingSaves;
            }
        }
    }

    private void Start()
    {
        fadeBG = transform.Find("fading_bg").GetComponent<fadingbg>();
    }

    private void OnGUI()
    {
        if (gs)
        {
            GUI.skin = gs;
        }

        //EULA Message
        if(State == menuStates.eula)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, 100, 400, Screen.height - 200), "Welcome to Quadrant 9", gs.window);
            GUILayout.BeginVertical();
            eulaPos = GUILayout.BeginScrollView(eulaPos);
            GUILayout.Space(10);
            GUILayout.Label("Thank you for playing Quadrant 9\n\nIf you are playing this, it's likely you obtained a copy directly from me, as the game is not yet in a proper release state. If you did not obtain this game from me, you are more than welcome to continue playing, but please understand the game is not completed, and the bugs are many.\n\nTo report bugs (I'd greatly appreciate it), you may contact me via Discord @Nyxton#6759. Also understand that, no matter how you obtained this game, by clicking 'Continue', you are agreeing to an Apache 2.0 license, which you can read by clicking 'License' below.\n\nThank you, and enjoy Quadrant 9 :)\n\n-Grady Lorenzo");
            GUILayout.Space(10);
            GUILayout.EndScrollView();
            if (GUILayout.Button("Continue", GUILayout.Height(60)))
            {
                PlayerPrefs.SetInt("eulaagreed", 1);
                Initialize();
            }
            GUILayout.Space(10);
            if (GUILayout.Button("License", GUILayout.Height(60)))
            {
                Application.OpenURL("https://github.com/gradylorenzo/Quadrant9/blob/master/LICENSE");
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        //Loading settings
        if(State == menuStates.loadingSaves)
        {
            Settings.LoadSaveList();
            if(Settings.saveList.Count > 0)
            {
                State = menuStates.listingSaves;
            }
            else
            {
                State = menuStates.playerNameEntry;
            }
        }

        //Save list
        else if(State == menuStates.listingSaves)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, 100, 400, Screen.height - 200), "Select Profile", gs.window);
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            savesPos = GUILayout.BeginScrollView(savesPos);
            foreach (KeyValuePair<string, string> v in Settings.saveList)
            {
                if(GUILayout.Button(v.Value, GUILayout.Height(60)))
                {
                    Settings.ReadSave(v.Key);
                    State = menuStates.mainMenu;
                }
                GUILayout.Space(10);
            }
            GUILayout.EndScrollView();
            GUILayout.Space(10);
            if(GUILayout.Button("New Game", GUILayout.Height(60)))
            {
                State = menuStates.playerNameEntry;
            }
            
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        //Player Name entry
        else if(State == menuStates.playerNameEntry)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, 200, 400, Screen.height - 400), "New Game");
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.Label("Enter Player Name", gs.customStyles[0]);
            GUILayout.Space(10);
            tempname = GUILayout.TextField(tempname);
            GUILayout.Space(10);
            if (tempname == "")
            {
                GUILayout.Label("<color=red>Name cannot be left blank</color>", gs.customStyles[0]);
                GUILayout.Space(10);
            }
            else
            {
                if (GUILayout.Button("Start", GUILayout.Height(60)))
                {
                    Settings.WriteNewProfile(tempname);
                    State = menuStates.loadingSaves;
                    tempname = "";
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
                GUILayout.Label("Welcome, " + Settings.Profile.playerName);
                GUILayout.Label("Credits: " + Settings.Profile.credits);
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
            
            GUILayout.BeginArea(new Rect(4, Screen.height - 64, (Screen.width/2) - (logo.width / 2), 60));
                GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Start", GUILayout.Height(60), GUILayout.Width((Screen.width / 4) - logo.width / 4)))
                    {
                        fadeBG.fadeOut();
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

        if(fadeBG.State == fadingbg.fadeState.full && allowLoad)
        {
            SceneManager.LoadSceneAsync("A1");
            allowLoad = false;
;        }
    }


    #region Window methods
    public void doSaveListWindow (int id)
    {
        
    }

    public void doSettingsWindow(int id)
    {
        GUILayout.BeginArea(new Rect(4, 20, 394, 400));
        GUILayout.BeginVertical();
        settingsPos = GUILayout.BeginScrollView(settingsPos);
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
        tempVol.ui = GUILayout.HorizontalSlider(tempVol.ui, 0, 1, GUILayout.Width(200));
        GUILayout.Label(tempVol.ui.ToString("0.#"), GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Voice", GUILayout.Width(150));
        tempVol.voice = GUILayout.HorizontalSlider(tempVol.voice, 0, 1, GUILayout.Width(200));
        GUILayout.Label(tempVol.voice.ToString("0.#"), GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.Space(30);
        GUILayout.EndScrollView();
        if (GUILayout.Button("Save", GUILayout.Height(60)))
        {
            Settings.WriteSettings(tempVol.sfx, tempVol.music, tempVol.ui, tempVol.voice);
            currentMenu = menus.none;
        }
        if (GUILayout.Button("Reset", GUILayout.Height(60)))
        {
            tempVol = null;
            tempVol = new Settings.Volumes();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();

        if (GUI.Button(new Rect(382, 4, 10, 10), " ", gs.customStyles[1]))
        {
            currentMenu = menus.none;
        }
    }

    public void doAboutWindow(int id)
    {
        string text = "";
        text = "Quadrant9  Copyright 2017 Grady Lorenzo, All Rights Reserved.   Special thanks to Josh Hollandsworth, the most badass lore god of all time.   Grover Baxley, great help in testing.   Sky Bettridge, my loving and supporting girlfriend < 3   You can follow this project by going to  github.com/gradyloreno/Quadrant9  This project in its entirety, including the  source found on GitHub, is under an Apache 2.0  license.You can find that in the  aforementioned source on GitHub, viewable at:  http://www.apache.org/licenses/LICENSE-2.0";
        GUILayout.BeginArea(new Rect(4, 20, 394, 400));
        aboutPos = GUILayout.BeginScrollView(aboutPos);
        GUILayout.BeginVertical();
        GUILayout.Label(text);
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
        if(GUI.Button(new Rect(382, 4, 10, 10), " ", gs.customStyles[1]))
        {
            currentMenu = menus.none;
        }
    }

    public void doQuitWindow(int id)
    {
        GUILayout.BeginArea(new Rect(4, 20, 394, 200));
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
