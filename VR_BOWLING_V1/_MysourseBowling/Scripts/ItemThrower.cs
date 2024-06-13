using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrower : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _forceMultiplier;
    [SerializeField] private float _maxSpeed = 1.0f;

    private Vector3 _heading;
    private Vector3 _lastPosition;
    private float _speed;
    private float _timeCount;//

    public Vector3 _TestVector;

    private void OnEnable()
    {
        StartCoroutine(SetLastSpeed());
        _lastPosition = transform.position;
    }

    private void Update()
    {
        _timeCount += Time.deltaTime;
        _speed = Vector3.Distance(transform.position, _lastPosition) / _timeCount;
        _heading = (transform.position - _lastPosition).normalized;

        _TestVector = _heading * _maxSpeed * _forceMultiplier; ////////////
    }

    private void OnDisable()
    {
        if (_speed < _maxSpeed)
        {
            _rigidbody.velocity = _heading * _speed * _forceMultiplier;
        }
        else
        {
            _rigidbody.velocity = _heading * _maxSpeed * _forceMultiplier;
        }

        StopCoroutine(SetLastSpeed());
    }

    private IEnumerator SetLastSpeed()
    {
        float delay = 0.1f;
        var WaitSeconds = new WaitForSeconds(delay);

        while (true)
        {
            _timeCount = 0f;
            _lastPosition = transform.position;
            yield return WaitSeconds;
        }
    }
}
