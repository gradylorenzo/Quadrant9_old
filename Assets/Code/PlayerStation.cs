using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStation : MonoBehaviour {

    public string stationName = "";
    public bool showOptions = false;
    public Vector2 mouseClickPoint;

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
                GameManager.setHomeStation(GameManager.currentSystem, GameManager.CelestialCam.transform.position, GameManager.currentSystem);
                GameManager.addCredits(-100);
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
