using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GamePause _gamePause;

    private void OnValidate()
    {
        _gamePause = FindObjectOfType<GamePause>();
    }

    private IEnumerator Start()
    {
        if (_clips.Count == 1)
        {
            _audioSource.clip = _clips[0];
            _audioSource.Play();
            _audioSource.loop = true;
        }
        else
        {
            AudioClip currentClip = null;
            AudioClip clip = null;

            while (true)
            {
                while (currentClip == clip)
                {
                    clip = ChangerMusic();
                }

                yield return new WaitWhile((() => _audioSource.isPlaying == true));
                _audioSource.clip = clip;
                currentClip = clip;
                _audioSource.Play();
                float timeClip = _audioSource.clip.length;
                yield return new WaitForSeconds(timeClip);
            }
        }

        yield return null;
    }

    private void OnEnable()
    {
        if (_gamePause != null)
        {
            _gamePause.Paused += SetPause;
            _gamePause.Continued += SetContinue;
        }
    }

    private void OnDisable()
    {
        if (_gamePause != null)
        {
            _gamePause.Paused -= SetPause;
            _gamePause.Continued -= SetContinue;
        }
    }

    private void SetPause()
    {
        Debug.Log("Pause");
        _audioSource.Pause();
    }

    private void SetContinue()
    {
        Debug.Log("Play");
        _audioSource.Play();
    }

    private AudioClip ChangerMusic()
    {
        if (_clips.Count > 1)
        {
            return _clips[Random.Range(0, _clips.Count)];
        }
        else
        {
            return _clips[0];
        }
    }
}
