using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixer audioMixer;

    public float MasterVolume { get; private set; }
    public float BGMVolume { get; private set; }
    public float SFXVolume {  get; private set; }

    private AudioSource bgmSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        bgmSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        float master;
        audioMixer.GetFloat("MasterVolume", out master);
        MasterVolume = master;

        float bgm;
        audioMixer.GetFloat("BGMVolume", out bgm);
        BGMVolume = bgm;

        float sfx;
        audioMixer.GetFloat("SFXVolume", out sfx);
        SFXVolume = sfx;
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void UpdateBGMSuspension(bool shouldPause)
    {
        if (shouldPause)
        {
            bgmSource.Pause();
        }
        else
        {
            bgmSource.UnPause();
        }
    }

    public void UpdateMasterVolume(float value)
    {
        MasterVolume = value;
        audioMixer.SetFloat("MasterVolume", MasterVolume);
    }

    public void UpdateBGMVolume(float value)
    {
        BGMVolume = value;
        audioMixer.SetFloat("BGMVolume", BGMVolume);
    }

    public void UpdateSFXVolume(float value)
    {
        SFXVolume = value;
        audioMixer.SetFloat("SFXVolume", SFXVolume);
    }
}
