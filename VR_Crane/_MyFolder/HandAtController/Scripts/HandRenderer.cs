using CraneGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{

    public class HandRenderer : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _rendererHand;
        [SerializeField] private HandConnector _handConnector;
        [SerializeField] private bool _isLeft = false;

        private HandPointArm _handPointArm;
        private bool _isActive = false;

        public void Activate()
        {
            if (TakeCheck() == true && _isActive == false)
            {
                _isActive = true;
                TakeArmHand();
                _handPointArm.IsDeactivated += ShowHand;
                _handPointArm.ActivateMoveToArm(gameObject);
                HideHand();
            }
        }

        public void Deactivate()
        {
            if(_isActive == true)
            {
                _isActive = false;
                _handPointArm.ActivateMoveToHand(gameObject);
            }
        }

        public bool TakeCheck()
        {
            return _handConnector.IsTaken;
        }

        public void TakeArmHand()
        {
            _handPointArm = _handConnector.Arm.GetHandPointArm(_isLeft);
        }

        private void HideHand()
        {
            _rendererHand.enabled = false;
        }

        private void ShowHand()
        {
            _rendererHand.enabled = true;
        }
    }
}
