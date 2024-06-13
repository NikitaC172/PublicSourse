using System;
using System.Collections;
using UnityEngine;

public class ConsoleButton : MonoBehaviour
{
    [SerializeField] private Material _buttonMaterial;
    [SerializeField] private Color _colorSelect;
    [SerializeField] private Color _colorActivate;
    [SerializeField] private Color _colorCurrent;
    [SerializeField] private bool testActivate = false;
    [SerializeField] private bool testSelect = false;
    [SerializeField] private float _waitSeconds = 1.5f;

    private IEnumerator _enumerator = null;
    private bool isReadyActivate = false;
    public Action Activated;

    private void Start()
    {
        _buttonMaterial.color = _colorCurrent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Picker>(out Picker picker))
        {
            isReadyActivate = true;
            Select(_colorSelect);
            picker.Activated += Activate;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Picker>(out Picker picker))
        {
            isReadyActivate = false;
            Select(_colorCurrent);
            picker.Activated -= Activate;
        }
    }

    private void FixedUpdate()
    {
        if (testActivate == true)
        {
            testActivate = false;
            isReadyActivate = true;
            Activate();
        }
        if (testSelect == true)
        {
            testSelect = false;
            Select(_colorSelect);
        }
    }

    public void Select(Color color)
    {
        {
            _enumerator = ChangeColor(color);
            StartCoroutine(_enumerator);
        }
    }

    public void Activate()
    {
        if(_enumerator != null)
        {
            StopCoroutine(_enumerator);
            _enumerator = null;
        }

        if (isReadyActivate == true)
        {
            StartCoroutine(RunAction());
        }
    }

    private IEnumerator ChangeColor(Color color)
    {
        float timeChange = 1.0f;
        float time = 0f;
        while (time < timeChange)
        {
            time += Time.deltaTime;
            _buttonMaterial.color = Color.Lerp(_buttonMaterial.color, color, time / timeChange);
            yield return null;
        }

        _enumerator = null;
    }

    private IEnumerator RunAction()
    {
        isReadyActivate = false;
        var waitForSecondsRealtime = new WaitForSecondsRealtime(_waitSeconds);
        StartCoroutine(ChangeColor(_colorActivate));
        Activated?.Invoke();
        yield return waitForSecondsRealtime;
        _enumerator = ChangeColor(_colorCurrent);
        StartCoroutine(_enumerator);
        isReadyActivate = true;
    }
}
