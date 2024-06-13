using DaVanciInk.AdvancedPlayerPrefs;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioPanel : MonoBehaviour
{
    [SerializeField] private Slider _sliderSound;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderAssistent;
    [SerializeField] private AudioMixerGroup _audioMixer;

    private const string _sound = "Sound";
    private const string _music = "Music";
    private const string _assistent = "Assistent";

    private void OnEnable()
    {
        SetValueFromSave();
        _sliderSound.onValueChanged.AddListener(delegate { ChangeVolume(_sound, _sliderSound.value); });
        _sliderMusic.onValueChanged.AddListener(delegate { ChangeVolume(_music, _sliderMusic.value); });
        _sliderAssistent.onValueChanged.AddListener(delegate { ChangeVolume(_assistent, _sliderAssistent.value); });
    }

    private void OnDisable()
    {
        _sliderSound.onValueChanged.RemoveListener(delegate { ChangeVolume(_sound, _sliderSound.value); });
        _sliderMusic.onValueChanged.RemoveListener(delegate { ChangeVolume(_music, _sliderMusic.value); });
        _sliderAssistent.onValueChanged.RemoveListener(delegate { ChangeVolume(_assistent, _sliderAssistent.value); });
    }

    private void ChangeVolume(string Mixer, float number)
    {
        _audioMixer.audioMixer.SetFloat(Mixer, number);
        SaveValue(Mixer, number);
    }

    private void SetValueFromSave()
    {
        Debug.Log("SetSave");
        float number = AdvancedPlayerPrefs.GetFloat("ValueSound");
        Debug.Log(number);
        _sliderSound.value = AdvancedPlayerPrefs.GetFloat("ValueSound");
        _sliderMusic.value = AdvancedPlayerPrefs.GetFloat("ValueMusic"); // save
        _sliderAssistent.value = AdvancedPlayerPrefs.GetFloat("ValueAssistent"); // save
    }

    private void SaveValue(string MixerAudio, float value)
    {
        string saveString = "Value" + MixerAudio;
        AdvancedPlayerPrefs.SetFloat(saveString, value);
    }
}
