using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CraneGame
{
    public class ArmKeyboardController : MonoBehaviour
    {
        [SerializeField] private List<HingeJoint> _arms;
        private XRIDefaultInputActions _inputActions;

        public Action ClickedEnter;

        private void Awake()
        {
            _inputActions = new XRIDefaultInputActions();
            _inputActions.Keybord.Arm1.performed += Arm1_performed;
            _inputActions.Keybord.Arm1.canceled += Arm1_canceled;
            _inputActions.Keybord.Arm2.performed += Arm2_performed;
            _inputActions.Keybord.Arm2.canceled += Arm2_canceled;
            _inputActions.Keybord.Arm3.performed += Arm3_performed;
            _inputActions.Keybord.Arm3.canceled += Arm3_canceled;
            _inputActions.Keybord.Arm4.performed += Arm4_performed;
            _inputActions.Keybord.Arm4.canceled += Arm4_canceled;
            _inputActions.Keybord.Arm5.performed += Arm5_performed;
            _inputActions.Keybord.Arm5.canceled += Arm5_canceled;
            _inputActions.Keybord.Arm6.performed += Arm6_performed;
            _inputActions.Keybord.Arm6.canceled += Arm6_canceled;


            _inputActions.Keybord.ClickEnter.performed += ClickEnter;
        }

        private void ClickEnter(InputAction.CallbackContext obj)
        {
            ClickedEnter?.Invoke();
        }

        private void Arm1_performed(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[0].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 12*value;
            _arms[0].spring = jointSpring;
        }

        private void Arm1_canceled(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[0].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 0;
            _arms[0].spring = jointSpring;
        }

        private void Arm2_performed(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[1].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 12 * value;
            _arms[1].spring = jointSpring;
        }

        private void Arm2_canceled(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[1].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 0;
            _arms[1].spring = jointSpring;
        }

        private void Arm3_performed(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[2].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 12 * value;
            _arms[2].spring = jointSpring;
        }

        private void Arm3_canceled(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[2].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 0;
            _arms[2].spring = jointSpring;
        }

        private void Arm4_performed(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[3].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 12 * value;
            _arms[3].spring = jointSpring;
        }

        private void Arm4_canceled(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[3].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 0;
            _arms[3].spring = jointSpring;
        }

        private void Arm5_performed(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[4].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 12 * value;
            _arms[4].spring = jointSpring;
        }

        private void Arm5_canceled(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[4].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 0;
            _arms[4].spring = jointSpring;
        }

        private void Arm6_performed(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[5].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 12 * value;
            _arms[5].spring = jointSpring;
        }

        private void Arm6_canceled(InputAction.CallbackContext obj)
        {
            JointSpring jointSpring = _arms[5].spring;
            float value = obj.ReadValue<float>();
            jointSpring.targetPosition = 0;
            _arms[5].spring = jointSpring;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
    }
}
