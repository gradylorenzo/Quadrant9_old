using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteManager : MonoBehaviour
{

    public Vector3[] AIPoints;
    public Vector3[] AISpawns;
    public float gizmoSize = 1.0f;
    private void Start()
    {
        GameManager.RegisterSiteManager(this);
    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 v in AIPoints)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(v, gizmoSize);
        }

        foreach (Vector3 v in AISpawns)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(v, gizmoSize);
        }
    }
}
