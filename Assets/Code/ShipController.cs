using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float warpCharge = 0.0f;
    public float warpChargeSpeed = 0.1f;

    public float turnSpeed;
    public AnimationCurve turnSpeedCurve = new AnimationCurve();
    public float maxTurnSpeed;

    public ShipClasses shipClass;

    private void Start()
    {
        GameManager.setPlayerShip(this.gameObject);
    }

    private void FixedUpdate()
    {
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
        }

        if(turnSpeed < maxTurnSpeed)
        {
            turnSpeed = Mathf.Clamp(turnSpeed + .01f, 0, maxTurnSpeed);
        }
    }
}
