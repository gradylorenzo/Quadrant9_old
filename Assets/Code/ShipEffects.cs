using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEffects : MonoBehaviour {


    public GameObject[] flares;
    public GameObject[] particles;
    public GameObject[] exhausts;

    public GameObject WarpSFX;
    public GameObject WarpAmbientSFX;
    public float flareSpeed;
    public float flareint;
    public float flareBrightness;
    public float flareMultiplier;

    public AnimationCurve flareCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(.5f, 1), new Keyframe(1, .2f));

    public float particleInt;
    public float particleMultiplier = 1;
    public Color particleColor = new Color( 1, 1, 1, 1);

    public Vector3 exhaustInitScale;
    public Vector3 exhaustScale = new Vector3();
    public float exhaustScaleMultiplier = 1;

    public void doWarpExit()
    {
        if (WarpSFX)
        {
            WarpSFX.GetComponent<AudioSource>().Play();
        }
        Camera.main.GetComponent<MouseOrbit>().doShake();
        GetComponent<ShipController>().wantedFlightSpeed = 0;
        WarpAmbientSFX.GetComponent<AudioSource>().enabled = false;
    }

    public void doWarpEnter()
    {
        if (WarpSFX)
        {
            WarpSFX.GetComponent<AudioSource>().Play();
        }
        Camera.main.GetComponent<MouseOrbit>().doShake();
        WarpAmbientSFX.GetComponent<AudioSource>().enabled = true;
    }

    public void Start()
    {
        if(exhausts.Length > 0)
        {
            exhaustInitScale = exhausts[0].transform.localScale;
        }
    }

    private void Update()
    {

        //Warp flash

        if(GameManager.shipTravelState == GameManager.TravelStates.Warping)
        {
            flareint = Mathf.MoveTowards(flareint, 1, flareSpeed);
        }
        else
        {
            flareint = Mathf.MoveTowards(flareint, 0, flareSpeed);
        }

        flareBrightness = flareCurve.Evaluate(flareint) * flareMultiplier;


        foreach (GameObject go in flares)
        {
            go.GetComponent<LensFlare>().brightness = flareBrightness;
        }

        //Warp Particles

        particleInt = GameManager.CelestialCam.GetComponent<CelestialCamera>().WarpSpeed;
        particleColor.a = particleInt * particleMultiplier;

        foreach(GameObject go in particles)
        {
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = ps.main;
            Color tcol = new Color();
            tcol = particleColor;


            main.startColor = tcol;
        }

        //Thrusters
        exhaustScale = new Vector3((exhaustScaleMultiplier * GetComponent<ShipController>().flightSpeed) * exhaustInitScale.x, exhaustInitScale.y, exhaustInitScale.z);

        foreach(GameObject go in exhausts)
        {
            go.transform.localScale = exhaustScale;
        }
    }
}
