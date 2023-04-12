using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSetter : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private const string VolumeSound = "Sound";
    private const string VolumeMusic = "Music";

    public void SetVolumeSound(float volume)
    {
        SetVolume(VolumeSound, volume);
    }

    public void SetVolumeMusic(float volume)
    {
        SetVolume(VolumeMusic, volume);
    }

    private void SetVolume(string MixerName, float volume)
    {
        float minVolume = -80;
        float maxVolume = 0;
        _audioMixer.SetFloat(MixerName, Mathf.Lerp(minVolume, maxVolume, volume));
    }
}
