using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using AdvancedInputFieldPlugin;

public class SimpleOffset : MonoBehaviour
{
    [SerializeField] private AdvancedInputField _inputRadius;
    [SerializeField] private AdvancedInputField _inputAngle;
    [SerializeField] private AdvancedInputField _inputLenght;
    [SerializeField] private AdvancedInputField _inputSize;
    [SerializeField] private TMP_Text _outputOffset;
    [SerializeField] private TMP_Text _output2Lenght;
    [SerializeField] private TMP_Text _output2LenghtText;
    [SerializeField] private TMP_Text _output2Angle;
    [SerializeField] private TMP_Text _outputSize;
    [SerializeField] private Toggle _isDiametr;
    [SerializeField] private Toggle _isInverseSide;
    [SerializeField] private Button _resultButton;

    private float _offset;
    private float _otherLeg;

    public Action OffsetCalculated;
    public Action SideCalculated;
    public Action SizeCalculated;

    private void Start()
    {
        var enUS = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        
    }

    private void OnEnable()
    {
        _resultButton.onClick.AddListener(ResultOffset);
    }

    private void OnDisable()
    {
        _resultButton.onClick.RemoveListener(ResultOffset);
    }

    private void ResultOffset()
    {
        _offset = 0;
        _otherLeg = 0;
        float inputRadius;
        float inputAngle;
        bool isCorrectedInputRadius;
        bool isCorrectedInputAngle;
        isCorrectedInputRadius = float.TryParse(_inputRadius.Text.Replace('.',','), out inputRadius);
        isCorrectedInputAngle = float.TryParse(_inputAngle.Text.Replace('.', ','), out inputAngle);

        if (isCorrectedInputRadius == true && isCorrectedInputAngle == true)
        {
            OffsetCalculate(inputRadius, inputAngle);
            OtherSideCalculate(inputAngle);
            CalculateSize();
        }
    }

    private void OffsetCalculate(float inputRadius, float inputAngle)
    {
        _offset = inputRadius * (1 - Mathf.Tan(Mathf.Deg2Rad * inputAngle / 2));
        _outputOffset.text = Math.Round(_offset,3).ToString();
        OffsetCalculated?.Invoke();
    }

    private void OtherSideCalculate(float inputAngle)
    {
        float lenght;
        bool isCorrectedLenght = float.TryParse(_inputLenght.Text.Replace('.', ','), out lenght);

        if (isCorrectedLenght == true)
        {
            _otherLeg = lenght / Mathf.Tan(Mathf.Deg2Rad * inputAngle);
            float otherAngle = 90 - inputAngle;
            _output2Lenght.text = Math.Round(_otherLeg,3).ToString();
            _output2Angle.text = Math.Round(otherAngle,3).ToString();
            SideCalculated?.Invoke();
        }
    }

    private void CalculateSize()
    {
        float size;
        int DiametrCoefficient;
        int InverseCoefficient;
        bool isCorrectedSize = float.TryParse(_inputSize.Text.Replace('.', ','), out size);

        if (isCorrectedSize == true)
        {
            if (_isDiametr.isOn == true)
            {
                DiametrCoefficient = 2;
            }
            else
            {
                DiametrCoefficient = 1;
            }

            if (_isInverseSide.isOn == true)
            {
                InverseCoefficient = -1;
            }
            else
            {
                InverseCoefficient = 1;
            }

            size += DiametrCoefficient * ((InverseCoefficient) * (-_otherLeg - _offset));
            ChangeTextOutput();
            _outputSize.text = Math.Round(size,3).ToString();
            SizeCalculated?.Invoke();
        }
    }

    private void ChangeTextOutput()
    {
        string text = string.Empty;

        if ( _isDiametr.isOn == true)
        {
            text += "Диаметр\n";
        }
        else
        {
            text += "Длина\n";
        }

        if(_isInverseSide.isOn == true)
        {
            text += "(смещение в плюс)";
        }
        else
        {
            text += "(смещение в минус)";
        }

        _output2LenghtText.text = text;
    }
}
