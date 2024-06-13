using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class PointUnload : MonoBehaviour
    {
        [SerializeField] private PointUnloadPlace _place;
        [SerializeField] private PointUnloadRotate _rotate;
        [SerializeField] private PointUnloadUp _upPoint;
        Rigidbody _rigidbodyCargo;

        private bool _isReady = false;

        public bool IsReady => _isReady;

        public void TakeRigigdBody(Rigidbody rigidbody)
        {
            _rigidbodyCargo = rigidbody;
        }

        public PointUnloadUp GetUnloadUp()
        {
            return _upPoint;
        }

        public PointUnloadRotate GetUnloadRotate()
        {
            return _rotate;
        }

        public PointUnloadPlace GetUnloadPlace()
        {
            return _place;
        }

        private void OnEnable()
        {
            _place.Placed += SetReady;
            _place.UnPlaced += UnSetReady;
        }

        private void OnDisable()
        {
            _place.Placed -= SetReady;
            _place.UnPlaced -= UnSetReady;
        }

        private void SetReady(Rigidbody rigidbody)
        {
            if (rigidbody == _rigidbodyCargo)
            {
                _isReady = true;
            }
        }

        private void UnSetReady(Rigidbody rigidbody)
        {
            if (rigidbody == _rigidbodyCargo)
            {
                _isReady = false;
            }
        }
    }
}
