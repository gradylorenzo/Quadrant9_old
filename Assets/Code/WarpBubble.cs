using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBubble : MonoBehaviour {

    public Vector2 speed;

    public void Update()
    {
        if (GameManager.CelestialCam)
        {
            this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", speed * Time.timeSinceLevelLoad);

            Color c = new Color(.01f, .01f, .01f, 0);
            //c = GetComponent<Renderer>().material.GetColor("_TintColor");
            c.a = (GameManager.CelestialCam.GetComponent<CelestialCamera>().WarpSpeed / 4) * 10;

            this.GetComponent<Renderer>().material.SetColor("_TintColor", c);
        }
    }
}