using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour {

    public Weapon shipWeapon;
    public GameObject WeaponEffects;
    public bool showWeaponEffects;
    public float WeaponEffectsDuration;


    public GameObject target;

    public float pointChangeInterval = 5.0f;
    public float lastPointChange;
    public Vector3 targetAIPoint = new Vector3();

    public float speed = 1.0f;
    public float maneuverability = 1.0f;
    public bool moving = true;

    private void Start()
    {
        if(GetComponent<Targetable>().Type == Targetable.targetTypes.hostile)
        {
            Invoke("LockPlayer", 10f + Random.Range(0, 10f));
        }

        gameObject.name = GetComponent<Targetable>().targetName;
    }

    private void LockPlayer()
    {
        target = GameManager.PlayerShip;
    }


    private void Update()
    {
        if (target)
        {
            if(Time.time > shipWeapon.lastFire + shipWeapon.cooldown + Random.Range(0, shipWeapon.cooldown / 2))
            {
                if (!target.GetComponent<ShipSheet>().Defense.isDestroyed)
                {
                    target.GetComponent<ShipSheet>().takeDamage(shipWeapon.baseDamage * (Random.Range(0.5f, 2)), shipWeapon.damageType);
                    shipWeapon.lastFire = Time.time;
                    showWeaponEffects = true;
                }
            }

            if(Time.time > shipWeapon.lastFire + WeaponEffectsDuration)
            {
                showWeaponEffects = false;
            }

            if (WeaponEffects)
            {
                WeaponEffects.GetComponent<LineRenderer>().SetPosition(0, WeaponEffects.transform.position);
                WeaponEffects.GetComponent<LineRenderer>().SetPosition(1, target.transform.position);
            }
        }
        else
        {
            showWeaponEffects = false;
        }
        WeaponEffects.GetComponent<LineRenderer>().enabled = showWeaponEffects;


        //AI Movement Controller
        if (moving)
        {
            if (Time.time >= lastPointChange + pointChangeInterval || lastPointChange == 0)
            {
                if (GameManager.SiteManager)
                {
                    int p = Random.Range(0, GameManager.SiteManager.AIPoints.Length);
                    targetAIPoint = GameManager.SiteManager.AIPoints[p];
                    lastPointChange = Time.time;
                }
            }

            if (targetAIPoint != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(targetAIPoint - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, maneuverability);
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }
    }
}
