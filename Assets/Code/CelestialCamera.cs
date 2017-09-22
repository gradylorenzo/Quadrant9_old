using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialCamera : MonoBehaviour {

    [SerializeField]
    private Vector3 destinationTrue;

    [SerializeField]
    private Vector3 destinationFake;

    [SerializeField]
    private AnimationCurve WarpCurve = new AnimationCurve(new Keyframe(0,0), new Keyframe(50.0f, 2.0f));
    public float WarpSpeed;
    private float WarpDistance;

    private void Start()
    {
        GameManager.setCelestialCamera(this.GetComponent<Camera>());
    }

    private void FixedUpdate()
    {
        if(GameManager.shipTravelState == GameManager.TravelStates.Warping)
        {
            WarpDistance = Vector3.Distance(this.transform.position, GameManager.WarpTarget.transform.position);
            WarpSpeed = WarpCurve.Evaluate(WarpDistance);
            transform.position = Vector3.MoveTowards(this.transform.position, GameManager.WarpTarget.transform.position, WarpSpeed);

            if (WarpDistance <= .01)
            {
                GameManager.exitWarp();
            }
        }
    }
}
