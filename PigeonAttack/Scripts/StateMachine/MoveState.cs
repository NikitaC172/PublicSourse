using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Agava.YandexGames;

public class MoveState : State
{
    [SerializeField] private Player _target;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _timeRotate = 1.5f;

    private void OnEnable()
    {
        transform.DOLookAt(_target.transform.position, 0, AxisConstraint.Y, Vector3.up);
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        // Always wait for it if invoking something immediately in the first scene.
        yield return YandexGamesSdk.Initialize();

        if (Device.Type == Agava.YandexGames.DeviceType.Mobile)
        {
            float deceleration = 0.8f;
            _speed *= deceleration;
        }
    }

    private void Update()
    {
        transform.DOLookAt(_target.transform.position, _timeRotate, AxisConstraint.Y, Vector3.up);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void ChangeSpeedForMobilePlatform(float deceleration)
    {
        _speed *= deceleration;
    }
}
