using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace CraneGame
{
    public class CargoConnectorTrigger : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbodyCargo;
        [SerializeField] private Cargo _cargo;
        [SerializeField] private List<Transform> _ropeConnectPoint;

        public Rigidbody GetRigidbody()
        {
            return _rigidbodyCargo;
        }

        public Cargo GetCargo()
        {
            return _cargo;
        }

        public ReadOnlyCollection<Transform> GetRopeConnectPoint()
        {
            return _ropeConnectPoint.AsReadOnly();
        }
    }
}
