using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeColorResult : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _outputTextFields;
    [SerializeField] private Calculate _calculate;
    [SerializeField] private Color _outdatedDataColor;
    [SerializeField] private Color _currentDataColor;

    private bool _isUpdate = false;

    private void OnEnable()
    {
        _calculate.Resulted += ChangeColorResultCurrentData;
    }

    private void OnDisable()
    {
        _calculate.Resulted -= ChangeColorResultCurrentData;
    }

    public void ChangeColorResultOutData()
    {
        if (_isUpdate == true)
        {
            _isUpdate = false;

            foreach (TMP_Text textField in _outputTextFields)
            {
                textField.color = _outdatedDataColor;
            }
        }
    }

    private void ChangeColorResultCurrentData()
    {
        if (_isUpdate == false)
        {
            _isUpdate = true;

            foreach (TMP_Text textField in _outputTextFields)
            {
                textField.color = _currentDataColor;
            }
        }
    }
}
