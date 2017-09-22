using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour {

    public GameObject Target;

    private void Update()
    {
        if (Target)
        {
            transform.LookAt(Target.transform);
        }
        else
        {
            Target = Camera.main.gameObject;
        }
    }
}
