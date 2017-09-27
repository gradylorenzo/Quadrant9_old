using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPointGizmo : MonoBehaviour {

    public float length = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(new Ray(transform.position, Vector3.up * length));
        Gizmos.DrawRay(new Ray(transform.position, Vector3.down * length));
        Gizmos.DrawRay(new Ray(transform.position, Vector3.left * length));
        Gizmos.DrawRay(new Ray(transform.position, Vector3.right * length));
        Gizmos.DrawRay(new Ray(transform.position, Vector3.forward * length));
        Gizmos.DrawRay(new Ray(transform.position, Vector3.back * length));
    }
}
