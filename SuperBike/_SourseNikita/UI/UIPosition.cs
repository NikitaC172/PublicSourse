using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPosition : MonoBehaviour
{
    [SerializeField] private TMP_Text _position;
    [SerializeField] private PositionChecker _positionChecker;

    private int countRiders;

    private void OnValidate()
    {
        _positionChecker = FindObjectOfType<PositionChecker>();
    }

    private void OnEnable()
    {
        _positionChecker.RidersSetted += SetCountRiders;
        _positionChecker.ChangedPositionPlayer += SetPosition;
    }

    private void OnDisable()
    {
        _positionChecker.ChangedPositionPlayer -= SetPosition;
        _positionChecker.RidersSetted-= SetCountRiders;
    }

    private void SetCountRiders(int count)
    {
        countRiders = count;
    }

    private void SetPosition(int position)
    {
        _position.text = $"{position + 1}/{countRiders}";
    }
}
