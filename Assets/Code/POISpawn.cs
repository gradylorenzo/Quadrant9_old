using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISpawn : MonoBehaviour {

    public GameObject[] spawnablePointsOfInterest;

    public Vector3 Range = new Vector3();
    public float SpawnRateInSeconds = 120.0f;

    public bool drawGizmos = false;
    private float lastSpawnTime;


    private void FixedUpdate()
    {
        if(Time.time > lastSpawnTime + SpawnRateInSeconds)
        {
            doSpawn();
            lastSpawnTime = Time.time;
        }
    }

    private void doSpawn()
    {
        Vector3 SpawnPosition = new Vector3(Random.Range(-Range.x, Range.x), Random.Range(-Range.y, Range.y), Random.Range(-Range.z, Range.z));
        int poiToSpawn = Random.Range(0, spawnablePointsOfInterest.Length);
        Instantiate(spawnablePointsOfInterest[poiToSpawn], SpawnPosition, new Quaternion(0, 0, 0, 0));
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, Range);
        }
    }
}
