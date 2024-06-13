using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PinAudio : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clipPinCollision;
    [SerializeField] private AudioClip _clipItemCollision;

    private List<GameObject> _objectsCollisons = new List<GameObject>();
    private float _volume = 0.5f;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitWhile(() => _rigidbody.velocity.magnitude<0.2f);
            _audioSource.volume = Mathf.Clamp01(_rigidbody.velocity.magnitude);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_objectsCollisons.Contains(collision.gameObject) == false)
        {
            _objectsCollisons.Add(collision.gameObject);

            if (collision.gameObject.TryGetComponent<Pin>(out _))
            {
                _audioSource.PlayOneShot(_clipPinCollision, _volume);
            }
            else if (collision.gameObject.TryGetComponent<Ball>(out _))
            {
                _audioSource.PlayOneShot(_clipPinCollision, _volume);
            }
            else
            {
                _audioSource.PlayOneShot(_clipItemCollision, _volume);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_objectsCollisons.Contains(collision.gameObject) == true)
        {
            _objectsCollisons.Remove(collision.gameObject);
        }
    }
}
