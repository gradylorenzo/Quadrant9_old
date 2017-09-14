using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float delay = 7;

    private void Start()
    {
        Invoke("Destroythis", delay);
    }

    private void Update()
    {
        if(GameManager.shipTravelState == GameManager.TravelStates.Warping)
        {
            Destroythis();
        }
    }

    private void Destroythis()
    {
        Destroy(gameObject);
    }
}
