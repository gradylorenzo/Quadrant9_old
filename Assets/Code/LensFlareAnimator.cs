using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensFlareAnimator : MonoBehaviour {

    public AnimationCurve curve = new AnimationCurve();
    public float timeSinceSpawn = 0;

    public void Awake()
    {
        if (GetComponent<LensFlare>())
        {
            GetComponent<LensFlare>().brightness *= 1;
        }
    }

    public void Update()
    {
        if (GetComponent<LensFlare>())
        {
            GetComponent<LensFlare>().brightness *= curve.Evaluate(timeSinceSpawn);
        }
        else
        {
            Debug.LogWarning("NO LENS FLARE ATTACHED");
        }
        timeSinceSpawn += Time.deltaTime;
    }
}
