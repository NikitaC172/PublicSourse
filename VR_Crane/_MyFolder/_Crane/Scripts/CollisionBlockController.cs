using System;
using UnityEngine;

namespace CraneGame
{
    public class CollisionBlockController : MonoBehaviour
    {
        public Action DetectedConflict;
        public Action ResolvedConflict;
        public Action TensionOver;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<StructureTrigger>(out _) == true)
            {
                DetectedConflict?.Invoke();
                TensionOver?.Invoke();
            }
            else
            {
                //TensionOver?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            ResolvedConflict?.Invoke();
        }
    }
}
