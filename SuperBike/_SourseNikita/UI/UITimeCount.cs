using System;
using TMPro;
using UnityEngine;

public class UITimeCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _bestLap;
    [SerializeField] private TMP_Text _currentLap;
    [SerializeField] private TimeCounter _timeCounterPlayer;

    private void Update()
    {
        SetTimeCurrentLap(_timeCounterPlayer.TimeCurrentLap);
    }

    private void OnEnable()
    {
        _timeCounterPlayer.ChangedBestLapTime += SetTimeBestLap;
    }

    private void TimeConversion(float time, out float miliseconds, out int seconds, out int minuts)
    {
        int midPoint = 3;
        miliseconds = (float)Math.Round((time % 1), midPoint) * 1000;
        seconds = (int)time;
        minuts = seconds / 60;
        seconds -= minuts * 60;
    }

    private void SetTime(TMP_Text textField, float time)
    {
        int rightPadding = 3;
        int leftPadding = 2;
        float miliseconds;
        int seconds;
        int minuts;
        TimeConversion(time, out miliseconds, out seconds, out minuts);
        textField.text = 
            $"{minuts.ToString().PadLeft(leftPadding, '0')}" +
            $":{seconds.ToString().PadLeft(leftPadding, '0')}" +
            $":{miliseconds.ToString().PadRight(rightPadding, '0')}";
    }

    private void SetTimeCurrentLap(float time)
    {
        SetTime(_currentLap, time);
    }

    private void SetTimeBestLap(float timeBestLap)
    {
        SetTime(_bestLap, timeBestLap);
    }
}
