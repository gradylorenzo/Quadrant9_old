using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotation : MonoBehaviour {

    public GameObject target;

    public void Update()
    {
        if (target)
        {
            this.transform.rotation = target.transform.rotation;
        }
        else
        {
            target = Camera.main.gameObject;
        }
    }
}
