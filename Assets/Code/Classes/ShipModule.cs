using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModule : ScriptableObject
{
    [System.Serializable]
    public enum SubtargetTypes
    {
        shield,
        armor,
        structure,
        capacitor
    }

    private string _moduleName;
    private Texture2D _icon;
    private float _cooldown = 1;
    private string _description;

    public string moduleName
    {
        get { return _moduleName; }
        set { _moduleName = value; }
    }

    public Texture2D icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    public float cooldown
    {
        get { return _cooldown; }
        set { _cooldown = value; }
    }

    public string description
    {
        get { return _description; }
        set { _description = value; }
    }

    public ShipModule(string moduleName = "", Texture2D icon = null, float cooldown = 5, string description = "")
    {
        this.moduleName = moduleName;
        if (icon)
        {
            this.icon = icon;
        }
        this.cooldown = cooldown;
        this.description = description;
    }

    public SubtargetTypes Subtarget;

    public float lastFire;
    public bool activated = false;
    public bool queueActivate = false;
    public bool queueDeactivate = false;




    public void Activate()
    {
        if (!activated)
        {
            queueActivate = true;
            Debug.Log("queueing activation");
        }
        else
        {
            queueDeactivate = true;
            Debug.Log("queueing deactivation");
        }
    }


    public void UpdateModule(GameObject t, GameObject u)
    {
        if (queueActivate)
        {
            activated = true;
            queueActivate = false;
        }

        if (activated)
        {
            if (Time.time > lastFire + cooldown || lastFire == 0)
            {
                if (!queueDeactivate)
                {
                    DoEffect(t, u);
                    Debug.Log(moduleName + " activated");
                    lastFire = Time.time;
                }
                else
                {
                    activated = false;
                    queueDeactivate = false;
                    Debug.Log(moduleName + " deactivated");
                }
            }
        }
    }

    public virtual void DoEffect(GameObject target, GameObject user)
    {
        
    }
}

public static class ShipModuleLibrary
{
    public static Dictionary<string, ShipModule> Modules = new Dictionary<string, ShipModule>()
    {
       {"Cruise Launch System T1", new CruiseLaunchSystemT1()},
       {"Cruise Launch System T2", new CruiseLaunchSystemT2()},
       {"Shield Booster T1", new ShieldBoosterT1()},
       {"Shield Booster T2", new ShieldBoosterT2()}
    };
}