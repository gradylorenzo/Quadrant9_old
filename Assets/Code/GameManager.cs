using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

static public class GameManager {

#region Variables

    [System.Serializable]
    public enum GameStates
    {
        Playing,
        Paused
    }

    [System.Serializable]
    public enum TravelStates
    {
        Aligned,
        Aligning,
        Warping,
        Stopped
    }

    static public GameStates State = GameStates.Playing;
    static public TravelStates shipTravelState = TravelStates.Stopped;

    static public uniqueLocation homeStation = new uniqueLocation();

    static public string ship = "Charybdis";
    static public int credits = 0;
    static public string playerName;

    static public string currentSystem;
    static public string currentScene;
    static public string nextScene;
    public static GameObject SystemRoot;
    static public GameObject CelestialCam;
    static public GameObject WarpTarget;
    static public GameObject PlayerShip;

    static public List<GameObject> poiCoords = new List<GameObject>();

    static public List<GameObject> Notifications = new List<GameObject>();

    static public float warpCharge;
    static public float warpChargeSpeed;

    static public void setCelestialCamera(Camera cam)
    {
        CelestialCam = cam.gameObject;
    }

    static public void respawn()
    {
        CelestialCam.transform.position = homeStation.point;
        SMUnload(currentScene);
        currentScene = homeStation.sceneName;
        exitWarp();
        GameObject ps = Resources.Load("Prefabs/Charybdis") as GameObject;
        SystemRoot.GetComponent<StarSystem>().SpawnShip(ps);
    }

    static public void setPlayerShip(GameObject go)
    {
        PlayerShip = go;
    }

    static public void setHomeStation(string sys, Vector3 pos, string sce)
    {
        homeStation = new uniqueLocation(sys, pos, sce);
    }

    static public void AddPointOfInterest(GameObject go)
    {
        poiCoords.Add(go);
    }

#endregion

#region UnityEvents

    //Unity Events for Warping phases.
    static public UnityEvent OnAlignEnter;
    static public UnityEvent OnAlignStay;
    static public UnityEvent OnAlignExit;

    static public UnityEvent OnWarpEnter;
    static public UnityEvent OnWarpStay;
    static public UnityEvent OnWarpExit;

    #endregion

    #region StaticMethods

    static public void enterAlign(GameObject go, string s)
    {
        WarpTarget = go;
        setTravelState(TravelStates.Aligning);
        nextScene = s;
        PlayerShip.GetComponent<ShipController>().turnSpeed = 0;
    }

    static public void exitAlign()
    {
        if(shipTravelState == TravelStates.Aligning)
        {
            shipTravelState = TravelStates.Stopped;
        }
    }

    static public void enterWarp(string s)
    {
        if (shipTravelState == TravelStates.Aligned)
        {
            if (currentScene != "" && currentScene != null)
            {
                SMUnload(currentScene);
            }
            currentScene = nextScene;
            setTravelState(TravelStates.Warping);
            PlayerShip.GetComponent<ShipWeapons>().ClearAvailableTargets();
        }
        else
        {

        }
    }

    static public void exitWarp()
    {
        setTravelState(GameManager.TravelStates.Stopped);
        SceneManager.LoadScene(currentScene, LoadSceneMode.Additive);
    }

    static public void addCredits(int c)
    {
        credits += c;
    }

    static public void setTravelState(TravelStates s)
    {
        shipTravelState = s;
    }

    static public void setWarpTarget(GameObject go)
    {
        WarpTarget = go;
    }

    #endregion

    static private void SMUnload(string s)
    {
        SceneManager.UnloadSceneAsync(s);
    }

    static public void addNotification(GameObject go)
    {
        Notifications.Add(go);
    }

    static public void removeNotification(GameObject go)
    {
        if(Notifications.Contains(go))
        Notifications.Remove(go);
    }

    public class Interface : MonoBehaviour
    {
        static public void createNewNotification(string s, notification.NotificationType t)
        {
            GameObject newNotification;
            newNotification = Resources.Load("GUI/NotificationObject") as GameObject;
            newNotification.GetComponent<notification>().text = s;
            newNotification.GetComponent<notification>().Type = t;
            Instantiate(newNotification, Vector3.zero, new Quaternion(0, 0, 0, 0));
            newNotification = null;
        }
    }
}

public struct uniqueLocation
{
    public string systemName;
    public Vector3 point;
    public string sceneName;

    public uniqueLocation(string sName = "", Vector3 sPoint = new Vector3(), string scene = "")
    {
        systemName = sName;
        point = sPoint;
        sceneName = scene;
    }
}
