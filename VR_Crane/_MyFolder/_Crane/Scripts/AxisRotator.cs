using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class AxisRotator : MonoBehaviour
    {
        [SerializeField] private Arm _armForRotateAxis;
        [SerializeField] private HingeJoint _hingeJoint;
        [SerializeField] private float _motionMultiplier = 1;
        [SerializeField] private float _minValue = -12f;
        [SerializeField] private float _maxValue = 12f;

        private float _positionAxis = 0;
        private float _positionArm = 0;
        private bool _isActive = false;

        private void OnEnable()
        {
            _armForRotateAxis.Activated += TurnOn;
            _armForRotateAxis.Deactivated += TurnOff;
        }

        private void OnDisable()
        {
            _armForRotateAxis.Activated -= TurnOn;
            _armForRotateAxis.Deactivated -= TurnOff;
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
            //if(_isActive == true)
            {
                _positionArm = _armForRotateAxis.gameObject.transform.localRotation.z;
                //Debug.Log(_armForRotateAxis.gameObject.transform.localRotation.z);
                _positionAxis += _positionArm * _motionMultiplier;
                _positionAxis = Mathf.Clamp(_positionAxis, _minValue, _maxValue);

                if (_positionAxis < -180)
                {
                    _positionAxis = 180;
                }
                else if(_positionAxis > 180)
                {
                    _positionAxis = -180;
                }

                JointSpring jointSpring = _hingeJoint.spring;
                jointSpring.targetPosition = _positionAxis;
                _hingeJoint.spring = jointSpring;
            }
        }
    }
}
