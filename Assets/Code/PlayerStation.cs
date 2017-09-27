using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStation : MonoBehaviour {

    public string stationName = "";
    public bool showOptions = false;
    public Vector2 mouseClickPoint;
    public int homeStationPrice;

    private void OnMouseUpAsButton()
    {
        showOptions = true;
        mouseClickPoint = Input.mousePosition;
    }

    private void OnGUI()
    {
        if (showOptions)
        {
            GUILayout.BeginArea(new Rect(mouseClickPoint.x, Screen.height - mouseClickPoint.y, 300, 200));
            GUILayout.BeginVertical();
            GUILayout.Label(stationName);
            if (GUILayout.Button("Set Home Station"))
            {
                Settings.WriteHomeStation(GameManager.currentSystem, GameManager.CelestialCam.transform.position, GameManager.currentScene);
                GameManager.addCredits(-homeStationPrice);
                showOptions = false;
            }
            if (GUILayout.Button("Close"))
            {
                showOptions = false;
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
