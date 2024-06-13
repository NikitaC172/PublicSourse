using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuInGame : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private List<GameObject> _itemsPanel;
    [SerializeField] private List<XRRayInteractor> _rays;
    private bool _isActive = false;
    private bool _isFistStateRays = false;

    private void OnEnable()
    {}

    private void OnDisable()
    {

    }

    public void Menu()
    {
        if (_isActive == false)
        {
            ShowMenu();
            ShowRays();
        }
        else
        {
            HideMenu();
            HideRays();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    private void ShowMenu()
    {
        _isActive = true;
        _isFistStateRays = _rays[0].enabled;
        _menuCanvas.SetActive(true);
    }

    private void HideMenu()
    {
        _isActive = false;
        _menuCanvas.SetActive(false);

        foreach (var item in _itemsPanel)
        {
            item.SetActive(false);
        }

        _itemsPanel[0].SetActive(true);
    }

    private void ShowRays()
    {
        foreach (var ray in _rays)
        {
            ray.enabled = true;
        }
    }

    private void HideRays()
    {
        foreach (var ray in _rays)
        {
            ray.enabled = _isFistStateRays;
        }
    }
}
