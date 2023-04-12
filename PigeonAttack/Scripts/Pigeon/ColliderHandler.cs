using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderHandler : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _pigeonModel;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private int _maxNumberOfCollisions = 0;

    private int _currentNumberOfCollisions;

    public event UnityAction Dead;
    public event UnityAction Caught;
    public event UnityAction Restarted;
    public event UnityAction Moved;

    private void Start()
    {
        _collider.enabled = true;
    }

    private void OnEnable()
    {
        _currentNumberOfCollisions = _maxNumberOfCollisions;
        _collider.isTrigger = true;
        _collider.enabled = true;
        Moved?.Invoke();
        _pigeonModel.SetActive(true);
    }

    private void OnDisable()
    {
        _collider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pigeon>(out Pigeon pigeon))
        {
            if (_currentNumberOfCollisions == 0)
            {
                Dead?.Invoke();
                _particleSystem.Play();
                _pigeonModel.SetActive(false);
                _collider.enabled = false;
                Restarted?.Invoke();
            }
            else
            {
                _currentNumberOfCollisions--;
            }
        }

        if (other.TryGetComponent<Player>(out Player player))
        {
            {
                Caught?.Invoke();
                player.Die();
                _collider.isTrigger = false;
            }
        }
    }
}
