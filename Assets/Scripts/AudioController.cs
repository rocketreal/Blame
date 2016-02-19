using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    private float volume;
    public void PlayFadeIn()
    {
        volume = 0;
        //InvokeRepeating("FadeInVolume", 0, 0.5f);
        //audioSource.volume = 1;
        GetComponent<AudioSource>().Play();
    }

    public void StopFadeOut()
    {
        GetComponent<AudioSource>().Stop();
        volume = 1;
        //InvokeRepeating("FadeOutVolume", 0, 0.5f);
    }

    private void FadeInVolume()
    {
        GetComponent<AudioSource>().volume = volume;
        GetComponent<AudioSource>().Play();
        volume += 0.05f;
        if (volume>=1f)
        {
            CancelInvoke("FadeInVolume");
        }
    }


    private void FadeOutVolume()
    {
        GetComponent<AudioSource>().volume = volume;
        volume -= 0.05f;
        if (volume <= 0.0f)
        {
            CancelInvoke("FadeInVolume");
            GetComponent<AudioSource>().Stop();
        }
    }
}
