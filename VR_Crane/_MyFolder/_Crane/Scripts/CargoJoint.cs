using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CraneGame
{
    public class CargoJoint : MonoBehaviour
    {
        private XRIDefaultInputActions _inputActionsKeyboard;//

        [SerializeField] private GameObject _objectHookConnectorToCargo;
        [SerializeField] private SpringJoint _joint;
        [SerializeField] private List<RopeScaler> _ropeScalerList;
        [SerializeField] private CargoRotator _cargoRotator;

        private Rigidbody _cargo;
        private List<Transform> _ropeConnectList;
        private float _maxDistanceCable;
        private bool _IsConnectedTest = false;
        private bool _isEmergencyReady = true;

        public Action<Rigidbody> ConnectChanged;

        public Rigidbody Cargo => _cargo;

        private void Awake()
        {
            _inputActionsKeyboard = new XRIDefaultInputActions();//
            _inputActionsKeyboard.Keybord.ConnectCargo.performed += ConnectCargoButton;//

        }

        private void OnEnable()
        {
            _inputActionsKeyboard.Enable();
        }

        private void OnDisable()
        {
            _inputActionsKeyboard.Disable();
        }

        public void ConnectCargoHandPose()
        {
            ConnectCargo();
        }

        public void EmergencyDisconnectCargo() 
        { 
            if(_isEmergencyReady == true)
            {
                _isEmergencyReady = false;
                ConnectCargo();
            }
        }

        private void ConnectCargoButton(InputAction.CallbackContext context)
        {
            ConnectCargo();
        }

        private void ConnectCargo()
        {
            if (_cargo != null && _IsConnectedTest == false)
            {
                SetPositionRopes();
                _isEmergencyReady = true;
                _joint.connectedBody = _cargo;
                _joint.maxDistance = _maxDistanceCable;
                _objectHookConnectorToCargo.SetActive(true);
                _IsConnectedTest = true; //?
                _cargoRotator.Activate(_cargo);                
            }
            else if (_IsConnectedTest == true)
            {
                _cargoRotator.Deactivate();
                _objectHookConnectorToCargo.SetActive(false);
                _joint.connectedBody = null;
                _cargo = null;
                _IsConnectedTest = false; //?   
            }

            ConnectChanged?.Invoke(_cargo);
        }

        private void SetPositionRopes()
        {
            if (_ropeConnectList != null)
            {
                for (int i = 0; i < _ropeConnectList.Count; i++)
                {
                    _ropeScalerList[i].TakeTarget(_ropeConnectList[i]);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CargoConnectorTrigger>(out CargoConnectorTrigger cargoConnectorTrigger))
            {
                _cargo = cargoConnectorTrigger.GetRigidbody();
                _maxDistanceCable = cargoConnectorTrigger.GetCargo().GetCableMaxDistance();
                _ropeConnectList = cargoConnectorTrigger.GetRopeConnectPoint().ToList();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CargoConnectorTrigger>(out CargoConnectorTrigger cargoConnectorTrigger))
            {
                _cargo = null;
            }
        }
    }
}
