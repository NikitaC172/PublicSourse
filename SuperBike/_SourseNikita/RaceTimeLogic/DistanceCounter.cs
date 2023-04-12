using BansheeGz.BGSpline.Components;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField] private BGCcMath _math;
    [SerializeField] private StartLevelSystem _startLevelSystem;

    private int _lap = 1;
    private float _distance = 0;
    private float _distanceLap = 0;
    private float _totalDistance = 0;
    private bool _isStart;
    private int _startLineCrossCount = 0;
    private int _checkLineCrossCount = 0;
    private bool _wrongWay = false;
    private bool _isNextLap = false;

    public float TotalDistance => _totalDistance;
    public bool WrongWay => _wrongWay;
    public int Lap => _lap;
    public bool IsStart => _isStart;

    public Action<int> ChangedLap;
    public Action CrossedStart;

    private void OnValidate()
    {
        _math = FindObjectOfType<BGCcMath>();
        _startLevelSystem = FindObjectOfType<StartLevelSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<StartLine>(out StartLine startLine))
        {
            if (_startLineCrossCount == _checkLineCrossCount)
            {
                _startLineCrossCount++;
                CrossedStart?.Invoke();
                _wrongWay = false;
            }
            else
            {
                _startLineCrossCount--;
            }

            if (_isStart == true)
            {
                SetLap();
            }
            else
            {
                _isStart = true;
                SetLapDistance();
                ChangedLap?.Invoke(_lap);
                StartCoroutine(StartTracking());
            }
        }

        if (other.gameObject.TryGetComponent<CheckLine>(out CheckLine checkLine))
        {
            if (_checkLineCrossCount == _startLineCrossCount - 1)
            {
                _checkLineCrossCount++;
            }
            else if (_checkLineCrossCount == _startLineCrossCount)
            {
                _checkLineCrossCount--;
                _wrongWay = true;
            }
        }
    }

    private void SetLap()
    {
        if (_wrongWay == false)
        {
            _isNextLap = true;
            _lap++;
            ChangedLap?.Invoke(_lap);
        }
        else
        {
            _lap--;
        }
    }

    private void SetLapDistance()
    {
        _distanceLap = _math.GetDistance();
    }

    private IEnumerator StartTracking()
    {
        float delayForStart = 1.0f;
        float delayForNextLap = 0.5f;

        yield return new WaitForSecondsRealtime(delayForStart);

        _isStart = true;

        while (_isStart == true)
        {
            var position = _math.CalcPositionByClosestPoint(transform.position, out _distance);
            _totalDistance = _distance + _lap * _distanceLap;

            if(_isNextLap == true)
            {
                _distance = 0;
                _totalDistance = _distance + _lap * _distanceLap;
                yield return new WaitForSecondsRealtime(delayForNextLap);
                _isNextLap = false;
            }

            if (_wrongWay == true)
            {
                yield return new WaitWhile(() => _wrongWay == true);
            }

            yield return null;
        }
    }
}
