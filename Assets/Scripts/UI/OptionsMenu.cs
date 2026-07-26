using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] OptionsSliderElement sensitivityElement;
    [SerializeField] OptionsSliderElement masterVolumeElement;
    [SerializeField] OptionsSliderElement bgmVolumeElement;
    [SerializeField] OptionsSliderElement sfxVolumeElement;

    private GameSettings GameSettings;
    private AudioManager audioManager;
    private GameState previousState;

    private void Start()
    {
        GameSettings = GameManager.instance.GameSettings;
        audioManager = AudioManager.instance;

        sensitivityElement.Initialize(0, 150, GameSettings.MouseSensitivity);
        masterVolumeElement.Initialize(-80, 20, audioManager.MasterVolume);
        bgmVolumeElement.Initialize(-80,20,audioManager.BGMVolume);
        sfxVolumeElement.Initialize(-80,20,audioManager.SFXVolume);

        sensitivityElement?.EvtSliderValueChanged.AddListener(OnSensitivitySliderValueChanged);
        masterVolumeElement?.EvtSliderValueChanged.AddListener(OnMasterSliderValueChanged);
        bgmVolumeElement?.EvtSliderValueChanged.AddListener(OnBGMSliderValueChanged);
        sfxVolumeElement?.EvtSliderValueChanged.AddListener(OnSFXSliderValueChanged);

    }

    private void OnSensitivitySliderValueChanged(float value)
    {
        GameSettings.MouseSensitivity = value;
    }

    private void OnMasterSliderValueChanged(float value)
    {
        audioManager.UpdateMasterVolume(value);
    }

    private void OnBGMSliderValueChanged(float value)
    {
        audioManager.UpdateBGMVolume(value);
    }

    private void OnSFXSliderValueChanged(float value)
    {
        audioManager.UpdateSFXVolume(value);
    }

    private void OnDestroy()
    {
        sensitivityElement?.EvtSliderValueChanged.RemoveAllListeners();
        masterVolumeElement?.EvtSliderValueChanged.RemoveAllListeners();
        bgmVolumeElement?.EvtSliderValueChanged.RemoveAllListeners();
        sfxVolumeElement?.EvtSliderValueChanged?.RemoveAllListeners();
    }
}
