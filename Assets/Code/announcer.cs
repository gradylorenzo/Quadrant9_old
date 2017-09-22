using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class announcer : MonoBehaviour {

	public enum announcement
    {
        warpCountdown,
    }

    public announcement type;
    public AudioClip[] announcements;

    public void announce (announcement a)
    {
        switch (a)
        {
            case announcement.warpCountdown:
                GetComponent<AudioSource>().PlayOneShot(announcements[0]);
                break;
        }
    }
}
