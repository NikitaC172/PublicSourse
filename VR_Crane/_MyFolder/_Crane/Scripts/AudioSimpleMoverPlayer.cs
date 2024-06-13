using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class AudioSimpleMoverPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SimpleMover _startMoverForAudio;
        [SerializeField] private SimpleMover _stopMoverForAudio;
        [SerializeField] private float _soundChangeTime;
        [SerializeField] private float _maxVolume;
        [SerializeField] private float _minVolume;

        private void OnEnable()
        {
            _startMoverForAudio.StartedMove += StartMove;
            _stopMoverForAudio.FinishedMove += StopMove;
        }

        private void OnDisable()
        {
            _startMoverForAudio.StartedMove -= StartMove;
            _stopMoverForAudio.FinishedMove -= StopMove;
        }

        private void StartMove()
        {
            _audioSource.Play();
            StartCoroutine(ChangeVolume(_maxVolume));
        }

        private void StopMove()
        {
            StartCoroutine(ChangeVolume(_minVolume));
        }

        private IEnumerator ChangeVolume(float volume)
        {
            float time = 0;
            float currentVolume = _audioSource.volume;

            while (time < _soundChangeTime)
            {
                time += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(currentVolume, volume, time / _soundChangeTime);
                yield return null;
            }
        }
    }
}
