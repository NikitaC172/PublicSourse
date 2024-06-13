using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSystem : MonoBehaviour
{
    [SerializeField] private List<Pin> _pins;
    [SerializeField] private List<PinTransparent> _pinTransparents;
    [SerializeField] private Pool _pool;
    [SerializeField] private bool _AcTIVATE = false;
    [SerializeField] private ConsoleButton _button;
    [SerializeField] private AudioSource _audioSource;

    private Ball _ball;
    private int _countReady = 0;
    private int _countDawnPins = 0;
    private bool _isActivateGame = false;
    private bool _isWorkCorotune = false;

    public Action<bool> ActivatedGame;
    public Action<int,int> PinsFell;
    public Action AllPinsFell;
    public Action BallCameOut;

    private void OnEnable()
    {
        _button.Activated += ResetPins;
        BallCameOut += CountingDownedPins;
        SubscribeEvents();
    }

    private void OnDisable()
    {
        _button.Activated -= ResetPins;
        BallCameOut -= CountingDownedPins;
        UnSubscribeEvents();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Pin>(out Pin pin))
        {
            if (_pins.Contains(pin) == false)
            {
                _pins.Add(pin);
            }
        }

        if (other.TryGetComponent<Player>(out _))
        {
            _isActivateGame = false;
            ActivatedGame?.Invoke(_isActivateGame);

            foreach (PinTransparent pinTransparent in _pinTransparents)
            {
                pinTransparent.ShowRender();
            }
        }

        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            _isActivateGame = true;
            ActivatedGame?.Invoke(_isActivateGame);

            if (_isWorkCorotune == false)
            {
                _isWorkCorotune = true;
                _ball = ball;
                StartCoroutine(ControleSpeedBall());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Pin>(out Pin pin))
        {
            _pins.Remove(pin);
        }
        if (other.TryGetComponent<Player>(out _))
        {
            foreach (PinTransparent pinTransparent in _pinTransparents)
            {
                pinTransparent.HideRender();
            }
        }
    }

    private void CountingDownedPins()
    {
        foreach (Pin pin in _pins)
        {
            if(pin.FallCheck() == true)
            {
                _countDawnPins++;
                pin.gameObject.SetActive(false);
            }
        }

        if(_countDawnPins == _pins.Count)
        {
            AllPinsFell?.Invoke();
        }
    }

    public void ResetPins()
    {
        _audioSource.Play();
        HidePins();
        _countDawnPins = 0;
        _countReady = 0;
        _isActivateGame = false;
        ActivatedGame?.Invoke(_isActivateGame);
        ResetTransparentPins();
        CreatePins();
        SetPosition();
        ShowPins();
    }

    private void SubscribeEvents()
    {
        foreach (PinTransparent pinTransparent in _pinTransparents)
        {
            pinTransparent.Ready += ReadyPin;
            pinTransparent.NotReady += NotReadyPin;
        }

    }

    private void UnSubscribeEvents()
    {
        foreach (PinTransparent pinTransparent in _pinTransparents)
        {
            pinTransparent.Ready -= ReadyPin;
            pinTransparent.NotReady -= NotReadyPin;
        }

    }

    private void ReadyPin()
    {
        _countReady++;

        if (_countReady == _pinTransparents.Count - 1)
        {
            _isActivateGame = true;
            ActivatedGame?.Invoke(_isActivateGame);
        }
    }

    private void NotReadyPin()
    {
        if (_isActivateGame == false)
        {
            _countReady--;
        }
    }


    private void ResetTransparentPins()
    {
        foreach (PinTransparent pinTransparent in _pinTransparents)
        {
            pinTransparent.gameObject.SetActive(false);
            pinTransparent.gameObject.SetActive(true);
        }
    }

    private void CreatePins()
    {
        if (_pins.Count < _pinTransparents.Count)
        {
            int needPins = _pinTransparents.Count - _pins.Count;

            for (int i = 0; i < needPins; i++)
            {
                _pins.Add(_pool.Take());
            }
        }
    }

    private void SetPosition()
    {
        for (int i = 0; i < _pinTransparents.Count; i++)
        {
            _pins[i].transform.localEulerAngles = Vector3.zero;
            _pins[i].transform.position = new Vector3(_pinTransparents[i].transform.position.x, _pinTransparents[i].transform.position.y + 0.45f, _pinTransparents[i].transform.position.z);
            _pins[i].GetComponent<Item>().ActivateGravity();
        }
    }

    private void HidePins()
    {
        foreach (Pin pin in _pins)
        {
            pin.gameObject.SetActive(false);
        }
    }

    private void ShowPins()
    {
        foreach (Pin pin in _pins)
        {
            pin.gameObject.SetActive(true);
        }
    }

    private IEnumerator ControleSpeedBall()
    {
        float minSpeedBall = 0.2f;
        float timeWaitAfterBallStop = 3.0f;
        float timeWait = 1.0f;

        yield return new WaitWhile(() => _ball.GetComponent<Rigidbody>().velocity.magnitude > minSpeedBall);
        _ball.gameObject.SetActive(false);
        _ball = null;
        yield return new WaitForSecondsRealtime(timeWaitAfterBallStop);
        CountingDownedPins();
        yield return new WaitForSecondsRealtime(timeWait);
        BallCameOut?.Invoke();
        yield return new WaitForSecondsRealtime(timeWait);
        PinsFell?.Invoke(_countDawnPins, _pins.Count);
        _isWorkCorotune = false;
    }
}
