using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour {

    public string poiName;
    public string poiType;
    public string sceneToLoad;

    private void Start()
    {
        GameManager.AddPointOfInterest(gameObject);
        GameManager.Interface.createNewNotification("Sensors have detected a new anomaly: " + poiName, notification.NotificationType.standard);
    }
}
