using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSheet : MonoBehaviour {

    public DefensiveStats Defense;

    public GameObject explosion;

    private string sclass;

    private void Start()
    {
        Defense.curShield = Defense.maxShield;
        Defense.curArmor = Defense.maxArmor;
        Defense.curStruct = Defense.maxStruct;

        Defense.shieldFinalResistance = Defense.shieldBaseResistance;
        Defense.armorFinalResistance = Defense.armorBaseResistance;
        Defense.structFinalResistance = Defense.structBaseResistance;

        Defense.shieldFinalRegen = Defense.shieldBaseRegen;

        sclass = GetComponent<ShipController>().shipClass.ToString();
    }

    public void takeDamage(float d, Weapon.DamageTypes t)
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
                    takeDamage(remainingD, t);
                }
                else
                {
                    Defense.curShield -= damageFinal;

                    GameManager.Interface.createNewNotification("<color=red>" + (Convert.ToInt32(damageFinal) + "</color> damage > Your " + sclass), notification.NotificationType.damageDone);
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
                    takeDamage(remainingD, t);
                }
                else
                {
                    Defense.curArmor -= damageFinal;

                    GameManager.Interface.createNewNotification("<color=red>" + (Convert.ToInt32(damageFinal) + "</color> damage > Your " + sclass), notification.NotificationType.damageDone);
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
                    takeDamage(remainingD, t);
                    Defense.isDestroyed = true;
                    doDestroySequence();
                }
                else
                {
                    Defense.curStruct -= damageFinal;

                    GameManager.Interface.createNewNotification("<color=red>" + (Convert.ToInt32(damageFinal) + "</color> damage > Your " + sclass), notification.NotificationType.damageDone);
                }
            }
#endregion
            #endregion
        }
        else
        {
            GameManager.Interface.createNewNotification("<color=red>0</color> damage > Your " + sclass, notification.NotificationType.damageDone);
        }
    }

    public void takeHeals(ShipModule.SubtargetTypes st, float a)
    {
        switch (st)
        {
            case ShipModule.SubtargetTypes.shield:
                Defense.curShield += a;
                break;
            case ShipModule.SubtargetTypes.armor:
                Defense.curArmor += a;
                break;
            case ShipModule.SubtargetTypes.structure:
                Defense.curStruct += a;
                break;
        }

        GameManager.Interface.createNewNotification("<color=green>" + (Convert.ToInt32(a) + "</color> repaired > Your " + sclass), notification.NotificationType.repaired);
    }

    public void Update()
    {
        if(Defense.curShield > Defense.maxShield)
        {
            Defense.curShield = Defense.maxShield;
        }
        if (Defense.curArmor > Defense.maxArmor)
        {
            Defense.curArmor = Defense.maxArmor;
        }
        if (Defense.curStruct > Defense.maxStruct)
        {
            Defense.curStruct = Defense.maxStruct;
        }
    }

    private void FixedUpdate()
    {
        if (!Defense.isDestroyed)
        {
            Defense.curShield += Defense.shieldFinalRegen;
        }
    }

    private void doDestroySequence()
    {
        if (explosion)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        GameManager.Interface.createNewNotification("<color=red>Your " + sclass + " was destroyed</color>", notification.NotificationType.damageDone);
    }
}
