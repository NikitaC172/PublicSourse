using System;
using System.Collections;
using UnityEngine;

namespace CraneGame
{
    public class HandPointArm : MonoBehaviour
    {
        [SerializeField] private HandInArmMark _armMark;
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private float _timeMove = 0.5f;
        [SerializeField] private const string Arm = "Arm";
        [SerializeField] private const string DefaultHand = "Default";

        private bool _isDeactivate = false;
        private IEnumerator _enumerator;

        public Action IsDeactivated;

        public void ActivateMoveToArm(GameObject gameObject)
        {
            if (_enumerator != null)
            {
                StopCoroutine(_enumerator);
            }

            SetTargetArm();
            _enumerator = MoveToTarget(gameObject, transform.gameObject);
            StartCoroutine(_enumerator);
        }

        public void ActivateMoveToHand(GameObject gameObject)
        {
            if (_enumerator != null)
            {
                StopCoroutine(_enumerator);
            }

            SetTargetHand();
            _enumerator = MoveToTarget(transform.gameObject, gameObject);
            StartCoroutine(_enumerator);
        }

        private void SetTargetArm()
        {
            _isDeactivate = false;
            _armMark.gameObject.transform.position = gameObject.transform.position;
            _armMark.gameObject.SetActive(true);
            _handAnimator.SetTrigger(Arm);
        }

        private void SetTargetHand()
        {
            _isDeactivate = true;
            _handAnimator.SetTrigger(DefaultHand);
        }

        private IEnumerator MoveToTarget(GameObject gameObjectCurrent, GameObject target)
        {
            float time = 0;

            while (time < _timeMove)
            {
                _armMark.gameObject.transform.position = Vector3.Lerp(gameObjectCurrent.transform.position, target.transform.position, time / _timeMove);
                _armMark.gameObject.transform.rotation = Quaternion.Lerp(gameObjectCurrent.transform.rotation, target.transform.rotation, time / _timeMove);
                time += Time.deltaTime;
                yield return null;
            }

            if(_isDeactivate == true)
            {
                IsDeactivated?.Invoke();
                _armMark.gameObject.SetActive(false);
            }
        }
    }
}
