using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {

    public class UIWindows
    {
        GameObject playerShip;

        public Dictionary<string, Rect> WindowRects = new Dictionary<string, Rect>()
    {
        {"loadout", new Rect(200, 200, 500, 500) },
    };

        public Dictionary<string, bool> WindowBools = new Dictionary<string, bool>()
    {
        {"loadout", false },
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


    public float hbposX;
    public float hbposY;

    public float monitorHScroll;
    public Vector2 monitorScrollPosition;

    float mShield, mArmor, mStruct = 0f;
    float cShield, cArmor, cStruct = 0f;

    private ShipWeapons weapons;
    private Camera cam;

    private float sbwidth = 68;
    private float sbPos = -68;
    private float wantedsbPos = -68;

    private void Start()
    {
        playerShip = GameManager.PlayerShip;
        cam = this.GetComponent<Camera>();
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
                GUI.Label(new Rect(Screen.width / 2 - 252, Screen.height - 220, 504, 20), "READY TO ENGAGE");
                if(GUI.Button(new Rect(Screen.width / 2 - 252, Screen.height - 200, 504, 14), "ENGAGE"))
                {
                    GameManager.enterWarp("station_1");
                    //oneShotSources[0].PlayOneShot(oneShotSFX[0]);
                }
            }
            else if (wc > 0 && wc < 10)
            {
                GUI.Label(new Rect(Screen.width / 2 - 252, Screen.height - 220, 504, 20), "WARP DRIVE CHARGING...");
                GUI.DrawTexture(new Rect(Screen.width / 2 - 252, Screen.height - 200, 504, 14), frame);
                GUI.DrawTexture(new Rect(Screen.width / 2 - 250, Screen.height - 198, 50 * wc, 10), bar);
            }
            #endregion

            #region monitorpanel
            //Monitor Panel
            GUILayout.BeginArea(new Rect(5, 5, 300, Screen.height - 10), gs.box);
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
            foreach(GameObject go in GameManager.poiCoords)
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
                GUILayout.BeginArea(new Rect(Screen.width - 205, 5, 200, Screen.height - 5));
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
                if (GUILayout.Button("CLEAR TARGETS"))
                {
                    weapons.ClearLockedTargets();
                    weapons.setSelectedTarget(null);
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
                        if (go.GetComponent<Targetable>().Type == Targetable.targetTypes.hostile && go.transform.FindChild("graphic").GetComponent<Renderer>().isVisible)
                        {
                            Vector3 pos = cam.WorldToScreenPoint(go.transform.position);
                            GUI.DrawTexture(new Rect(pos.x - 8, Screen.height - (pos.y + 8), 16, 16), icons[1]);
                        }
                    }
                }

                //Selected target icon
                if (weapons.SelectedTarget != null)
                {
                    if (weapons.SelectedTarget.transform.FindChild("graphic").GetComponent<Renderer>().isVisible)
                    {
                        Vector3 pos = cam.WorldToScreenPoint(weapons.SelectedTarget.transform.position);
                        GUI.DrawTexture(new Rect(pos.x - 8, Screen.height - (pos.y + 8), 16, 16), icons[0]);
                    }
                }

                //POI Icons
                if(GameManager.poiCoords.Count > 0)
                {
                    foreach(GameObject go in GameManager.poiCoords)
                    {
                        Vector3 relativePos = go.transform.position - GameManager.CelestialCam.transform.position;
                        Quaternion lookRot = Quaternion.LookRotation(relativePos);
                        float ang = Quaternion.Angle(lookRot, GameManager.CelestialCam.transform.rotation);
                        float dis = Vector3.Distance(GameManager.CelestialCam.transform.position, go.transform.position);
                        //print(ang);
                        if (ang < GameManager.CelestialCam.GetComponent<Camera>().fieldOfView + 4  && dis > .1f)
                        {
                            Vector3 pos = GameManager.CelestialCam.GetComponent<Camera>().WorldToScreenPoint(go.transform.position);
                            if(GUI.Button(new Rect(pos.x - 8, Screen.height - (pos.y + 8), 16, 16), icons[2], gs.customStyles[6]))
                            {
                                GameManager.enterAlign(go, go.GetComponent<PointOfInterest>().sceneToLoad);
                            }

                            float mDis = Vector2.Distance(pos, Input.mousePosition);
                            if(mDis < 20)
                            {
                                GUI.Label(new Rect(pos.x + 13, Screen.height - (pos.y + 8), 200, 16), go.GetComponent<PointOfInterest>().poiName);
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

            GUILayout.BeginArea(new Rect(700, Screen.height - 64, playerShip.GetComponent<ShipModuleHost>().modules.Length * 69, 200));
            GUILayout.BeginHorizontal();
            foreach (ShipModule sm in playerShip.GetComponent<ShipModuleHost>().modules)
            {
                GUILayout.BeginVertical();

                if (sm != null)
                {
                    if (GUILayout.Button(sm.icon, gs.customStyles[4], GUILayout.Width(sm.icon.width)))
                    {
                        sm.Activate();
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            #endregion

            #region sidebar


            sbPos = Mathf.Lerp(sbPos, wantedsbPos, .1f);
            

            if(Input.mousePosition.x < sbwidth)
            {
                wantedsbPos = 0;
            }

            else
            {
                wantedsbPos = -68;
            }

            GUILayout.BeginArea(new Rect(sbPos, 0, sbwidth, Screen.height), gs.customStyles[7]);
            GUILayout.BeginVertical();
            GUILayout.Label("Sidebar");
            //loadout button
            if (GUILayout.Button(sidebarIcons[0], gs.customStyles[8]))
            {
                Windows.WindowBools["loadout"] = !Windows.WindowBools["loadout"];
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


            #endregion

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
}
