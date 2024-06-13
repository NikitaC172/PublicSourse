using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class Arm : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private HandPointArm _handPointArmLeft;
        [SerializeField] private HandPointArm _handPointArmRight;

        public Action Activated;
        public Action Deactivated;

        public Rigidbody GetRigidbody()
        {            
            return _rigidbody;
        }

        public HandPointArm GetHandPointArm(bool isleft) 
        {
            if (isleft == true)
            {
                return _handPointArmLeft;
            }
            else
            {
                return _handPointArmRight;
            }
        }

        public void Activate()
        {
            Activated?.Invoke();
        }

        public void Deactivator()
        {
            Deactivated?.Invoke();
        }

        public void ResetRigidbody()
        {
            if(gameObject.TryGetComponent<HingeJoint>(out HingeJoint hingeJoint))
            {
                JointSpring jointSpring = hingeJoint.spring;
                //jointSpring.spring = 100;
                //jointSpring.damper = 50;
                jointSpring.targetPosition = 0;
                hingeJoint.spring = jointSpring;
            }
        }
    }
}
