using System;
using UnityEngine;

public class PictureOpener : MonoBehaviour
{
    [SerializeField] private PackageAnimator _packageObject;
    [SerializeField] private HandObject _handObject;    

    private bool _isOpen = true;
    private bool _isReadyToOpen;

    public Action Opened;

    public bool IsOpen => _isOpen;
    public bool IsReadyToOpen => _isReadyToOpen;

    private void OnEnable()
    {
        _packageObject.Opened += SetOpenStatus;
    }

    private void OnDisable()
    {
        _packageObject.Opened -= SetOpenStatus;
    }

    public bool TryOpenPicture()
    {
        if (_isReadyToOpen == true)
        {
            _isReadyToOpen = false;
            SetReadyToOpen(false);
            StartOpen();

        }

        return _isOpen;
    }

    public void TakeStatus(bool isReadyToOpen)
    {        
        _isReadyToOpen = isReadyToOpen;

        if (isReadyToOpen == true)
        {
            SetCloseStatus();
            SetReadyToOpen(true);
        }
        else
        {
            SetCloseStatus();
        }
    }

    private void StartOpen()
    {
        _packageObject.Open();
        SaveSystem.CurrentOpenedInteractivePicture++;
    }

    private void SetOpenStatus()
    {
        Opened?.Invoke();
        _packageObject.gameObject.SetActive(false);
        _handObject.gameObject.SetActive(false);
        _isOpen = true;
    }

    private void SetCloseStatus()
    {
        _packageObject.gameObject.SetActive(true);
        _isOpen = false;
    }

    private void SetReadyToOpen(bool isShow)
    {
        _handObject.gameObject.SetActive(isShow);
    }
}
