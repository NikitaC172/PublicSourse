using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CraneGame
{
    public class Cargo : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Cargo _cargoPointPlace;
        [SerializeField] private CargoConnectorTrigger _cargoConnectorTrigger;
        [SerializeField] private Sprite _imageCargo;
        [SerializeField] private string _name;
        [SerializeField] private int _health = 100;
        [SerializeField] private float _cableMaxDistance = 0.15f;

        private float _mass;

        public Action<int> HealthChanged;

        private void OnEnable()
        {
            _mass = _rigidbody.mass;
        }

        public void SetHealth(int health)
        {
            _health = Mathf.Clamp(_health - health, 0, 100);
            HealthChanged?.Invoke(_health);
        }

        public int GetHealth()
        {
            return _health;
        }

        public float GetCableMaxDistance()
        {
            return _cableMaxDistance;
        }

        public string GetCargoName()
        {
            return _name;
        }

        public Sprite GetImageName()
        {
            return _imageCargo;
        }

        public float GetMass()
        {
            return _mass;
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }

        public Cargo GetCargoPointPlace()
        {
            return _cargoPointPlace;
        }

        public CargoConnectorTrigger GetCargoConnectorTrigger()
        {
            return _cargoConnectorTrigger;
        }
    }
}
