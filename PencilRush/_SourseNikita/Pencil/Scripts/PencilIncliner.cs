using System.Collections;
using UnityEngine;

public class PencilIncliner : MonoBehaviour
{
    [SerializeField] private PencilObjectRotation _objectRotation;
    [SerializeField] private float _multiplierIncline = 4.5f;
    [SerializeField] private LineSystemPosition _lineSystemPosition;

    private bool _isActive = true;

    private void OnValidate()
    {
        _lineSystemPosition = FindObjectOfType<LineSystemPosition>();
    }

    private void OnEnable()
    {
        _isActive = true;
        StartIncline();
    }

    public void StopIncline()
    {
        _isActive = false;
        _objectRotation.transform.Rotate(new Vector3(0, 0, 0));
    }

    private void StartIncline()
    {
        StartCoroutine(Incline());
    }

    private IEnumerator Incline()
    {

        while (_isActive)
        {
            _objectRotation.transform.localEulerAngles = new Vector3(0, 0, _lineSystemPosition.Drag * _multiplierIncline);
            yield return null;
        }
    }
}
