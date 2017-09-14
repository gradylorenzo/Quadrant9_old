using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Weapon
{
    [System.Serializable]
	public enum DamageTypes
    {
        electromagnetic,
        kinetic,
        thermal,
        explosive
    }

    [System.Serializable]
    public enum WeaponTypes
    {
        missile,
        railgun,
        drone,
        laser
    }

    public DamageTypes damageType;
    public WeaponTypes weaponType;
    public float baseDamage;
    public float range;
    public float cooldown;
    public float lastFire;
}
