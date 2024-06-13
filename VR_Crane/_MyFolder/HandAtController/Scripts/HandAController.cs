using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CraneGame
{
    public class HandAController : MonoBehaviour
    {
        private XRIDefaultInputActions _inputActions;

        [SerializeField] private UnityEvent GripLeftStarted;
        [SerializeField] private UnityEvent TriggerLeftStarted;
        [SerializeField] private UnityEvent XLeftStarted;

        [SerializeField] private UnityEvent YLeftStarted;

        [SerializeField] private UnityEvent GripLeftCanceled;
        [SerializeField] private UnityEvent TriggerLeftCanceled;
        [SerializeField] private UnityEvent XLeftCanceled;


        [SerializeField] private UnityEvent GripRightStarted;
        [SerializeField] private UnityEvent TriggerRightStarted;
        [SerializeField] private UnityEvent ARightStarted;

        [SerializeField] private UnityEvent GripRightCanceled;
        [SerializeField] private UnityEvent TriggerRightCanceled;
        [SerializeField] private UnityEvent ARightCanceled;

        private string _left = "left";
        private string _right = "right";


        private void Awake()
        {
            _inputActions = new XRIDefaultInputActions();
            _inputActions.XRILeftHandInteraction.Select.started += ctx => ClickGrip(_left, true);
            _inputActions.XRILeftHandInteraction.Select.canceled += ctx => ClickGrip(_left, false);

            _inputActions.XRILeftHandInteraction.Activate.started += ctx => ClickTrigger(_left, true);
            _inputActions.XRILeftHandInteraction.Activate.canceled += ctx => ClickTrigger(_left, false);

            _inputActions.XRILeftHandInteraction.X_Button.started += ctx => ClickPrimaryButton(_left, true);
            _inputActions.XRILeftHandInteraction.X_Button.canceled += ctx => ClickPrimaryButton(_left, false);

            _inputActions.XRILeftHandInteraction.Y_Button.started += ctx => ClickSecondaryButton(_left);

            _inputActions.XRIRightHandInteraction.Select.started += ctx => ClickGrip(_right, true);
            _inputActions.XRIRightHandInteraction.Select.canceled += ctx => ClickGrip(_right, false);

            _inputActions.XRIRightHandInteraction.Activate.started += ctx => ClickTrigger(_right, true);
            _inputActions.XRIRightHandInteraction.Activate.canceled += ctx => ClickTrigger(_right, false);

            _inputActions.XRIRightHandInteraction.A_Button.started += ctx => ClickPrimaryButton(_right, true);
            _inputActions.XRIRightHandInteraction.A_Button.canceled += ctx => ClickPrimaryButton(_right, false);

        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        public void DisavbleControl()
        {
            _inputActions.Disable();
        }

        private void ClickGrip(string hand, bool isActive)
        {
            if (isActive == true)
            {
                if (hand == _left)
                {
                    GripLeftStarted.Invoke();
                }
                else
                {
                    GripRightStarted.Invoke();
                }
            }
            else
            {
                if (hand == _left)
                {
                    GripLeftCanceled.Invoke();
                }
                else
                {
                    GripRightCanceled.Invoke();
                }
            }
        }

        private void ClickTrigger(string hand, bool isActive)
        {
            if (isActive == true)
            {
                if (hand == _left)
                {
                    TriggerLeftStarted.Invoke();
                }
                else
                {
                    TriggerRightStarted.Invoke();
                }
            }
            else
            {
                if (hand == _left)
                {
                    TriggerLeftCanceled.Invoke();
                }
                else
                {
                    TriggerRightCanceled.Invoke();
                }
            }
        }

        private void ClickPrimaryButton(string hand, bool isActive)
        {
            if (isActive == true)
            {
                if (hand == _left)
                {
                    XLeftStarted.Invoke();
                }
                else
                {
                    ARightStarted.Invoke();
                }
            }
            else
            {
                if (hand == _left)
                {
                    XLeftCanceled.Invoke();
                }
                else
                {
                    ARightCanceled.Invoke();
                }
            }
        }

        private void ClickSecondaryButton(string hand)
        {
            YLeftStarted.Invoke();
            Debug.Log("Second Y Pressed");
        }
    }
}
