using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour {

    public float lifetime;

    public AudioClip[] sounds;

    private void Start()
    {
        int i;
        i = Random.Range(0, sounds.Length);
        this.GetComponent<AudioSource>().PlayOneShot(sounds[i]);
        StartCoroutine("d");
        SceneManager.sceneLoaded += destroyThis;
    }

    private IEnumerator d()
    {
        yield return new WaitForSeconds(lifetime);
        SceneManager.sceneLoaded -= destroyThis;
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if(GameManager.shipTravelState == GameManager.TravelStates.Warping)
        {
            SceneManager.sceneLoaded -= destroyThis;
            Destroy(gameObject);
        }
        
    }
    
    void destroyThis(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= destroyThis;
        Destroy(this.gameObject);
    }
}
