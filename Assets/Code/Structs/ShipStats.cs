using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DefensiveStats {

    [System.Serializable]
    public struct ResistanceProfile
    {
        public float electromagnetic, kinetic, thermal, explosive;
    }

    public ResistanceProfile shieldBaseResistance, armorBaseResistance, structBaseResistance;
    public ResistanceProfile shieldFinalResistance, armorFinalResistance, structFinalResistance;

    public float maxShield, maxArmor, maxStruct;
    public float curShield, curArmor, curStruct;

    public float shieldBaseRegen, shieldFinalRegen;

    public bool isImmune;
    public bool isDestroyed;
}


[System.Serializable]
public class OffensiveStats
{

}
