using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModuleHost : MonoBehaviour {

    public ShipModule[] modules;

    private void Start()
    {
        modules[0] = new CruiseLaunchSystemT2();
        modules[1] = new ShieldBoosterT2();
        modules[2] = new ShieldBoosterT2();
    }

    private void Update()
    {
        foreach(ShipModule sm in modules)
        {
            if (sm != null)
            {
                sm.UpdateModule(this.GetComponent<ShipWeapons>().SelectedTarget, gameObject);
            }
        }
    }

    public void updateEquippedModules (string[] mods)
    {
        List<ShipModule> tempMods = new List<ShipModule>();

        if(mods.Length == modules.Length && !GetComponent<ShipSheet>().Defense.isDestroyed)
        {
            foreach(string m in mods)
            {
                tempMods.Add(ShipModuleLibrary.Modules[m]);
            }

            modules = tempMods.ToArray();
            GameManager.Interface.createNewNotification("<color=cyan>Modules updated.</color>", notification.NotificationType.error);
        }
        else
        {
            GameManager.Interface.createNewNotification("<color=red>Bad module count. You bad. Stop hacking.</color>", notification.NotificationType.error);
        }
    }
}