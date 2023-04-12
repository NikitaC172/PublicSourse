using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private DistanceCounter _distanceCounter;
    [SerializeField] private StartLevelSystem _startLevelSystem;
    [SerializeField] private Finish _finish;
    private float _timeCurrentLap = 0;
    private float _timeBestLap = 0;
    private float _timeTotal = 0;
    private bool _isStart = false;

    private Coroutine _totalTimeCoroutine;

    public float TimeCurrentLap => _timeCurrentLap;
    public float TimeTotal { get => _timeTotal; set => _timeTotal = value; }
    public float TimeBestLap { get => _timeBestLap; set => _timeBestLap = value; }

    public Action<float> ChangedBestLapTime;

    private void OnValidate()
    {
        _startLevelSystem = FindObjectOfType<StartLevelSystem>();
        _finish = FindObjectOfType<Finish>();
    }

    private void OnEnable()
    {
        _distanceCounter.CrossedStart += CrossStartLine;
        _startLevelSystem.RaceStarted += StartCounterTotalTime;
        _distanceCounter.ChangedLap += OnChangedLap;
    }

    private void OnDisable()
    {
        _distanceCounter.CrossedStart -= CrossStartLine;
        _startLevelSystem.RaceStarted -= StartCounterTotalTime;
        _distanceCounter.ChangedLap -= OnChangedLap;
    }

    private void StartCounterTotalTime()
    {
        _totalTimeCoroutine = StartCoroutine(StartCountTotalTime());
    }

    private void CrossStartLine()
    {
        if (_distanceCounter.WrongWay == false)
        {
            if (_isStart == true)
            {
                SetBestLap();
            }
            else
            {
                _isStart = true;
                StartCoroutine(StartCountLapTime());
            }
        }
    }

    private void SetBestLap()
    {
        if (_timeBestLap == 0)
        {
            _timeBestLap = _timeCurrentLap;
            _timeCurrentLap = 0;
            ChangedBestLapTime?.Invoke(_timeBestLap);
        }
        else
        {
            if (_timeBestLap > _timeCurrentLap)
            {
                _timeBestLap = _timeCurrentLap;
                ChangedBestLapTime?.Invoke(_timeBestLap);
                _timeCurrentLap = 0;
            }
            else
            {
                _timeCurrentLap = 0;
            }
        }
    }

    private IEnumerator StartCountTotalTime()
    {
        while (true)
        {
            _timeTotal += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator StartCountLapTime()
    {
        while (_isStart == true)
        {
            _timeCurrentLap += Time.deltaTime;
            yield return null;
        }
    }

    private void OnChangedLap(int lap)
    {
        if (lap > _finish.MaxLap)
            if (_totalTimeCoroutine != null)
                StopCoroutine(_totalTimeCoroutine);
    }
}
