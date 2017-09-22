using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour {

    public string targetName;

    [System.Serializable]
    public enum targetTypes
    {
        hostile,
        neutral,
        friendly,
        player,
        station,
        asteroid,
        wreck
    }

    public targetTypes Type;
    public GameObject explosion;

    public DefensiveStats Defense;

    public Texture2D image;

    public int Bounty = 100;

    private void Start()
    {
        if (GameManager.PlayerShip)
        {
            GameManager.PlayerShip.GetComponent<ShipWeapons>().AddAvailableTarget(gameObject);
        }

        Defense.curShield = Defense.maxShield;
        Defense.curArmor = Defense.maxArmor;
        Defense.curStruct = Defense.maxStruct;

        this.gameObject.tag = "Targetable";
    }

    private void OnMouseUpAsButton()
    {
        if (GameManager.PlayerShip)
        {
            if (!Defense.isImmune)
            {
                GameManager.PlayerShip.GetComponent<ShipWeapons>().AddLockedTarget(gameObject);
            }
        }
    }

    public void takeDamage(float d, Weapon.DamageTypes t, string name)
    {
        if (!Defense.isImmune)
        {
            float remainingD = 0;

            #region damagecalc
            #region shield
            if (Defense.curShield > 0)
            {
                float damageFinal = 0.0f;

                switch (t)
                {
                    case Weapon.DamageTypes.electromagnetic:
                        damageFinal = d - (d * Defense.shieldFinalResistance.electromagnetic);
                        break;
                    case Weapon.DamageTypes.kinetic:
                        damageFinal = d - (d * Defense.shieldFinalResistance.kinetic);
                        break;
                    case Weapon.DamageTypes.thermal:
                        damageFinal = d - (d * Defense.shieldFinalResistance.thermal);
                        break;
                    case Weapon.DamageTypes.explosive:
                        damageFinal = d - (d * Defense.shieldFinalResistance.explosive);
                        break;
                }

                if (damageFinal > Defense.curShield)
                {
                    remainingD = damageFinal - Defense.curShield;
                    Defense.curShield = 0;
                    takeDamage(remainingD, t, name);
                }
                else
                {
                    Defense.curShield -= damageFinal;

                    GameManager.Interface.createNewNotification("<color=cyan>" + (Convert.ToInt32(damageFinal) + "</color> damage > " + targetName), notification.NotificationType.damageDone);
                }
            }
            #endregion
            #region armor
            else if (Defense.curArmor > 0)
            {
                float damageFinal = 0.0f;

                switch (t)
                {
                    case Weapon.DamageTypes.electromagnetic:
                        damageFinal = d - (d * Defense.armorFinalResistance.electromagnetic);
                        break;
                    case Weapon.DamageTypes.kinetic:
                        damageFinal = d - (d * Defense.armorFinalResistance.kinetic);
                        break;
                    case Weapon.DamageTypes.thermal:
                        damageFinal = d - (d * Defense.armorFinalResistance.thermal);
                        break;
                    case Weapon.DamageTypes.explosive:
                        damageFinal = d - (d * Defense.armorFinalResistance.explosive);
                        break;
                }

                if (damageFinal > Defense.curArmor)
                {
                    remainingD = damageFinal - Defense.curArmor;
                    Defense.curArmor = 0;
                    takeDamage(remainingD, t, name);
                }
                else
                {
                    Defense.curArmor -= damageFinal;

                    GameManager.Interface.createNewNotification("<color=cyan>" + (Convert.ToInt32(damageFinal) + "</color> damage > " + targetName), notification.NotificationType.damageDone);
                }
            }
            #endregion
            #region structure
            else if (Defense.curStruct > 0)
            {
                float damageFinal = 0.0f;

                switch (t)
                {
                    case Weapon.DamageTypes.electromagnetic:
                        damageFinal = d - (d * Defense.structFinalResistance.electromagnetic);
                        break;
                    case Weapon.DamageTypes.kinetic:
                        damageFinal = d - (d * Defense.structFinalResistance.kinetic);
                        break;
                    case Weapon.DamageTypes.thermal:
                        damageFinal = d - (d * Defense.structFinalResistance.thermal);
                        break;
                    case Weapon.DamageTypes.explosive:
                        damageFinal = d - (d * Defense.structFinalResistance.explosive);
                        break;
                }

                if (damageFinal > Defense.curStruct)
                {
                    remainingD = damageFinal - Defense.curStruct;
                    Defense.curStruct = 0;
                }
                else
                {
                    Defense.curStruct -= damageFinal;

                    GameManager.Interface.createNewNotification("<color=cyan>" + (Convert.ToInt32(damageFinal) + "</color> damage > " + targetName), notification.NotificationType.damageDone);
                }
            }
            #endregion
            #endregion


            if (Defense.curShield + Defense.curArmor + Defense.curStruct <= 0)
            {
                GameManager.PlayerShip.GetComponent<ShipWeapons>().ClearDeadTarget(gameObject);
                if (explosion)
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }
                GameManager.Interface.createNewNotification((targetName + " destroyed"), notification.NotificationType.damageDone);
                GameManager.addCredits(Bounty);
                Debug.Log(name);
                Defense.isImmune = true;
                Destroy(gameObject);
            }
        }
    }
}
