using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VolumeController : MonoBehaviour {

    private float localVolume;
    public Settings.volumeTypes Type;

    private void Awake()
    {
        localVolume = this.GetComponent<AudioSource>().volume;
    }

    private void LateUpdate()
    {
        switch (Type)
        {
            case Settings.volumeTypes.sfx:
                this.GetComponent<AudioSource>().volume = Settings.Volume.sfx * localVolume;
                break;
            case Settings.volumeTypes.music:
                this.GetComponent<AudioSource>().volume = Settings.Volume.music * localVolume;
                break;
            case Settings.volumeTypes.ui:
                this.GetComponent<AudioSource>().volume = Settings.Volume.ui * localVolume;
                break;
            case Settings.volumeTypes.voice:
                this.GetComponent<AudioSource>().volume = Settings.Volume.voice * localVolume;
                break;
        }
    }
}
