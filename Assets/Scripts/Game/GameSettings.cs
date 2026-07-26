using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/GameSettings")]
public class GameSettings : ScriptableObject
{
    private const float DefaultMouseSensitivity = 50f;
    private const float DefaultMasterVolume = 0f;
    private const float DefaultBGMVolume = 0f;
    private const float DefaultSFXVolume = 0f;

    public float MouseSensitivity = DefaultMouseSensitivity;
    public float MasterVolume = DefaultMasterVolume;
    public float BGMVolume = DefaultBGMVolume;
    public float SFXVolume = DefaultSFXVolume;

    public void RestoreToDefault()
    {
        MouseSensitivity = DefaultMouseSensitivity;
        MasterVolume = DefaultMasterVolume;
        BGMVolume = DefaultBGMVolume;
        SFXVolume = DefaultSFXVolume;
    }
}
