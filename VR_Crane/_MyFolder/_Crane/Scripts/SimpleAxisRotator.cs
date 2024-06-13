using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class SimpleAxisRotator : MonoBehaviour
    {
        [SerializeField] private Arm _armForRotateAxis;
        [SerializeField] private TriggerStructureController _triggerStructureControllerPositiveDirection;
        [SerializeField] private TriggerStructureController _triggerStructureControllerNegativeDirection;
        [SerializeField] private CollisionBlockController _collisionBlockController;
        [SerializeField] private SaggingCorrector _saggingCorrector;
        [SerializeField] private float _motionMultiplier = 1;
        [SerializeField] private float _minValue = -12f;
        [SerializeField] private float _maxValue = 12f;
        [SerializeField] private Vector3 _axisController;
        [SerializeField] private float _direction = 1;

        private Vector3 _startAxisPosition;
        private Vector3 _axisPosition;
        private Vector3 _axisDeltaPosition;
        //private float _time;
        //private float _position = 0;
        [SerializeField] private float _positionAxis = 0;
        private float _positionArm = 0;
        private bool _isActive = false;
        private bool _isBlockPositiveDirection = false;
        private bool _isBlockNegativeDirection = false;

        public float PositionAxis => _positionAxis;
        public float PositionArm => _positionArm;

        private void OnEnable()
        {
            _startAxisPosition = transform.localRotation.eulerAngles;
            _armForRotateAxis.Activated += TurnOn;
            _armForRotateAxis.Deactivated += TurnOff;

            if (_triggerStructureControllerPositiveDirection != null)
            {
                _triggerStructureControllerPositiveDirection.ResolvedConflict += UnblockPositiveDirection;
                _triggerStructureControllerPositiveDirection.DetectedConflict += BlockPositiveDirection;
            }

            if (_triggerStructureControllerNegativeDirection != null)
            {
                _triggerStructureControllerNegativeDirection.ResolvedConflict += UnblockNegativeDirection;
                _triggerStructureControllerNegativeDirection.DetectedConflict += BlockNegativeDirection;
            }

            if(_collisionBlockController != null)
            {
                _collisionBlockController.DetectedConflict += BlockNegativeDirection;
                _collisionBlockController.ResolvedConflict += UnblockNegativeDirection;
            }
        }

        private void OnDisable()
        {
            _armForRotateAxis.Activated -= TurnOn;
            _armForRotateAxis.Deactivated -= TurnOff;

            if (_triggerStructureControllerPositiveDirection != null)
            {
                _triggerStructureControllerPositiveDirection.ResolvedConflict -= UnblockPositiveDirection;
                _triggerStructureControllerPositiveDirection.DetectedConflict -= BlockPositiveDirection;
            }

            if (_triggerStructureControllerNegativeDirection != null)
            {
                _triggerStructureControllerNegativeDirection.ResolvedConflict -= UnblockNegativeDirection;
                _triggerStructureControllerNegativeDirection.DetectedConflict -= BlockNegativeDirection;
            }
        }

        private void BlockPositiveDirection()
        {
            _isBlockPositiveDirection = true;
        }

        private void BlockNegativeDirection()
        {
            _isBlockNegativeDirection = true;
        }

        private void UnblockPositiveDirection()
        {
            _isBlockPositiveDirection = false;
        }

        private void UnblockNegativeDirection()
        {
            _isBlockNegativeDirection = false;
        }

        private bool CheckBlockMove(float positionArm)
        {
            if (positionArm > 0)
            {
                if (_isBlockPositiveDirection == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (positionArm < 0)
            {
                if (_isBlockNegativeDirection == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void TurnOn()
        {
            _isActive = true;
        }

        private void TurnOff()
        {
            _isActive = false;
        }

        private void Move()
        {
            _positionAxis += Time.deltaTime * _positionArm * _motionMultiplier;
            _positionAxis = Mathf.Clamp(_positionAxis, _minValue, _maxValue);
            _axisDeltaPosition = new Vector3(_positionAxis * _axisController.x, _positionAxis * _axisController.y, _positionAxis * _axisController.z);

            if (_saggingCorrector != null)
            {
                _axisPosition = (_startAxisPosition + _axisDeltaPosition - new Vector3(_axisController.x * _saggingCorrector.SagAngle, _axisController.y * _saggingCorrector.SagAngle, _axisController.z * _saggingCorrector.SagAngle));
            }
            else
            {
                _axisPosition = (_startAxisPosition + _axisDeltaPosition);
            }

            transform.localRotation = Quaternion.Euler(_axisPosition);
        }

        private void FixedUpdate()
        {
            //if(_isActive == true)
            {
                _positionArm = _direction * _armForRotateAxis.gameObject.transform.localRotation.z;

                if (CheckBlockMove(_positionArm) == true)
                {
                    Move();
                }
            }
        }
    }
}
