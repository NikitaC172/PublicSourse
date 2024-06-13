using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class CargoHealthCalculator : MonoBehaviour
    {
        [SerializeField] private Cargo _cargo;
        [SerializeField] private float _speedDamage = 3;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.relativeVelocity.magnitude > _speedDamage)
            {
                _cargo.SetHealth((int)collision.relativeVelocity.magnitude);
            }
        }
    }
}
