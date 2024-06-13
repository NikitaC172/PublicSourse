using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class SimpleMassTorqueCalculator : MonoBehaviour
    {
        [SerializeField] private Rigidbody _centreMass;
        [SerializeField] private CargoJoint _cargoJoint;
        [SerializeField] private List<SimpleAxisMover> _movers;
        [SerializeField] private SimpleAxisRotator _rotator;
        [SerializeField] private HookMover _hookMover;
        [SerializeField] private CollisionBlockController _collisionBlockController;
        [SerializeField] private AngleCalculator _angleCalculator;
        [SerializeField] private float _massDeltaLenght = 1.0f;
        [SerializeField] private float _forceMax = 100f;

        private Rigidbody _cargo;
        private float _force;
        private float _lenghtJoint;
        private float _defaultMass = 1.0f;
        private float _startAngle = 40.0f;
        private float _counterMass = 14.5f;
        private float _currentMass;
        private float _cargoMass;
        private float _cableDistance;

        private float _boomMinRadius = 1;
        private float _boomRadius;

        private float _maxMassForCoefficient = 70;
        private float _massCoefficient;

        public float MassCoefficient => _massCoefficient;
        public float Force => _force;

        private void OnEnable()
        {
            _currentMass = _defaultMass;
            _cargoJoint.ConnectChanged += ChangeRigidbody;
            _collisionBlockController.DetectedConflict += ResetMass;
            _collisionBlockController.TensionOver += ResetMass;
            _angleCalculator.GameOver += StopGame;
        }

        private void FixedUpdate()
        {
            ChangeMass();
            _centreMass.AddForce(CalculateDirectionForce() * CalculateForce());
            CalculateMassCoefficient();
            /*if (_cargo != null) //
            {
                _currentMass = _cargo.mass;
            }*/
        }

        private void OnDisable()
        {
            _cargoJoint.ConnectChanged -= ChangeRigidbody;
            _collisionBlockController.DetectedConflict -= ResetMass;
            _collisionBlockController.TensionOver -= ResetMass;
            _angleCalculator.GameOver -= StopGame;
        }

        public void ManualChangeMass(float mass)
        {
            _cargoMass = mass;
        }

        private void CalculateMassCoefficient()
        {
            _massCoefficient = _currentMass / _maxMassForCoefficient;
        }

        private void ChangeRigidbody(Rigidbody rigidbody)
        {
            _cargo = rigidbody;

            if (_cargo != null)
            {
                _lenghtJoint = _hookMover.transform.position.y;//_hookMover.PositionAxis;//
                _cargoMass = _cargo.mass;
                _cableDistance = _cargo.GetComponent<Cargo>().GetCableMaxDistance();
            }
            else
            {
                _currentMass = _defaultMass;
            }
        }

        private void StopGame()
        {
            _centreMass.isKinematic = true;
        }

        private void ResetMass()
        {
            _lenghtJoint = _hookMover.transform.position.y;//_hookMover.PositionAxis;//
            _currentMass = _defaultMass;
        }

        private void ChangeMass()
        {
            if (_currentMass != _cargoMass && _cargo != null)
            {
                //_currentMass = Mathf.Lerp(_defaultMass, _cargoMass, (_lenghtJoint - _hookMover.PositionAxis) / _massDeltaLenght); origin
                _currentMass = Mathf.Lerp(_defaultMass, _cargoMass, ((_hookMover.transform.position.y - _cableDistance) - _lenghtJoint) / _massDeltaLenght);
            }
        }

        private float CalculateForce()
        {
            _force = (float)((_currentMass - _counterMass) * BoomRadiusCalculate() * Math.Tan((Math.PI / 180) * (_startAngle - _rotator.PositionAxis)));
            _force = Mathf.Clamp(_force, 0f, _forceMax);
            return _force;
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

        private Vector3 CalculateDirectionForce()
        {
            Vector3 direction = _cargoJoint.transform.position - transform.position;
            return new Vector3(direction.x, 0, direction.z).normalized;
        }
    }
}
