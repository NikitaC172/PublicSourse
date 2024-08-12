using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderCounter : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private float _value;
    private bool _isStart = false;

    public float Value => _value;

    private void OnEnable()
    {
        _ = ChangeValueSlider();
    }

    private void OnDisable()
    {
        StopOperation();
    }

    public void StopOperation()
    {
        _isStart = false;
    }

    private async UniTask ChangeValueSlider()
    {
        _isStart = true;
        float value = 0;
        float step = 0.5f;

        while (_isStart == true)
        {
            _slider.value = value;
            value = Math.Clamp(value += step * Time.deltaTime, 0, 1);
            await UniTask.Yield();

            if (value == 1f || value == 0f)
            {
                step *= -1f;
            }
        }
    }
}
