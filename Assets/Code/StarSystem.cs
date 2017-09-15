using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarSystem : MonoBehaviour {

    public string SystemName = "TestSystem";
    public int uniqueID = 0;
    public float SystemScale;
    public GameObject SceneDirectionalLight;
    public GameObject CelestialCamera;

    private void Start()
    {
        SystemScale = this.transform.localScale.x;
        GameManager.currentScene = SystemName;
        GameManager.SystemRoot = gameObject;
        GameManager.setHomeStation("A1", new Vector3(-28.4f, 0.0f, -12.0f), "player_station_1");
        if (!GameManager.PlayerShip)
        {
            GameManager.respawn();
        }
    }

    public void SpawnShip(GameObject ps)
    {
        Instantiate(ps, transform.position, transform.rotation);
    }

    private void Update()
    {
        if (SceneDirectionalLight && CelestialCamera)
        {
            SceneDirectionalLight.transform.rotation = Quaternion.LookRotation(CelestialCamera.transform.position);
        }
    }
}
