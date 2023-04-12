using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class GamePause : MonoBehaviour
{
    private const string MasterVolume = "MasterVolume";

    [SerializeField] private AudioMixerGroup _audioMixer;

    private float _delayPause = 0.05f;
    private float _targetTimeScale;

    public event Action Paused;
    public event Action Continued;

    /*private void OnApplicationFocus(bool focus)
    {
        if (focus)
            ContinueGame();
        else
            PauseGame();
    }*/

    public void PauseGame()
    {
        _targetTimeScale = 0;
        Paused?.Invoke();
        StopCoroutine(SetPause(_targetTimeScale));
        StartCoroutine(SetPause(_targetTimeScale));
        //SetPause(_targetTimeScale);

    }

    public void ContinueGame()
    {
        _targetTimeScale = 1;
        Continued?.Invoke();
        StopCoroutine(SetPause(_targetTimeScale));
        StartCoroutine(SetPause(_targetTimeScale));
    }

    /*public void ContinueGame(bool isClose)
    {
        _targetTimeScale = 1;
        SetPause(_targetTimeScale);
    }*/

    private IEnumerator SetPause(float timeScale)
    {
        float minVolume = -80;
        float maxVolume = 0;

        if (timeScale == 0)
        {
            _audioMixer.audioMixer.SetFloat(MasterVolume, minVolume);
            yield return new WaitForSeconds(_delayPause);
        }
        else
        {
            _audioMixer.audioMixer.SetFloat(MasterVolume, maxVolume);
            yield return null;
        }

        Time.timeScale = timeScale;
    }

    /*private void SetPause(float timeScale)
    {
        float minVolume = -80;
        float maxVolume = 0;

        if (timeScale == 0)
        {
            _audioMixer.audioMixer.SetFloat(MasterVolume, minVolume);
        }
        else
        {
            _audioMixer.audioMixer.SetFloat(MasterVolume, maxVolume);
        }

        Invoke(nameof(SetTimeScale), _delayPause);
        //Time.timeScale = timeScale;
    }

    private void SetTimeScale()
    {
        Time.timeScale = _targetTimeScale;
    }*/
}