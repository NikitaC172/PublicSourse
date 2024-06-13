using CraneGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaggingCorrector : MonoBehaviour
{
    [SerializeField] private List<SimpleAxisMover> _simpleAxisMovers;
    [SerializeField] private SimpleMassTorqueCalculator _massTorqueCalculator;
    [SerializeField] private float _sagDegress1Step;
    [SerializeField] private float _sagDegress2Step;
    [SerializeField] private float _sagDegress3Step;
    [SerializeField] private float _sagDegressMass;
    [SerializeField] private float _deltaStep = 0.05f;

    private float _sagAngle = 0;
    private float _currentSagAngle = 0;
    private float _sagAxisMover;
    private float _sagMass;

    public float SagAngle  => _sagAngle;

    private void FixedUpdate()
    {
        _currentSagAngle = _sagAngle;
        _sagAngle = Mathf.Lerp(_currentSagAngle, CalculateSagAxisMover() + CalculateSagMass(), _deltaStep);
    }

    private float CalculateSagAxisMover()
    {
        _sagAxisMover = 
              _sagDegress1Step * _simpleAxisMovers[0].ExtensionCoefficient
            + _sagDegress2Step * _simpleAxisMovers[1].ExtensionCoefficient
            + _sagDegress3Step * _simpleAxisMovers[2].ExtensionCoefficient;
        return _sagAxisMover;
    }

    private float CalculateSagMass()
    {
        _sagMass = _massTorqueCalculator.MassCoefficient * _sagDegressMass;
        return _sagMass;
    }
}
