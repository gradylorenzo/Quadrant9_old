using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{

    public class UIWindows
    {
        GameObject playerShip;

        public Dictionary<string, Rect> WindowRects = new Dictionary<string, Rect>()
        {
            {"quit", new Rect(200, 200, 500, 500) },
            {"loadout", new Rect(200, 200, 500, 500) },
            {"profile", new Rect(200, 200, 500, 500) },
        };

        public Dictionary<string, bool> WindowBools = new Dictionary<string, bool>()
        {
            {"quit", false },
            {"loadout", false },
            {"profile", false },
        };
    }

    public GameObject playerShip;
    public AudioSource[] oneShotSources;
    public AudioClip[] oneShotSFX;
    public Texture2D frame;
    public Texture2D bar;
    public Texture2D barBG;
    public Texture2D barGUIL;
    public GUISkin gs;
    public Texture2D[] icons;
    public Texture2D[] sidebarIcons;
    public UIWindows Windows = new UIWindows();

    public float offset;

    public float hbposX;
    public float hbposY;

    public float monitorHScroll;
    public Vector2 monitorScrollPosition;

    float mShield, mArmor, mStruct = 0f;
    float cShield, cArmor, cStruct = 0f;

    private ShipWeapons weapons;
    private Camera cam;

    private float wantedsbPos = -68;
    private int i = 0;


    private bool confirmQuit = false;
    private void Start()
    {
        playerShip = GameManager.PlayerShip;
        cam = this.GetComponent<Camera>();
        print(GameManager.playerName);
    }

    public void OnGUI()
    {
        if (gs)
        {
            GUI.skin = gs;
        }

        if (playerShip)
        {

            weapons = playerShip.GetComponent<ShipWeapons>();

            #region warpcharge_display
            //Warp Charge Display
            float wc = playerShip.GetComponent<ShipController>().warpCharge;

            if (wc == 10)
            {
                if (playerShip.GetComponent<ShipController>().countdown)
                {
                    GUI.Label(new Rect(Screen.width / 2 - 252, Screen.height - 220, 504, 20), "ENGAGING IN " + playerShip.GetComponent<ShipController>().timeRemaining.ToString("0.0"), gs.customStyles[10]);
                }
                else
                {
                    GUI.Label(new Rect(Screen.width / 2 - 252, Screen.height - 220, 504, 20), "READY TO ENGAGE", gs.customStyles[10]);
                    if (GUI.Button(new Rect(Screen.width / 2 - 252, Screen.height - 200, 504, 14), "ENGAGE"))
                    {
                        playerShip.GetComponent<ShipController>().doCountdown();
                    }
                }
            }
            else if (wc > 0 && wc < 10)
            {
                GUI.Label(new Rect(Screen.width / 2 - 252, Screen.height - 220, 504, 20), "WARP DRIVE CHARGING...", gs.customStyles[10]);
                GUI.DrawTexture(new Rect(Screen.width / 2 - 252, Screen.height - 200, 504, 14), frame);
                GUI.DrawTexture(new Rect(Screen.width / 2 - 250, Screen.height - 198, 50 * wc, 10), bar);
            }
            #endregion

            #region monitorpanel
            //Monitor Panel
            GUILayout.BeginArea(new Rect(Screen.width - 304, 5, 300, Screen.height - 10), gs.box);
            GUILayout.BeginVertical();
            GUILayout.Label("Monitor");
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", GUILayout.Width(200));
            GUILayout.Space(5);
            GUILayout.Label("Distance", GUILayout.Width(100));
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            monitorScrollPosition = GUILayout.BeginScrollView(monitorScrollPosition);

            GUILayout.BeginVertical();
            GUILayout.Label("Celestial Points");
            foreach (GameObject go in GameManager.poiCoords)
            {

                float dis = Vector3.Distance(GameManager.CelestialCam.transform.position, go.transform.position);
                if (dis > .05f)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(go.GetComponent<PointOfInterest>().poiName, gs.customStyles[3], GUILayout.Width(200)))
                    {
                        if (GameManager.shipTravelState != GameManager.TravelStates.Warping)
                        {
                            GameManager.enterAlign(go, go.GetComponent<PointOfInterest>().sceneToLoad);
                        }
                    }

                    //GUILayout.Space(10);
                    float f_dist = Vector3.Distance(GameManager.CelestialCam.transform.position, go.transform.position);
                    int i_dist = Convert.ToInt32(f_dist / 10);
                    float f_i_dist = i_dist / 10;
                    GUILayout.Label(f_i_dist.ToString() + " AU");
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5);
                }
            }


            GUILayout.Label("Targets");
            foreach (GameObject go in weapons.AvailableTargets)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(go.GetComponent<Targetable>().targetName, gs.customStyles[3], GUILayout.Width(200)))
                {
                    if (!go.GetComponent<Targetable>().Defense.isImmune)
                    {
                        weapons.AddLockedTarget(go);
                    }
                }
                GUILayout.Space(10);
                float f_dist = Vector3.Distance(GameManager.PlayerShip.transform.position, go.transform.position);
                int i_dist = Convert.ToInt32(f_dist / 10);
                GUILayout.Label(i_dist.ToString() + " KM");
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            //scrollbar
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndArea();
            #endregion

            #region lockedtargets_display
            //Locked Targets display
            if (GameManager.shipTravelState != GameManager.TravelStates.Warping)
            {
                GUILayout.BeginArea(new Rect(Screen.width - 508, 5, 200, Screen.height - 5));
                GUILayout.BeginVertical();
                GUILayout.Label("LOCKED TARGETS");

                if (weapons.LockedTargets.Count > 0)
                {
                    foreach (GameObject go in weapons.LockedTargets)
                    {
                        //Red frames for selected target to distinguish from other locked targets
                        //Selected target
                        if (go != weapons.SelectedTarget)
                        {
                            GUILayout.BeginVertical(gs.customStyles[0]);
                            GUILayout.Label(go.GetComponent<Targetable>().targetName);
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button(go.GetComponent<Targetable>().image, gs.customStyles[1]))
                            {
                                weapons.SelectedTarget = go;
                            }
                            GUILayout.Space(10);
                            GUILayout.BeginVertical();
                            GUILayout.Label("Shield:" + Convert.ToInt32(go.GetComponent<Targetable>().Defense.curShield));
                            GUILayout.Label("Armor:" + Convert.ToInt32(go.GetComponent<Targetable>().Defense.curArmor)); ;
                            GUILayout.Label("Struct:" + Convert.ToInt32(go.GetComponent<Targetable>().Defense.curStruct));
                            GUILayout.EndVertical();
                            GUILayout.EndHorizontal();
                            GUILayout.Space(5);
                            GUILayout.EndVertical();
                            GUILayout.Space(10);
                        }
                        //Unselected targets
                        else
                        {
                            GUILayout.BeginVertical(gs.customStyles[2]);
                            GUILayout.Label(go.GetComponent<Targetable>().targetName);
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button(go.GetComponent<Targetable>().image, gs.customStyles[1]))
                            {
                                weapons.SelectedTarget = go;
                            }
                            GUILayout.Space(10);
                            GUILayout.BeginVertical();
                            GUILayout.Label("Shield:" + Convert.ToInt32(go.GetComponent<Targetable>().Defense.curShield));
                            GUILayout.Label("Armor:" + Convert.ToInt32(go.GetComponent<Targetable>().Defense.curArmor)); ;
                            GUILayout.Label("Struct:" + Convert.ToInt32(go.GetComponent<Targetable>().Defense.curStruct));
                            GUILayout.EndVertical();
                            GUILayout.EndHorizontal();
                            GUILayout.Space(5);
                            GUILayout.EndVertical();
                            GUILayout.Space(10);
                        }
                    }
                }
                //clear targets
                GUILayout.Space(20);
                if (playerShip.GetComponent<ShipWeapons>().LockedTargets.Count > 0)
                {
                    if (GUILayout.Button("CLEAR TARGETS"))
                    {
                        weapons.ClearLockedTargets();
                        weapons.setSelectedTarget(null);
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
            #endregion

            #region in-space_icons
            //Ship icons in-space
            if (weapons.AvailableTargets.Count > 0)
            {
                foreach (GameObject go in weapons.AvailableTargets)
                {
                    Vector3 relativePos = go.transform.position - GameManager.CelestialCam.transform.position;
                    Quaternion lookRot = Quaternion.LookRotation(relativePos);
                    float ang = Quaternion.Angle(lookRot, GameManager.CelestialCam.transform.rotation);

                    if (go.GetComponent<Targetable>().Type == Targetable.targetTypes.hostile && go.transform.Find("graphic").GetComponent<Renderer>().isVisible)
                    {
                        Vector3 pos = cam.WorldToScreenPoint(go.transform.position);
                        GUI.DrawTexture(new Rect(pos.x - (icons[1].width / 2), Screen.height - (pos.y + (icons[1].height / 2)), icons[1].width, icons[1].height), icons[1]);
                    }
                }
            }

            //Selected target icon
            if (weapons.SelectedTarget != null)
            {
                if (weapons.SelectedTarget.transform.Find("graphic").GetComponent<Renderer>().isVisible)
                {
                    Vector3 pos = cam.WorldToScreenPoint(weapons.SelectedTarget.transform.position);
                    GUI.DrawTexture(new Rect(pos.x - (icons[0].width / 2), Screen.height - (pos.y + (icons[0].height / 2)), icons[0].width, icons[0].height), icons[0]);
                }
            }

            //POI Icons
            if (GameManager.poiCoords.Count > 0)
            {
                foreach (GameObject go in GameManager.poiCoords)
                {
                    Vector3 relativePos = go.transform.position - GameManager.CelestialCam.transform.position;
                    Quaternion lookRot = Quaternion.LookRotation(relativePos);
                    float ang = Quaternion.Angle(lookRot, GameManager.CelestialCam.transform.rotation);
                    float dis = Vector3.Distance(GameManager.CelestialCam.transform.position, go.transform.position);
                    if (ang < GameManager.CelestialCam.GetComponent<Camera>().fieldOfView + 4 && dis > .1f)
                    {
                        Vector3 pos = GameManager.CelestialCam.GetComponent<Camera>().WorldToScreenPoint(go.transform.position);
                        if (GUI.Button(new Rect(pos.x - (icons[2].width / 2), Screen.height - (pos.y + (icons[2].height / 2)), icons[2].width, icons[2].height), icons[2], gs.customStyles[6]))
                        {
                            GameManager.enterAlign(go, go.GetComponent<PointOfInterest>().sceneToLoad);
                        }

                        float mDis = Vector2.Distance(pos, Input.mousePosition);
                        if (mDis < 20)
                        {
                            GUI.Label(new Rect(pos.x + 13, Screen.height - (pos.y + 8), go.GetComponent<PointOfInterest>().poiName.Length * 8, 16), go.GetComponent<PointOfInterest>().poiName, gs.customStyles[10]);
                        }
                    }
                }
            }
            #endregion

            #region health_display
            GUI.BeginGroup(new Rect(320, Screen.height - 55, Screen.width - 320, 55));
            GUI.Label(new Rect(0, 0, 100, 30), "Shields: ");
            GUI.Label(new Rect(0, 15, 100, 30), "Armor  : ");
            GUI.Label(new Rect(0, 30, 100, 30), "Struct : ");

            mShield = playerShip.GetComponent<ShipSheet>().Defense.maxShield;
            mArmor = playerShip.GetComponent<ShipSheet>().Defense.maxArmor;
            mStruct = playerShip.GetComponent<ShipSheet>().Defense.maxStruct;

            cShield = Mathf.Lerp(cShield, playerShip.GetComponent<ShipSheet>().Defense.curShield, .1f);
            cArmor = Mathf.Lerp(cArmor, playerShip.GetComponent<ShipSheet>().Defense.curArmor, .1f);
            cStruct = Mathf.Lerp(cStruct, playerShip.GetComponent<ShipSheet>().Defense.curStruct, .1f);


            GUI.DrawTexture(new Rect(70, 5, 200, 10), barBG);
            GUI.DrawTexture(new Rect(70, 5, Mathf.Lerp(0, 1, (cShield / mShield)) * 200, 10), bar);

            GUI.DrawTexture(new Rect(70, 20, 200, 10), barBG);
            GUI.DrawTexture(new Rect(70, 20, Mathf.Lerp(0, 1, (cArmor / mArmor)) * 200, 10), bar);

            GUI.DrawTexture(new Rect(70, 35, 200, 10), barBG);
            GUI.DrawTexture(new Rect(70, 35, Mathf.Lerp(0, 1, (cStruct / mStruct)) * 200, 10), bar);

            GUI.Label(new Rect(280, 0, 100, 30), Convert.ToInt32(cShield) / 1000 + "%");
            GUI.Label(new Rect(280, 15, 100, 30), Convert.ToInt32(cArmor) / 1000 + "%");
            GUI.Label(new Rect(280, 30, 100, 30), Convert.ToInt32(cStruct) / 1000 + "%");
            GUI.EndGroup();
            #endregion

            #region modules_display

            GUILayout.BeginArea(new Rect(700, Screen.height - 85, playerShip.GetComponent<ShipModuleHost>().modules.Length * 69, 200));
            GUILayout.BeginHorizontal();
            foreach (ShipModule sm in playerShip.GetComponent<ShipModuleHost>().modules)
            {
                GUILayout.BeginVertical();

                if (sm != null)
                {
                    if (GUILayout.Button(sm.icon, gs.customStyles[4], GUILayout.Width(sm.icon.width), GUILayout.Height(80)))
                    {
                        sm.Activate();
                    }

                    GUILayout.Space(-20);
                    if (sm.activated)
                    {
                        if (!sm.queueDeactivate)
                        {
                            GUILayout.Label("<color=green>" + (sm.cooldown - (Time.time - sm.lastFire)).ToString("0.0") + "</color>");
                        }
                        else
                        {
                            GUILayout.Label("<color=red>" + (sm.cooldown - (Time.time - sm.lastFire)).ToString("0.0") + "</color>");
                        }
                    }
                    else
                    {
                        GUILayout.Label("inactive");
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            #endregion

            #region systeminfo

            if (GameManager.currentSystem != "" && GameManager.currentSystem != null)
            {
                GUILayout.BeginArea(new Rect(68, 0, 200, 200), gs.customStyles[11]);
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                GUILayout.BeginVertical();
                GUILayout.Label("System: " + GameManager.currentSystem, gs.customStyles[12]);
                GUILayout.Space(5);
                GUILayout.Label(GameManager.playerName);
                GUILayout.Label("CR " + GameManager.credits);
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

                GUILayout.EndArea();
            }

            #endregion

            #region sidebar

            GUILayout.BeginArea(new Rect(0, 0, 68, Screen.height), gs.customStyles[7]);
            GUILayout.BeginVertical();
            GUILayout.Label("Sidebar");
            //loadout button
            if (GUILayout.Button(sidebarIcons[0], gs.customStyles[8], GUILayout.Width(64), GUILayout.Height(64)))
            {
                Windows.WindowBools["loadout"] = !Windows.WindowBools["loadout"];
            }
            GUILayout.Space(10);
            if (GUILayout.Button(sidebarIcons[1], gs.customStyles[8], GUILayout.Width(64), GUILayout.Height(64)))
            {
                Windows.WindowBools["profile"] = !Windows.WindowBools["profile"];
            }
            if (GUILayout.Button(sidebarIcons[2], gs.customStyles[8], GUILayout.Width(64), GUILayout.Height(64)))
            {
                Windows.WindowBools["quit"] = !Windows.WindowBools["quit"];
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
            //print(Input.mousePosition.z);
            #endregion

            #region windows
            if (Windows.WindowBools["loadout"])
            {
                Windows.WindowRects["loadout"] = GUILayout.Window(0, Windows.WindowRects["loadout"], doLoadoutWindow, "Loadout");
            }
            if (Windows.WindowBools["profile"])
            {
                Windows.WindowRects["profile"] = GUILayout.Window(1, Windows.WindowRects["profile"], doProfileWindow, "Profile");
            }
            if (Windows.WindowBools["quit"])
            {
                Windows.WindowRects["quit"] = GUILayout.Window(2, Windows.WindowRects["quit"], doQuitWindow, "Quit");
            }

            #endregion

        }
        else
        {
            playerShip = GameManager.PlayerShip;
        }
    }

    //game window
    public void doQuitWindow(int windowID)
    {
        if (GUI.Button(new Rect(382, 4, 10, 10), " ", gs.customStyles[9]))
        {
            Windows.WindowBools["quit"] = false;
        }

        if (playerShip)
        {
            //GUI.DragWindow(new Rect(0, 0, 100000, 400));
            //GUILayout.BeginArea(new Rect(4, 20, 394, 200));
            GUILayout.BeginVertical();
            GUILayout.Label("Are you sure?", gs.customStyles[0]);
            GUILayout.Space(4);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("No", GUILayout.Width(196), GUILayout.Height(60)))
            {
                confirmQuit = false;
            }
            GUILayout.Space(4);
            if (GUILayout.Button("Yes", GUILayout.Width(196), GUILayout.Height(60)))
            {
                GameManager.Notifications.Clear();
                GameManager.poiCoords.Clear();
                SceneManager.LoadScene("MainMenu");
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            //GUILayout.EndArea();
        }
        else
        {
            playerShip = GameManager.PlayerShip;
        }
    }

    //loadout window
    public void doLoadoutWindow(int windowID)
    {
        if (GUI.Button(new Rect(382, 4, 10, 10), " ", gs.customStyles[9]))
        {
            Windows.WindowBools["loadout"] = false;
        }

        if (playerShip)
        {
            GUI.DragWindow(new Rect(0, 0, 100000, 400));
            //GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.Label("Loadout");
            GUILayout.BeginVertical();
            foreach (ShipModule sm in playerShip.GetComponent<ShipModuleHost>().modules)
            {
                GUILayout.BeginHorizontal(GUILayout.Width(400));
                GUILayout.Button(sm.icon, GUI.skin.customStyles[4]);

                GUILayout.BeginVertical();
                GUILayout.Label(sm.moduleName);
                GUILayout.Label(sm.description);
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.Space(10);
            }

            GUILayout.EndVertical();
            //GUILayout.EndArea();
        }
        else
        {
            playerShip = GameManager.PlayerShip;
        }
    }

    public void doProfileWindow(int windowID)
    {
        if (GUI.Button(new Rect(382, 4, 10, 10), " ", gs.customStyles[9]))
        {
            Windows.WindowBools["profile"] = false;
        }

        GUI.DragWindow(new Rect(0, 0, 100000, 400));
        //GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.Label("Profile");
        GUILayout.BeginVertical();
        GUILayout.Label("Name: " + Settings.Profile.playerName);
        GUILayout.Label("Credits: " + GameManager.credits);

        GUILayout.EndVertical();
        //GUILayout.EndArea();
    }
}
