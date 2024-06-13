using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _currentPanel;
    private GameObject _panel;

    private void Awake()
    {
        _panel = _currentPanel;
    }

    public GameObject CurrentPanelSet(GameObject newPanel)
    {
        _currentPanel = _panel;
        _panel = newPanel;
        return _currentPanel;
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
