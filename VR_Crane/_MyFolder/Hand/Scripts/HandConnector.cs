using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CraneGame;
using TMPro;

namespace CraneGame
{
    public class HandConnector : MonoBehaviour
    {
        [SerializeField] private Joint _joint;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private bool _isTaken = false;
        [SerializeField] private bool _isReady = true;

        private Arm _arm;

        public bool IsTaken => _isTaken;
        public Arm Arm => _arm;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Arm>(out Arm arm))
            {
                _arm = arm;

                if (_isReady == true)
                {
                    _isReady = false;
                    _rigidbody = arm.GetRigidbody();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {

            if (other.TryGetComponent<Arm>(out Arm arm))
            {
                _arm = arm;

                if (_isTaken == false)
                {
                    _rigidbody = null;
                    _isReady = true;
                }
            }
        }

        public void SetConnect()
        {
            if (_isReady == false)
            {
                _isTaken = true;
                _arm.Activate();
                _joint.connectedBody = _rigidbody;
            }
        }

        public void DisconnectBody()
        {
            if (_isTaken == true)
            {

                _isReady = true;
                _isTaken = false;
                _joint.connectedBody = null;

                if (_rigidbody.gameObject.TryGetComponent<Arm>(out Arm arm))
                {
                    arm.ResetRigidbody();
                }

                _arm.Deactivator();
                _rigidbody = null;
            }
        }
    }
}
