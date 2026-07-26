using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXListPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clipList;
    [SerializeField] private bool shouldLoop = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = shouldLoop;
    }

    public void Play(int index = -1)
    {
        if (audioSource == null)
            return;

        if (index < 0)
            index = Random.Range(0, clipList.Count);
        audioSource.clip = clipList[index];
        audioSource.Play();
    }

    public void Stop()
    {
        if (audioSource == null)
            return;

        audioSource.Stop();
        audioSource.time = 0;
    }
}
