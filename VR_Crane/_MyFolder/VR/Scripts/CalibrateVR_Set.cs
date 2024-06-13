using System;
using UnityEngine;
using UnityEngine.UI;

public class CalibrateVR_Set : MonoBehaviour
{
    [SerializeField] private float _deltaMove;
    [SerializeField] private Button _button;
    [SerializeField] private Vector3 _vectorMove;

    public Action<Vector3> OffsetChanged;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        OffsetChanged?.Invoke(_vectorMove * _deltaMove);
    }
}
