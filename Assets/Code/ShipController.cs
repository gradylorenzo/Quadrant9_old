using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour {

    public float warpCharge = 0.0f;
    public float warpChargeSpeed = 0.1f;
    public bool countdown;
    public float countdownFinish;
    public float timeRemaining;

    public float flightSpeed;
    public float maxFlightSpeed;
    public float flightAcceleration;
    public float wantedFlightSpeed;

    public float turnSpeed;
    public AnimationCurve turnSpeedCurve = new AnimationCurve();
    public float maxTurnSpeed;
    public ShipClasses shipClass;
    public bool inWarp = false;

    private float warpFlareInt;
    private float wantedWarpFlareInt;

    private void Start()
    {
        GameManager.setPlayerShip(this.gameObject);
    }

    private void FixedUpdate()
    {
        flightSpeed = Mathf.MoveTowards(flightSpeed, wantedFlightSpeed, flightAcceleration);

        if(GameManager.shipTravelState == GameManager.TravelStates.Aligning || GameManager.shipTravelState == GameManager.TravelStates.Aligned)
        {
            wantedFlightSpeed = maxFlightSpeed;
            transform.Translate(0, 0, flightSpeed);
        }
        else
        {
            //wantedFlightSpeed = 0;
        }
        

        if (GameManager.shipTravelState == GameManager.TravelStates.Aligning)
        {
            Vector3 relativePos = GameManager.WarpTarget.transform.position - GameManager.CelestialCam.transform.position;
            Quaternion alignRotation = Quaternion.LookRotation(relativePos);
            float ang = Quaternion.Angle(transform.rotation, alignRotation);
            float amp = turnSpeedCurve.Evaluate(ang);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, alignRotation, turnSpeed * amp);
            


            if(Quaternion.Angle(this.transform.rotation, alignRotation) < 1.0f && warpCharge < 10)
            {
                warpCharge = Mathf.Clamp(warpCharge += warpChargeSpeed, 0, 10);
            }

            if(Quaternion.Angle(this.transform.rotation, alignRotation) < 1.0f && warpCharge == 10)
            {
                GameManager.setTravelState(GameManager.TravelStates.Aligned);
            }
        }
        else if(GameManager.shipTravelState == GameManager.TravelStates.Warping)
        {
            warpCharge = 0;
            this.transform.position = Vector3.zero;
            GetComponent<ShipWeapons>().ClearLockedTargets();
            flightSpeed = maxFlightSpeed;
            wantedFlightSpeed = maxFlightSpeed;
        }

        if(turnSpeed < maxTurnSpeed)
        {
            turnSpeed = Mathf.Clamp(turnSpeed + .01f, 0, maxTurnSpeed);
        }
    }

    private void Update()
    {
        if (countdown)
        {
            timeRemaining = countdownFinish - Time.time;

            if (timeRemaining <= 0)
            {
                GameManager.enterWarp();
                countdown = false;
                inWarp = true;
            }
        }
    }

    public void doCountdown()
    {
        countdown = true;
        countdownFinish = Time.time + 5;
        Camera.main.transform.Find("announcer").GetComponent<announcer>().announce(announcer.announcement.warpCountdown);
    }
}
