using System.Collections.Generic;
using UnityEngine;

public class LineSystemPickerSetter : MonoBehaviour
{
    [SerializeField] private List<LinePositionSetter> _linePositionSetters;
    [SerializeField] private List<LineObjectPencilPicker> _lineObjectPencilPickers;

    private List<bool> _linePositionSettersStateFulled = new List<bool>();

    public List<LineObjectPencilPicker> LineObjectPencilPickers  => _lineObjectPencilPickers;

    private void OnEnable()
    {
        for (int i = 0; i < _linePositionSetters.Count; i++)
        {
            _linePositionSettersStateFulled.Add(false);
        }

        foreach (LinePositionSetter _linePositionSetter in _linePositionSetters)
        {
            _linePositionSetter.Fulled += TryChangeStatePicker;
        }
    }

    private void TryChangeStatePicker(LinePositionSetter setter, bool stateLine)
    {
        int numberSetter = _linePositionSetters.IndexOf(setter);

        if (_linePositionSettersStateFulled[numberSetter] != stateLine)
        {
            _linePositionSettersStateFulled[numberSetter] = stateLine;
            ChangeStateObjectPencilPicker(_linePositionSettersStateFulled.IndexOf(false));
        }

    }

    private void ChangeStateObjectPencilPicker(int numberNotFilled)
    {
        int notfound = -1;

        if (numberNotFilled == notfound)
        {
            for (int i = 0; i < _lineObjectPencilPickers.Count; i++)
            {
                if (i == _lineObjectPencilPickers.Count - 1)
                {
                    _lineObjectPencilPickers[i].gameObject.SetActive(true);
                }
                else
                {
                    _lineObjectPencilPickers[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < _lineObjectPencilPickers.Count; i++)
            {
                if (i == numberNotFilled)
                {
                    _lineObjectPencilPickers[i].gameObject.SetActive(true);
                }
                else
                {
                    _lineObjectPencilPickers[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
