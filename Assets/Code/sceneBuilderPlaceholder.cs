using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneBuilderPlaceholder : MonoBehaviour {

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
