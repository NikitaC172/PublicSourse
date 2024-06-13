using System;
using System.Collections;
using UnityEngine;

public class PinTransparent : MonoBehaviour
{
    [SerializeField] private PinSystem _pinSystem;
    [SerializeField] private Pin _pin;
    [SerializeField] private float _smoothTime = 1.5f;
    [SerializeField] private MeshRenderer _meshRenderer;

    private Item _item;
    private Vector3 _velocity = Vector3.zero;
    private IEnumerator _enumerator;
    private bool _isMove = false;
    private bool _isActive = false;
    private bool _isReadyPin = false;
    private bool _isKinematic = false;

    public Action Ready;
    public Action NotReady;

    private void OnTriggerEnter(Collider other)
    {
        if (_isActive == false && other.TryGetComponent<Pin>(out Pin pin))
        {
            if (_pin == null && _isReadyPin == false && _isKinematic == false)
            {
                _pin = pin;
                _item = pin.GetComponent<Item>();
                _enumerator = MoveToPosition();
                StartCoroutine(_enumerator);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isActive == false && other.TryGetComponent<Pin>(out Pin pin))
        {
            if (_isKinematic == false)
            {
                if (_isMove == true && _enumerator != null)
                {
                    StopCoroutine(_enumerator);
                    _item = null;
                    _enumerator = null;
                }

                ShowRender();
                _pin = null;
                _isReadyPin = false;
                NotReady?.Invoke();
            }

        }
    }

    private void OnEnable()
    {
        _pin = null;
        _isReadyPin = false;
        _pinSystem.ActivatedGame += SetActive;
    }

    private void OnDisable()
    {
        _pinSystem.ActivatedGame -= SetActive;
    }

    public void HideRender()
    {
        _meshRenderer.enabled = false;
    }

    public void ShowRender()
    {
        _meshRenderer.enabled = true;
    }

    private void SetActive(bool isActive)
    {
        _isActive = isActive;        
    }

    private IEnumerator MoveToPosition()
    {
        _isMove = true;
        yield return new WaitWhile(() => _item.IsTaked == true);

        if (_item.IsTaked == false)
        {
            _isKinematic = true;
            _item.DeactivateGravity();
            Transform target = transform;
            Transform startPosition = _pin.transform;
            float currentTime = -1f;

            while (_smoothTime > currentTime)
            {
                _pin.transform.position = Vector3.SmoothDamp(startPosition.position, target.position, ref _velocity, _smoothTime);
                _pin.transform.localEulerAngles = Vector3.zero;
                currentTime += Time.deltaTime;
                yield return null;
            }

            _pin.transform.position = target.position;
            _isReadyPin = true;
            _item.ActivateGravity();
            yield return null;
            HideRender();
            _isMove = false;
            _isKinematic = false;
            Ready?.Invoke();
        }
    }
}
