using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleStarGame : MonoBehaviour
{
    [SerializeField] private TMP_Text _textCount;
    [SerializeField] private GameObject _panel;
    [SerializeField] private PanelChanger _panelChanger;
    [SerializeField] private FinishGame _finishGame;
    [SerializeField] private Button _buttonPlus;
    [SerializeField] private Button _buttonMinus;
    [SerializeField] private Button _buttonStartGame;

    private int _count = 1;

    public Action<int> StartedGame;
    public Action Clicked;

    private void OnEnable()
    {
        ResetPanel();
        _finishGame.RestartedGame += ResetPanel;
        _buttonPlus.onClick.AddListener(IncreaseCount);
        _buttonMinus.onClick.AddListener(Decrease);
        _buttonStartGame.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _finishGame.RestartedGame -= ResetPanel;
        _buttonPlus.onClick.RemoveListener(IncreaseCount);
        _buttonMinus.onClick.RemoveListener(Decrease);
        _buttonStartGame.onClick.RemoveListener(StartGame);
    }

    private void IncreaseCount()
    {
        Clicked?.Invoke();
        _count++;
        RenderCount();
    }

    private void RenderCount()
    {
        _textCount.text = _count.ToString();
    }

    private void Decrease()
    {
        if (_count <= 1)
        {
            _count = 1;
        }
        else
        {
            _count--;
        }

        RenderCount();
        Clicked?.Invoke();
    }

    private void ResetPanel()
    {
        _panelChanger.ChangePanel(_panel);
        _count = 1;
        _textCount.text = _count.ToString();
    }

    private void StartGame()
    {
        StartedGame?.Invoke(_count);
    }
}
