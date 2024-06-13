using CraneGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class MassTorqueCalculator : MonoBehaviour
    {
        [SerializeField] private Rigidbody _grossMass;
        [SerializeField] private CargoJoint _cargoJoint;
        [SerializeField] private List<SimpleAxisMover> _movers;
        [SerializeField] private SimpleAxisRotator _rotator;
        [SerializeField] private HookMover _hookMover;
        [SerializeField] private float _massDeltaLenght = 1.0f;

        private Rigidbody _cargo;
        private float _lenghtJoint;
        private float _defaultMass = 1.0f;
        private float _startAngle = 40.0f;

        private float _boomMinRadius = 1;
        private float _boomRadius;
        private float _cargoMass;
        private float _currentMass;
        private float _maxMassForCoefficient = 70;
        private float _MassCoefficient;

        public float MassCoefficient  => _MassCoefficient;

        private void OnEnable()
        {
            _cargoMass = _defaultMass;
            _cargoJoint.ConnectChanged += ChangeRigidbody;
        }

        private void OnDisable()
        {
            _cargoJoint.ConnectChanged -= ChangeRigidbody;
        }

        private void FixedUpdate()
        {
            ChangeMass();
            _grossMass.mass = (float)(_currentMass * BoomRadiusCalculate() * Math.Tan((Math.PI / 180) * (_startAngle - _rotator.PositionAxis)));
            CalculateMassCoefficient();
        }

        private void ChangeMass()
        {
            if (_currentMass != _cargoMass)
            {
                _currentMass = Mathf.Lerp(_defaultMass, _cargoMass, MathF.Abs((_lenghtJoint - _hookMover.PositionAxis) / _massDeltaLenght));
            }
        }

        private void CalculateMassCoefficient()
        {
            _MassCoefficient = _currentMass / _maxMassForCoefficient;
        }

        private void ChangeRigidbody(Rigidbody rigidbody)
        {
            _cargo = rigidbody;

            if (_cargo != null)
            {
                _lenghtJoint = _hookMover.PositionAxis;
                _cargoMass = _cargo.mass;
            }
            else
            {
                _currentMass = _defaultMass;
            }
        }

        private float BoomRadiusCalculate()
        {
            _boomRadius = _boomMinRadius;

            foreach (SimpleAxisMover mover in _movers)
            {
                _boomRadius += Math.Abs(mover.PositionAxis) / 100;
            }

            return _boomRadius;
        }
    }
}
