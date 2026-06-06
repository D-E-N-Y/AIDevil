using UnityEngine;
using UnityEngine.UI;

public class UI_AudioPanel : UI_SettingsPanel 
{
    [SerializeField] private Slider ui_masterVolumeSlider;
    [SerializeField] private Slider ui_musicVolumeSlider;
    [SerializeField] private Slider ui_soundVolumeSlider;
    
    public override SettingsType Type => SettingsType.Audio;

    public void Initialize(AudioSystem audioSystem)
    {
        ui_masterVolumeSlider.value = audioSystem.MasterVolume;
        ui_masterVolumeSlider.onValueChanged.RemoveAllListeners();
        ui_masterVolumeSlider.onValueChanged.AddListener((value) => audioSystem.SetMasterVolume(value));

        ui_musicVolumeSlider.value = audioSystem.Music.Volume;
        ui_musicVolumeSlider.onValueChanged.RemoveAllListeners();
        ui_musicVolumeSlider.onValueChanged.AddListener((value) => audioSystem.Music.SetVolume(value));

        ui_soundVolumeSlider.value = audioSystem.Sound.Volume;
        ui_soundVolumeSlider.onValueChanged.RemoveAllListeners();
        ui_soundVolumeSlider.onValueChanged.AddListener((value) => audioSystem.Sound.SetVolume(value));
    }
}