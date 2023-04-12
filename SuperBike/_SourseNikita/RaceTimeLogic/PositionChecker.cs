using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.Burst.Intrinsics;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Unity.VisualScripting;

public class PositionChecker : MonoBehaviour
{
    [SerializeField] List<DistanceCounter> _distanceCounters = new List<DistanceCounter>();
    [SerializeField] private DistanceCounter _player;

    private int _playerPosition = 0;
    private int _countRacers;
    private bool _isPause = false;
    private List<DistanceCounter> _sortDistance = new List<DistanceCounter>();

    public int PlayerPosition => _playerPosition;
    public IReadOnlyList<DistanceCounter> SortDistance => _sortDistance;

    public Action<IReadOnlyList<DistanceCounter>> UpdatedPositions;
    public Action<int> ChangedPositionPlayer;
    public Action<int> RidersSetted;

    private void OnEnable()
    {
        _player.ChangedLap += SetPause;
    }

    private void Start()
    {
        RidersSetted?.Invoke(_distanceCounters.Count);
        _playerPosition = _distanceCounters.IndexOf(_player);
        StartCoroutine(SetPosition());        
    }

    private void OnDisable()
    {
        _player.ChangedLap -= SetPause;
    }

    private void SetPause(int lap)
    {
        _isPause = true;
    }

    private IEnumerator SetPosition()
    {
        var timeBetweenCheck = new WaitForSecondsRealtime(0.5f);
        var pausetimeCheck = new WaitForSecondsRealtime(1.5f);
        ChangedPositionPlayer?.Invoke(_playerPosition);

        while (true)
        {
            if (_isPause == true)
            {
                yield return pausetimeCheck;
            }

            int positionPlayer;
            _sortDistance = (from DistanceCounter counter in _distanceCounters orderby counter.TotalDistance descending select counter).ToList();
            positionPlayer = _sortDistance.IndexOf(_player);
            UpdatedPositions?.Invoke(_sortDistance.AsReadOnly());

            if (_playerPosition != positionPlayer)
            {
                _playerPosition = positionPlayer;
                ChangedPositionPlayer?.Invoke(_playerPosition);
            }

            yield return timeBetweenCheck;
        }
    }
}
