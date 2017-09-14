using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem : MonoBehaviour {

    public float SystemScale;
    public GameObject SceneDirectionalLight;
    public GameObject CelestialCamera;

    private void Awake()
    {
        SystemScale = this.transform.localScale.x;
    }

    private void Update()
    {
        if (SceneDirectionalLight && CelestialCamera)
        {
            SceneDirectionalLight.transform.rotation = Quaternion.LookRotation(CelestialCamera.transform.position);
        }
    }
}
