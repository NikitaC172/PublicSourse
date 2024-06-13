using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class HookMover : MonoBehaviour
    {
        [SerializeField] private Arm _armForMoveAxis;
        [SerializeField] private ConfigurableJoint _configJoint;
        [SerializeField] private CollisionBlockController _collisionBlockController;
        [SerializeField] private AngleCalculator _angleCalculator;
        [SerializeField] private float _emergencyReducePosition = 1.5f;
        [SerializeField] private float _motionMultiplier = 1;
        [SerializeField] private float _minValue = 0f;
        [SerializeField] private float _maxValue = 50f;

        private float _positionAxis = 0;
        private float _positionArm = 0;
        private bool _isActive = false;
        private bool _isDownBlock = false;

        public float PositionAxis  => _positionAxis;
        public float PositionArm => _positionArm;

        private void OnEnable()
        {
            if (_collisionBlockController != null)
            {
                _collisionBlockController.ResolvedConflict += UnblockDownDirection;
                _collisionBlockController.DetectedConflict += BlockDownDirection;
            }

            _angleCalculator.EmergencyDisconected += EmergencyReducePosition;
            _armForMoveAxis.Activated += TurnOn;
            _armForMoveAxis.Deactivated += TurnOff;
        }

        private void OnDisable()
        {
            _angleCalculator.EmergencyDisconected -= EmergencyReducePosition;
            _armForMoveAxis.Activated -= TurnOn;
            _armForMoveAxis.Deactivated -= TurnOff;
        }

        private void UnblockDownDirection()
        {
            _isDownBlock = false;
        }

        private void BlockDownDirection()
        {
            _isDownBlock = true;
        }

        private void TurnOn()
        {
            _isActive = true;
        }

        private void TurnOff()
        {
            _isActive = false;
        }

        private void EmergencyReducePosition()
        {
            _positionAxis -= _emergencyReducePosition;
        }

        private bool CheckBlockMove(float positionArm)
        {
            if (positionArm > 0)
            {
                if (_isDownBlock == true)
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

        private void Move()
        {
            _positionAxis += _positionArm * _motionMultiplier;
            _positionAxis = Mathf.Clamp(_positionAxis, _minValue, _maxValue);
            _configJoint.targetPosition = new Vector3(0, 0, _positionAxis);
        }

        private void FixedUpdate()
        {
            //if(_isActive == true) ////////
            {
                _positionArm = _armForMoveAxis.gameObject.transform.localRotation.z;

                if (CheckBlockMove(_positionArm) == true)
                {
                    Move();
                }
            }
        }
    }
}
