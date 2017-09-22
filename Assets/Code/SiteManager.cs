using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteManager : MonoBehaviour {

    public  GameObject[] AIPoints;

    private void Start()
    {
        GameManager.RegisterSiteManager(this);
    }
}
