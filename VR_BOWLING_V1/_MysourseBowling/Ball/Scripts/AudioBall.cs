using System.Collections;
using UnityEngine;

public class AudioBall : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSource _audioSourceOneShot;
    [SerializeField] private AudioSource _audioSourceLoop;
    [SerializeField] private AudioClip _clipHittingAnySurface;
    [SerializeField] private AudioClip _clipHittingTrack;
    [SerializeField] private AudioClip _clipRolling;
    [SerializeField] private AudioClip _clipHittingPin;

    private float _volumeClips;
    private bool _isFloor = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Pin>(out _))
        {
            _audioSourceOneShot.PlayOneShot(_clipHittingPin, _volumeClips);
        }

        if (collision.gameObject.TryGetComponent<floor>(out _))
        {
            _isFloor = true;
            _audioSourceOneShot.PlayOneShot(_clipHittingTrack, _volumeClips);
            _audioSourceLoop.Play();
        }
        else
        {
            _audioSourceOneShot.PlayOneShot(_clipHittingAnySurface, _volumeClips);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<floor>(out _))
        {
            _isFloor = false;
            _audioSourceLoop.Stop();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(StartRollingSound());
    }

    private IEnumerator StartRollingSound()
    {
        while (true)
        {
            _volumeClips = Mathf.Clamp01(_rigidbody.velocity.magnitude);

            if (_isFloor == true)
            {
                if (_audioSourceLoop.isPlaying == false)
                {
                    _audioSourceLoop.Play();
                }

                _audioSourceLoop.volume = _volumeClips;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
