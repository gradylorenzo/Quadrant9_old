using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBooster : ShipModule
{


    public ShieldBooster(float amount = 0, string moduleName = "", Texture2D modIcon = null, float cooldown = 5, string desc = "")
        : base("Shield Booster TEMPLATE", modIcon, cooldown, desc)
    {
        this.moduleName = moduleName;
        this.amount = amount;
        this.cooldown = cooldown;
    }

    public float _amount;
    public float amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    public override void DoEffect(GameObject target, GameObject user)
    {
        if (Time.time > lastFire)
        {
            if (!user.GetComponent<ShipSheet>().Defense.isDestroyed)
            {
                user.GetComponent<ShipSheet>().takeHeals(SubtargetTypes.shield, _amount);
                lastFire = Time.time + cooldown;
            }
        }
    }
}

public class ShieldBoosterT1 : ShieldBooster
{
    static public float boostAmount = 2750;
    static public string modName = "Shield Booster T1";
    static public float cdown = 5;
    static public string desc = "Periodically replenishes shield integrity while activated. " + boostAmount + " added to shields every " + cdown + " seconds.";

    static public Texture2D modIcon = Resources.Load("GUI/icon_shieldboostert1") as Texture2D;

    public ShieldBoosterT1() : base(boostAmount, modName, modIcon, cdown, desc)
    {

    }
}

public class ShieldBoosterT2 : ShieldBooster
{
    static public float boostAmount = 5500;
    static public string modName = "Shield Booster T2";
    static public float cdown = 5;
    static public string desc = "Periodically replenishes shield integrity while activated. " + boostAmount + " added to shields every " + cdown + " seconds.";

    static public Texture2D modIcon = Resources.Load("GUI/icon_shieldboostert2") as Texture2D;

    public ShieldBoosterT2() : base(boostAmount, modName, modIcon, cdown, desc)
    {

    }
}
