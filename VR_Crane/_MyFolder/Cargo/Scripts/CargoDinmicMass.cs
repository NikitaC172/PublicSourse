using System.Collections;
using UnityEngine;

namespace CraneGame
{
    public class CargoDinmicMass : MonoBehaviour
    {
        [SerializeField] private float _yCoordinateForWater;
        [SerializeField] private float _yCoordinateForMassChange;
        [SerializeField] private SimpleMassTorqueCalculator _torqueCalculator;
        [SerializeField] private float _massTruck = 20f;
        [SerializeField] private float _massWater = 40f;
        [SerializeField] private float _deltaMassWaterChage = 0.5f;

        private float _coef = 0;
        private float _mass;

        private void OnEnable()
        {
            StartCoroutine(IncreaseMass());
            StartCoroutine(ChangeMassWater());
        }

        private IEnumerator IncreaseMass()
        {
            float massCoefStart = 0.005f;
            float massCoefFinish = 1f;
            _mass = _massTruck + _massWater;
            float heightStart = _yCoordinateForMassChange - gameObject.transform.position.y;
            float height = 0;

            while (true)
            {
                Debug.Log(gameObject.transform.position.y);

                height = _yCoordinateForMassChange - gameObject.transform.position.y;
                _coef = Mathf.Lerp(massCoefFinish, massCoefStart, height / heightStart);
                _mass = (_massTruck + _massWater) * _coef;
                _torqueCalculator.ManualChangeMass(_mass);
                yield return null;
            }
        }

        private IEnumerator ChangeMassWater()
        {
            float mass = _massWater;

            while (mass > 0)
            {
                if (gameObject.transform.position.y < _yCoordinateForWater)
                {
                    _massWater = mass;
                    yield return new WaitWhile(() => gameObject.transform.position.y > _yCoordinateForWater);
                }

                _massWater -= _deltaMassWaterChage;

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
