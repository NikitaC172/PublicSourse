using CraneGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UltimateXR.Extensions.Unity.Math;
using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
    [SerializeField] private CollisionBlockController _collisionBlockController;
    [SerializeField] private CargoJoint _cargoJoint;
    [SerializeField] private Vector3 _vector;
    [SerializeField] private float _maxAngleDisconect = 5.0f;
    [SerializeField] private float _maxAngleSlopeGameOver = 30.0f;
    [SerializeField] private float _minAngle = 1.0f;
    private float _angle;
    private bool _isEmergency = false;

    public Action GameOver;
    public Action EmergencyDisconected;

    private void Awake()
    {
        _vector = transform.position;
    }

    private void OnEnable()
    {
        _collisionBlockController.TensionOver += DisconecteCargo;
    }

    private void OnDisable()
    {
        _collisionBlockController.TensionOver -= DisconecteCargo;
    }

    private void DisconecteCargo()
    {
        if(_angle > _maxAngleDisconect && _isEmergency == false)
        {
            _isEmergency = true;
            _cargoJoint.EmergencyDisconnectCargo();
            EmergencyDisconected?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        _angle = Vector3.Angle(transform.position, _vector);
        //Debug.Log(_angle);

        if (_angle > _maxAngleSlopeGameOver)
        {
            GameOver?.Invoke();
            //Debug.LogError("GameOver" + _angle);
        }
        else if(_angle < _minAngle & _isEmergency == true)
        {
            _isEmergency = false;
        }
    }
}
