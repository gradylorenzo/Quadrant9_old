using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadingbg : MonoBehaviour {

    public enum fadeState
    {
        none,
        full,
        transitioning
    }

    public fadeState State;
    public float fade = 0.0f;
    public float wantedFade = 0.0f;
    private Color matColor;

    private void Start()
    {
        matColor = this.GetComponent<Renderer>().materials[0].GetColor("_Color");
    }

    private void Update()
    {
        fade = Mathf.MoveTowards(fade, wantedFade, .01f);
        matColor = new Color(0, 0, 0, fade);

        this.GetComponent<Renderer>().materials[0].SetColor("_Color", matColor);

        if (fade == 1)
        {
            State = fadeState.full;
        }
        else if(fade == 0)
        {
            State = fadeState.none;
        }
        else
        {
            State = fadeState.transitioning;
        }
    }

    public void fadeIn()
    {
        wantedFade = 0;
    }

    public void fadeOut()
    {
        wantedFade = 1;
    }
}
