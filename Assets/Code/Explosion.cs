using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float lifetime;

    public AudioClip[] sounds;

    private void Start()
    {
        int i;
        i = Random.Range(0, sounds.Length);
        this.GetComponent<AudioSource>().PlayOneShot(sounds[i]);
        StartCoroutine("d");
    }

    private IEnumerator d()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if(GameManager.shipTravelState == GameManager.TravelStates.Warping)
        {
            Destroy(gameObject);
        }
    }
}
