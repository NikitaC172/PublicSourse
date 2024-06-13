using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class TriggerStructureController : MonoBehaviour
    {
        private bool _isBarrierConflict = false;//
        public Action DetectedConflict;
        public Action ResolvedConflict;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StructureTrigger>(out _))
            {
                _isBarrierConflict = true;//
                Debug.Log(_isBarrierConflict);//
                DetectedConflict?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<StructureTrigger>(out _))
            {
                _isBarrierConflict = false;//
                Debug.Log(_isBarrierConflict);//
                ResolvedConflict?.Invoke();
            }
        }
    }
}
