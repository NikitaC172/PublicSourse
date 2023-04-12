using System.Collections;
using UnityEngine;

public class AudioMusicChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _sourceForMusic;
    [SerializeField] private AudioClip _clipWin;
    [SerializeField] private AudioClip _clipFail;
    [SerializeField] private AudioClip _clipPigeonFly;
    [SerializeField] private Player _player;
    [SerializeField] private float _volumeMax = 0.3f;

    private void OnEnable()
    {
        _player.Won += PlayWonClip;
        _player.Dead += PlayFailClip;
    }

    private void OnDisable()
    {
        _player.Won -= PlayWonClip;
        _player.Dead -= PlayFailClip;
    }

    private void PlayWonClip()
    {
        SetClip(_clipWin);
    }

    private void PlayFailClip()
    {
        _sourceForMusic.PlayOneShot(_clipPigeonFly);
        SetClip(_clipFail);
    }

    private void SetClip(AudioClip audioClip)
    {
        _sourceForMusic.loop = false;
        StartCoroutine(ChangeClip(audioClip));
    }

    private IEnumerator ChangeClip(AudioClip audioClip)
    {
        float minVolume = 0.1f;
        float currentVolume = _volumeMax;
        float timeReduceVolumeSecond = 0.5f;
        float timeIncreseVolumeSecond = 0.25f;
        float time = 0;

        while (_sourceForMusic.volume > minVolume)
        {
            time += Time.deltaTime;
            _sourceForMusic.volume = Mathf.Lerp(currentVolume, 0, time / timeReduceVolumeSecond);
            yield return null;
        }

        time = 0;
        _sourceForMusic.clip= audioClip;
        _sourceForMusic.Play();

        while (_sourceForMusic.volume < _volumeMax)
        {
            time += Time.deltaTime;
            _sourceForMusic.volume = Mathf.Lerp(currentVolume, _volumeMax, time / timeIncreseVolumeSecond);
            yield return null;
        }
    }
}
