using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeapons : MonoBehaviour {

    public GameObject[] Hardpoints;
    public List<GameObject> LockedTargets;
    public List<GameObject> AvailableTargets;
    public GameObject SelectedTarget;
    public int MaxTargets;

    public void AddLockedTarget (GameObject go)
    {
        bool duplicatetarg = false;
        foreach (GameObject listgo in LockedTargets)
        {
            if(listgo == go)
            {
                duplicatetarg = true;
            }
        }

        if (!duplicatetarg)
        {
            if(LockedTargets.Count < MaxTargets)
            {
                if(go.GetComponent<Targetable>().Type == Targetable.targetTypes.hostile || go.GetComponent<Targetable>().Type == Targetable.targetTypes.neutral)
                LockedTargets.Add(go);
            }
        }
    }

    public void AddAvailableTarget(GameObject go)
    {
        AvailableTargets.Add(go);
    }

    public void ClearLockedTargets()
    {
        LockedTargets.Clear();
        setSelectedTarget(null);
    }

    public void ClearAvailableTargets()
    {
        AvailableTargets.Clear();
    }

    public void setSelectedTarget (GameObject go)
    {
        SelectedTarget = go;
    }

    public void ClearDeadTarget (GameObject go)
    {
        if(SelectedTarget == go)
        {
            SelectedTarget = null;
        }

        if (LockedTargets.Contains(go))
        {
            LockedTargets.Remove(go);
        }

        if (AvailableTargets.Contains(go))
        {
            AvailableTargets.Remove(go);
        }
    }

    public void Update()
    {
        if(!SelectedTarget && LockedTargets.Count > 0)
        {
            setSelectedTarget(LockedTargets[0]);
        }
    }
}
