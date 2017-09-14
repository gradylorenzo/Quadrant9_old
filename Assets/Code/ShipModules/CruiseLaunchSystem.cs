using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CruiseLaunchSystem : ShipModule
{
    public GameObject MissilePrefab;

    public CruiseLaunchSystem(float damage = 0, string moduleName = "", Texture2D modIcon = null, float cooldown = 5, string desc = "")
        : base("Cruise Launch System TEMPLATE", modIcon, cooldown)
    {
        this.damage = damage;
        this.moduleName = moduleName;
        this.cooldown = cooldown;
        this.description = "Launches warheads from hardpoints located on the ship. " + damage + " per hardpoint per " + cooldown + " seconds.";
    }

    public float _damage;

    public float damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public override void DoEffect(GameObject target, GameObject user)
    {
        MissilePrefab = Resources.Load("Prefabs/cruise_missile") as GameObject;

        if (Time.time > lastFire)
        {
            if (!user.GetComponent<ShipSheet>().Defense.isDestroyed)
            {
                if (user.GetComponent<ShipWeapons>().SelectedTarget)
                {
                    MissilePrefab.GetComponent<Missile>().damage = _damage;
                    MissilePrefab.GetComponent<Missile>().target = user.GetComponent<ShipWeapons>().SelectedTarget;
                    foreach (GameObject go in user.GetComponent<ShipWeapons>().Hardpoints)
                    {
                        Instantiate(MissilePrefab, go.transform.position, go.transform.rotation);
                    }
                }
                else
                {
                    GameManager.Interface.createNewNotification("No target for " + moduleName + ". Deactivating.", notification.NotificationType.error);
                    this.Activate();
                }
                lastFire = Time.time + cooldown;
            }
            else
            {
                Activate();
            }
        }
    }
}

public class CruiseLaunchSystemT1 : CruiseLaunchSystem
{
    static public float missileDamage = 800;
    static public string modName = "Cruise Launch System T1";
    static public float cdown = 5;

    static public Texture2D modIcon = Resources.Load("GUI/icon_cruiselaunchsystemt1") as Texture2D;

    public CruiseLaunchSystemT1()
        : base(missileDamage, modName, modIcon, cdown)
    {

    }
}

public class CruiseLaunchSystemT2 : CruiseLaunchSystem
{
    static public float missileDamage = 1600;
    static public string modName = "Cruise Launch System T2";
    static public float cdown = 5;

    static public Texture2D modIcon = Resources.Load("GUI/icon_cruiselaunchsystemt2") as Texture2D;

    public CruiseLaunchSystemT2()
        : base(missileDamage, modName, modIcon, cdown)
    {

    }
}
