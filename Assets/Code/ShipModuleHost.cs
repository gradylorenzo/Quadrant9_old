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

        #region F-keys
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(modules[0])
            modules[0].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (modules[1])
                modules[1].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (modules[2])
                modules[2].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (modules[3])
                modules[3].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (modules[4])
                modules[4].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            if (modules[5])
                modules[5].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            if (modules[6])
                modules[6].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            if (modules[7])
                modules[7].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (modules[8])
                modules[8].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (modules[9])
                modules[9].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            if (modules[10])
                modules[10].Activate();
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (modules[11])
                modules[11].Activate();
        }
        #endregion
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