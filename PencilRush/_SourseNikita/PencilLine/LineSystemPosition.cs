using System;
using UnityEngine;

public class LineSystemPosition : MonoBehaviour
{
    [SerializeField] private float _timeMoveToCentr = 0.5f;
    [SerializeField] private float _timeMoveToAngle = 0.25f;

    private float _drag = 0.0f;
    private float _currentX;
    private float _timeCurrentPath = 1.0f;
    private float _time;
    private float _directionMove = 0.0f;
    private float _currentDrag = 0.0f;
    private bool _isMoveCentr = true;

    private const float DeviationMultiplier = 200;
    private const float DeviationLimits = 10;

    public float Drag => _drag;

    private void Start()
    {
        _currentX = transform.position.x;
    }

    private void Update()
    {
        if (_currentX != transform.position.x)
        {
            _currentDrag = Mathf.Clamp(DeviationMultiplier * (transform.position.x - _currentX), -DeviationLimits, DeviationLimits);
            _isMoveCentr = false;
        }

        if (Math.Abs(_currentDrag) > Math.Abs(_drag) && _isMoveCentr == false)
        {
            _isMoveCentr = true;
            _directionMove = _currentDrag;
            _timeCurrentPath = _timeMoveToAngle;
            _time = 0;
        }
        else
        {
            _time = 0;
            _directionMove = 0;
            _timeCurrentPath = _timeMoveToCentr;
        }

        _currentX = transform.position.x;
        _time += Time.deltaTime;
        _drag = Mathf.LerpUnclamped(_drag, _directionMove, _time / _timeCurrentPath);

        if (_timeCurrentPath == _timeMoveToAngle && _time > _timeMoveToAngle)
        {
            _currentDrag = 0;
        }
    }
}
