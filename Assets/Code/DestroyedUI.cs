using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyedUI : MonoBehaviour {

    private bool showQuit = false;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 75, 600, 300));
        GUILayout.BeginVertical();
        GUILayout.Label("Your ship was destroyed");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Respawn: CR 10,000"))
        {
            GameManager.respawn();
            GameManager.addCredits(-10000);
            Destroy(gameObject);
        }
        if (GUILayout.Button("Quit"))
        {
            showQuit = true;
        }
        GUILayout.EndHorizontal();
        if (showQuit)
        {
            GUILayout.Space(20);
            GUILayout.Label("Are you sure?");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("No"))
            {
                showQuit = false;
            }
            if (GUILayout.Button("Yes"))
            {
                SceneManager.LoadSceneAsync("MainMenu");
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
