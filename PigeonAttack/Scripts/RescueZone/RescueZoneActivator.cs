using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueZoneActivator : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private ZonePointer _zonePointer;
    [SerializeField] private RescueZone _rescue;

    private void OnEnable()
    {
        _score.LevelCompleted += Activate;
    }

    private void Activate()
    {
        _rescue.gameObject.SetActive(true);
        _zonePointer.gameObject.SetActive(true);
    }
}
