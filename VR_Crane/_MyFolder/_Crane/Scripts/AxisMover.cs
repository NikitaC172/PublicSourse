using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class AxisMover : MonoBehaviour
    {
        [SerializeField] private Arm _armForMoveAxis;
        [SerializeField] private ConfigurableJoint _configJoint;
        [SerializeField] private float _motionMultiplier = 1;
        [SerializeField] private float _minValue = -12f;
        [SerializeField] private float _maxValue = 12f;

        private float _positionAxis = 0;
        private float _positionArm = 0;
        private bool _isActive = false;

        private void OnEnable()
        {
            _armForMoveAxis.Activated += TurnOn;
            _armForMoveAxis.Deactivated += TurnOff;
        }

        private void OnDisable()
        {
            _armForMoveAxis.Activated -= TurnOn;
            _armForMoveAxis.Deactivated -= TurnOff;
        }

        private void TurnOn()
        {
            _isActive = true;
        }

        private void TurnOff()
        {
            _isActive = false;
        }

        private void FixedUpdate()
        {
            //if(_isActive == true) ////////
            {
                _positionArm = _armForMoveAxis.gameObject.transform.localRotation.z;
                //Debug.Log(_armForRotateAxis.gameObject.transform.localRotation.z);
                _positionAxis += _positionArm * _motionMultiplier;
                _positionAxis = Mathf.Clamp(_positionAxis, _minValue, _maxValue);

                if (_positionAxis < -180)
                {
                    _positionAxis = 180;
                }
                else if (_positionAxis > 180)
                {
                    _positionAxis = -180;
                }

                _configJoint.targetPosition = new Vector3(_positionAxis, 0, 0);
                //JointSpring jointSpring = _configJoint.;
                //jointSpring.targetPosition = _positionAxis;
                //_configJoint.spring = jointSpring;
            }
        }
    }
}
