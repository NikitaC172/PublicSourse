using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _pointStart;
    [SerializeField] private GameObject _playerStickMan;
    [SerializeField] private GameObject _ragDollPlayer;
    [SerializeField] private Collider _playerCapsule;
    [SerializeField] private Scene _restartScene;

    private bool _isComplite = false;

    public event UnityAction Dead;
    public event UnityAction Won;

    public bool IsComplite => _isComplite;

    private void OnEnable()
    {
        _restartScene.Restarted += Restart;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<RescueZone>(out RescueZone rescueZone))
        {
            _isComplite = true;
            Won?.Invoke();
        }
    }

    private void OnDisable()
    {
        _restartScene.Restarted -= Restart;
    }

    public void Die()
    {
        Dead?.Invoke();
        _playerCapsule.enabled = false;
        _playerStickMan.SetActive(false);
        _ragDollPlayer.SetActive(true);
    }

    private void Restart()
    {
        _ragDollPlayer.SetActive(false);
        gameObject.transform.position = _pointStart.position;
        _playerStickMan.SetActive(true);
    }
}
