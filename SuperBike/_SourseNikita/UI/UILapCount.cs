using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UILapCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _position;
    [SerializeField] private DistanceCounter _distanceCounterPlayer;
    [SerializeField] private Finish _finish;

    private void OnValidate()
    {
        _finish = FindObjectOfType<Finish>();
    }

    private void OnEnable()
    {
        _distanceCounterPlayer.ChangedLap += SetLap;
    }

    private void OnDisable()
    {
        _distanceCounterPlayer.ChangedLap -= SetLap;
    }

    private void SetLap(int lap)
    {
        _position.text = $"{lap}/{_finish.MaxLap}";
    }
}
