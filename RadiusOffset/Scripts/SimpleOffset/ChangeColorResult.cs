using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeColorResult : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _outputTextFields;
    [SerializeField] private SimpleOffset _simpleOffset;
    [SerializeField] private Color _outdatedDataColor;
    [SerializeField] private Color _currentDataColor;

    private bool _isOffsetUpdate = false;
    private bool _isSideUpdate = false;
    private bool _isSizeUpdate = false;

    private void OnEnable()
    {
        _simpleOffset.OffsetCalculated += ChangeColorOffset;
        _simpleOffset.SideCalculated += ChangeColorSide;
        _simpleOffset.SizeCalculated += ChangeColorSize;
    }

    private void OnDisable()
    {
        _simpleOffset.OffsetCalculated -= ChangeColorOffset;
        _simpleOffset.SideCalculated -= ChangeColorSide;
        _simpleOffset.SizeCalculated -= ChangeColorSize;
    }

    public void ChangeColorResultOutData()
    {
        if (_isOffsetUpdate == true || _isSideUpdate == true || _isSizeUpdate == true)
        {
            _isOffsetUpdate = false;
            _isSideUpdate = false;
            _isSizeUpdate = false;

            foreach (TMP_Text textField in _outputTextFields)
            {
                textField.color = _outdatedDataColor;
            }
        }
    }

    private void ChangeColorOffset()
    {
        if (_isOffsetUpdate == false)
        {
            _outputTextFields[0].color = _currentDataColor;
            _isOffsetUpdate = true;
        }
    }

    private void ChangeColorSide()
    {
        if (_isSideUpdate == false)
        {
            _outputTextFields[2].color = _currentDataColor;
            _outputTextFields[3].color = _currentDataColor;
            _isSideUpdate = true;
        }
    }

    private void ChangeColorSize()
    {
        if (_isSizeUpdate == false)
        {
            _outputTextFields[1].color = _currentDataColor;
            _isSizeUpdate = true;
        }
    }
}
