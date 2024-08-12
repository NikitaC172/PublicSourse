using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<MenuRenderer> _menuRenderers;
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private List<Button> _buttonTasks;
    [SerializeField] private Button _buttonShoot;
    [SerializeField] private Button _buttonReset;

    private void OnEnable()
    {
        SetMenuTask();
        
        foreach (var button in _buttonTasks) 
        {
            button.onClick.AddListener(SetMenuShoot);
        }

        _buttonShoot.onClick.AddListener(SetMenuReset);
        _buttonReset.onClick.AddListener(SetMenuTask);
    }

    private void SetMenuShoot()
    {
        _ = SetMenu(1);
    }

    private void SetMenuReset()
    {
        _ = SetMenu(2);
    }

    private void SetMenuTask()
    {
        _ = SetMenu(0);
    }

    private async UniTask SetMenu(int index)
    {
        if(currentIndex != index)
        {
            _menuRenderers[currentIndex].HideMenu();
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }

        currentIndex = index;
        _menuRenderers[currentIndex].ShowMenu();
    }
}
