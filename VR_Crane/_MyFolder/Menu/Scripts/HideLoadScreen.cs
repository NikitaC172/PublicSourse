using System;
using System.Collections;
using UnityEngine;

public class HideLoadScreen : MonoBehaviour
{
    [SerializeField] private float _timeWaitAfterLoad;
    [SerializeField] private float _timeToHide;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _startAlpha = 1;
    [SerializeField] private float _finishAlpha = 0;
    [SerializeField] private bool _isActive = false;

    public Action Activated;
    public Action Deactivated;

    private void OnEnable()
    {
        Activated?.Invoke();
        StartCoroutine(Hide(_startAlpha, _finishAlpha));
    }

    private IEnumerator Hide(float _startAlpha, float _finishAlpha)
    {
        //Activated?.Invoke();
        yield return new WaitForSecondsRealtime(_timeWaitAfterLoad);
        Deactivated?.Invoke();
        float time = 0f;
        _canvasGroup.alpha = _startAlpha;

        while (time < _timeToHide)
        {
            time += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, _finishAlpha, time / _timeToHide);
            yield return null;
        }

        gameObject.SetActive(_isActive);
    }
}
