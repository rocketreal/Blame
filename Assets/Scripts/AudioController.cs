using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    AudioSource audioSource = null;
    private float volume;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFadeIn()
    {
        volume = 0;
        //InvokeRepeating("FadeInVolume", 0, 0.5f);
        //audioSource.volume = 1;
        audioSource.Play();
    }

    public void StopFadeOut()
    {
        audioSource.Stop();
        volume = 1;
        //InvokeRepeating("FadeOutVolume", 0, 0.5f);
    }

    private void FadeInVolume()
    {
        audioSource.volume = volume;
        audioSource.Play();
        volume += 0.05f;
        if (volume>=1f)
        {
            CancelInvoke("FadeInVolume");
        }
    }


    private void FadeOutVolume()
    {
        audioSource.volume = volume;
        volume -= 0.05f;
        if (volume <= 0.0f)
        {
            CancelInvoke("FadeInVolume");
            audioSource.Stop();
        }
    }
}
