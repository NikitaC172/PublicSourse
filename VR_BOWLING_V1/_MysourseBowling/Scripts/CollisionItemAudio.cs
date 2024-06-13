using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollisionItemAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioCollisonWithOtherItem;
    [SerializeField] private Rigidbody _rigidbody;

    private List<GameObject> _objectsCollisons = new List<GameObject>();
    private float _volume = 0.0f;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitWhile(() => _rigidbody.velocity.magnitude < 0.5f);
            _volume = Mathf.Clamp01(_rigidbody.velocity.magnitude);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_objectsCollisons.Contains(collision.gameObject) == false)
        {
            _objectsCollisons.Add(collision.gameObject);
            _audioSource.PlayOneShot(_audioCollisonWithOtherItem, _volume);            
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
