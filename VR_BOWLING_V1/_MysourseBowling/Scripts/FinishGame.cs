using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private PanelChanger _panelChanger;
    [SerializeField] private BowlingGame _game;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _textThrow;
    [SerializeField] private TMP_Text _textPins;

    private bool _isGameOver = false;

    public Action RestartedGame;

    private void OnEnable()
    {
        _game.EndedGame += ShowResult;
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        _game.EndedGame -= ShowResult;
        _restartButton.onClick.RemoveListener(RestartGame);
    }

    private void RestartGame()
    {
        RestartedGame?.Invoke();
        _isGameOver = false;
    }

    private void ShowResult(int totalThrow, int pins)
    {
        if (_isGameOver == false)
        {
            _isGameOver = true;
            _panelChanger.ChangePanel(_panel);
            _textThrow.text = totalThrow.ToString();
            _textPins.text = pins.ToString();
        }
    }
}
