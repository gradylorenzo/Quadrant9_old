using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float speed;
    public float acceleration;
    public float damp = 0.1f;
    public GameObject target;
    public GameObject plume;
    public GameObject ExplosionPrefab;
    public float damage;
    public Weapon.DamageTypes damageType;
    public Weapon.WeaponTypes weaponType = Weapon.WeaponTypes.missile;

    private bool allowTargeting = false;
    private bool targetLocked = false;
    private Vector3 relativePos = new Vector3();
    private GameObject selectedST;
    private bool usingST = false;
    private void Start()
    {
        if (this.transform.Find("plume"))
        {
            plume = this.transform.Find("plume").gameObject;
        }
        Invoke("startTarg", 3);
        Invoke("Detonate", 20);
        acceleration = acceleration * Random.Range(1f, 2f);
    }

    private void FixedUpdate()
    {
        if (target && allowTargeting)
        {
            if (!targetLocked)
            {
                if(target.GetComponent<Targetable>().subtargets.Length > 0)
                {
                    int i = Random.Range(0, target.GetComponent<Targetable>().subtargets.Length);
                    selectedST = target.GetComponent<Targetable>().subtargets[i];
                }
                targetLocked = true;
            }

            if (selectedST)
            {
                relativePos = selectedST.transform.position;
            }
            else
            {
                relativePos = target.transform.position;
            }

            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(relativePos - transform.position), damp);
            speed += acceleration;

        }

        transform.Translate(Vector3.forward * speed);
    }

    private void Update()
    {
        if (GameManager.shipTravelState == GameManager.TravelStates.Warping)
        {
            Destroy(gameObject);
        }

        if (!target)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Targetable")
        {
            if (!collision.gameObject.GetComponent<Targetable>().Defense.isImmune)
            {
                collision.gameObject.GetComponent<Targetable>().takeDamage(damage * Random.Range(0.5f, 2f), damageType, this.name);
                Detonate();
            }
        }
    }

    private void Detonate()
    {
        if (plume)
        {
            //plume.transform.parent = null;
        }
        Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void startTarg()
    {
        allowTargeting = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(relativePos, 1);
    }
}
