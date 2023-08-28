
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private bool isPlayingSource1 = true;

    void Start()
    {
        // Play the first audio source
        PlayNextAudioSource();
    }

    void PlayNextAudioSource()
    {
        if (isPlayingSource1)
        {
            isPlayingSource1 = false;
            PlayAudioSource(audioSource1);
        }
        else
        {
            isPlayingSource1 = true;
            PlayAudioSource(audioSource2);
        }
    }

    void PlayAudioSource(AudioSource audioSource)
    {
        audioSource.Play();
        StartCoroutine(WaitForAudioFinish(audioSource));
    }

    IEnumerator WaitForAudioFinish(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        // Play the next audio source when the current one finishes
        PlayNextAudioSource();
    }
}
