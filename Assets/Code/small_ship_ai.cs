using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class small_ship_ai : MonoBehaviour {

    public Weapon shipWeapon;
    public GameObject WeaponEffects;
    public bool showWeaponEffects;
    public float WeaponEffectsDuration;

    public GameObject target;

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
                WeaponEffects.GetComponent<LineRenderer>().enabled = showWeaponEffects;
                WeaponEffects.GetComponent<LineRenderer>().SetPosition(0, WeaponEffects.transform.position);
                WeaponEffects.GetComponent<LineRenderer>().SetPosition(1, target.transform.position);
            }
        }
    }
}
