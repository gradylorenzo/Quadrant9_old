using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBubble : MonoBehaviour {

    public Vector2 speed;

    public void Update()
    {
        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", speed * Time.timeSinceLevelLoad);

        if(GameManager.shipTravelState == GameManager.TravelStates.Warping)
        {
            this.GetComponent<Renderer>().enabled = true;
            this.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            this.GetComponent<Renderer>().enabled = false;
            this.GetComponent<AudioSource>().enabled = false;
        }
    }
}
