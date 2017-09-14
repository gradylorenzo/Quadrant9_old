using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notification : MonoBehaviour {

    [System.Serializable]
    public enum NotificationType
    {
        standard,
        damageDone,
        damageTaken,
        repaired,
        warning,
        error
    }

    public NotificationType Type = NotificationType.standard;
    public string text;

    public float timestamp;
    public float lifespan;
    public float xPos;
    public float wantedXPos;
    public float damp;
    public float fade;
    public float fadeSpeed;
    public GUISkin gs;

    private void Start()
    {
        xPos = 0;
        wantedXPos = 0;
        fade = 1;
        timestamp = Time.time;
        foreach(GameObject go in GameManager.Notifications)
        {
            go.GetComponent<notification>().wantedXPos += 15;
        }
        GameManager.addNotification(gameObject);
    }

    private void FixedUpdate()
    {
        if(timestamp + lifespan < Time.time || xPos >= Screen.height / 3)
        {
            GameManager.removeNotification(gameObject);
            DestroyObject(gameObject);
        }
        fade -= fadeSpeed;
        xPos = Mathf.MoveTowards(xPos, wantedXPos, damp);
    }

    private void OnGUI()
    {
        Color gcol = new Color(1, 1, 1, fade);

        GUI.skin = gs;
        GUI.color = gcol;


        GUILayout.BeginArea(new Rect(Screen.width / 2 - 400, Screen.height / 3 - xPos, 800, 20));

        GUILayout.BeginHorizontal();

        GUILayout.Label(text);

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}
