using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    [SerializeField] private GameObject PanelForOpen;
    [SerializeField] private Menu _menu;

    public void ChangePanel()
    {
        GameObject currentPanel = _menu.CurrentPanelSet(PanelForOpen);
        currentPanel.SetActive(false);
        PanelForOpen.SetActive(true);
    }

}
