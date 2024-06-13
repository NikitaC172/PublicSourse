using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BowlingGame : MonoBehaviour
{
    [SerializeField] private PanelChanger _panelChanger;
    [SerializeField] private ConsoleStarGame _consoleStarGame;
    [SerializeField] private PinSystem _pinSystem;
    [SerializeField] private BallDispenser _ballDispenser;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _textCurrentThrow;
    [SerializeField] private TMP_Text _textPinsFall;
    [SerializeField] private TMP_Text _textLeftThrow;

    private int _totalCountThrow;
    private int _currentThrow = 0;
    private int _leftThrow;
    private int _pins;
    private int _currentPinsFall = 0;
    private bool _isGameStarted;

    public Action<int, int> EndedGame;

    private void OnEnable()
    {
        _consoleStarGame.StartedGame += StartGame;
        _pinSystem.BallCameOut += IncreaseThrow;
        _pinSystem.PinsFell += IncreasePinsFall;
    }

    private void OnDisable()
    {
        _consoleStarGame.StartedGame -= StartGame;
        _pinSystem.BallCameOut -= IncreaseThrow;
        _pinSystem.PinsFell -= IncreasePinsFall;
    }

    private void IncreaseThrow()
    {
        _currentThrow++;
        _leftThrow = _totalCountThrow - _currentThrow;
        _textCurrentThrow.text = _currentThrow.ToString();
        _textLeftThrow.text = _leftThrow.ToString();

        if (_currentThrow == _totalCountThrow)
        {
            _isGameStarted = false;
        }

        ResetBall();
    }

    private void IncreasePinsFall(int pinsFall, int allPins)
    {
        _pins = pinsFall;
        _textPinsFall.text = (_pins + _currentPinsFall).ToString();

        if (pinsFall == allPins)
        {
            ResetPins();
            _currentPinsFall = pinsFall;

            if (_isGameStarted == false)
            {
                _pins = 0;
            }
        }

        if (_isGameStarted == false)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        _currentPinsFall += _pins;
        EndedGame?.Invoke(_totalCountThrow, _currentPinsFall);
    }

    private void ResetPinsAndBall()
    {
        if (_isGameStarted == true)
        {
            _pinSystem.ResetPins();
            _ballDispenser.SpawnBall();
        }
    }

    private void ResetPins()
    {
        if (_isGameStarted == true)
        {
            _pinSystem.ResetPins();
        }
    }

    private void ResetBall()
    {
        if (_isGameStarted == true)
        {
            _ballDispenser.SpawnBall();
        }
    }

    private void ResetGame()
    {
        _currentThrow = 0;
        _textCurrentThrow.text = _currentThrow.ToString();
        _textPinsFall.text = 0.ToString();
        _textLeftThrow.text = 0.ToString();
    }

    private void StartGame(int countThrow)
    {
        _currentPinsFall = 0;
        _leftThrow = countThrow;
        _panelChanger.ChangePanel(_panel);
        ResetGame();
        _isGameStarted = true;
        ResetPinsAndBall();
        _totalCountThrow = countThrow;
        _textLeftThrow.text = countThrow.ToString();
    }
}
