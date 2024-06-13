using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class SimpleMover : MonoBehaviour
    {
        [SerializeField] private float _zAxisRelatively;
        [SerializeField] private float _yAxisRelatively;
        [SerializeField] private float _xAxisRelatively;
        [SerializeField] private float _timeMoveSeconds;
        [SerializeField] private SimpleMover _nextMover;

        public Action StartedMove;
        public Action FinishedMove;

        private void OnEnable()
        {
            StartCoroutine(MoveObject());
        }

        private IEnumerator MoveObject()
        {
            StartedMove?.Invoke();
            Vector3 currentPosition = transform.position;
            Vector3 deltaPosition;
            float time = 0f;
            float deltaTime = 0.01f;

            while (time < _timeMoveSeconds)
            {
                time += Time.deltaTime;
                deltaTime = time / _timeMoveSeconds;
                deltaPosition = currentPosition + new Vector3(_xAxisRelatively * deltaTime, _yAxisRelatively * deltaTime, _zAxisRelatively * deltaTime);
                transform.position = deltaPosition;
                yield return null;
            }

            transform.position = currentPosition + new Vector3(_xAxisRelatively, _yAxisRelatively, _zAxisRelatively);

            if(_nextMover != null)
            {
                _nextMover.enabled = true;
            }

            FinishedMove?.Invoke();
            yield return null;
        }

    }
}
