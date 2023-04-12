using System.Collections.Generic;
using UnityEngine;

public class PencilBonusSetter : MonoBehaviour
{
    [SerializeField] private List<PencilUpgrader> _pencilUpgraders;
    [SerializeField] private Transform _parentObject;
    [SerializeField] private Animator _animatorObject;
    [SerializeField] private PencilBonusPanel _pencilBonusPanel;

    private PencilUpgrader _pencilUpgrader;

    private void OnValidate()
    {
        _pencilBonusPanel = FindObjectOfType<PencilBonusPanel>();
    }

    public void TakeNumberColor(int numberColor)
    {
        for (int i = 0; i < _pencilUpgraders.Count; i++)
        {
            if ((int)_pencilUpgraders[i].ColorPencil == numberColor)
            {
                SetPencil(_pencilUpgraders[i]);
                _pencilUpgrader = _pencilUpgraders[i];
            }
        }
    }

    public void Activate(bool isActiveStatus)
    {
        _animatorObject.gameObject.SetActive(isActiveStatus);

        if (isActiveStatus == true)
        {
            _pencilUpgrader.Upgrade();
        }
    }

    private void SetPencil(PencilUpgrader pencilUpgrader)
    {
        int scaleCorrector = 2;
        pencilUpgrader.transform.parent = _parentObject;
        pencilUpgrader.transform.localScale = new Vector3(pencilUpgrader.transform.localScale.x / scaleCorrector, pencilUpgrader.transform.localScale.y / scaleCorrector, pencilUpgrader.transform.localScale.z / scaleCorrector);
        PassPencilUpgrader(pencilUpgrader);
    }

    private void PassPencilUpgrader(PencilUpgrader pencilUpgrader)
    {
        _pencilBonusPanel.GetPencil(pencilUpgrader);
    }
}
