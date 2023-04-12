using System.Collections.Generic;
using UnityEngine;

public class ImprovedPencilTrigger : MonoBehaviour
{
    [SerializeField] private ColorsObject _color;
    [SerializeField] private List<PencilUpgrader> _pencilUpgraders;
    [SerializeField] private Transform _parentObject;
    [SerializeField] private int _dropChancePercent = 70;
    [SerializeField] private PoolObjectUpgraderPencil _poolObjectUpgraderPencil;
    [SerializeField] private Animator _animatorObject;
    [SerializeField] private PencilBonusPanel _pencilBonusPanel;

    private void OnValidate()
    {
        _poolObjectUpgraderPencil = FindObjectOfType<PoolObjectUpgraderPencil>();
        _pencilBonusPanel = FindObjectOfType<PencilBonusPanel>();
    }

    private void Start()
    {
        if (CheckingSaveForOpenImprovement() == false)
        {
            _pencilBonusPanel.Activate((int)_color);

            if (ChanceCheck() > 100 - _dropChancePercent)
            {
                InsertPencil();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PencilFollower>(out PencilFollower pencilFollower))
        {
            gameObject.SetActive(false);
        }
    }

    private bool CheckingSaveForOpenImprovement()
    {
        switch (_color)
        {
            case (ColorsObject)PencilUpgrader.ColorsObject.Blue:

                if (SaveSystem.PencilUpgrades.BlueCollect == 1)
                {
                    return true;
                }
                break;
            case (ColorsObject)PencilUpgrader.ColorsObject.Red:

                if (SaveSystem.PencilUpgrades.RedCollect == 1)
                {
                    return true;
                }
                break;
            case (ColorsObject)PencilUpgrader.ColorsObject.Green:

                if (SaveSystem.PencilUpgrades.GreenCollect == 1)
                {
                    return true;
                }
                break;
            case (ColorsObject)PencilUpgrader.ColorsObject.Purple:

                if (SaveSystem.PencilUpgrades.PurpleCollect == 1)
                {
                    return true;
                }
                break;
            case (ColorsObject)PencilUpgrader.ColorsObject.Yellow:

                if (SaveSystem.PencilUpgrades.YellowCollect == 1)
                {
                    return true;
                }
                break;
            case (ColorsObject)PencilUpgrader.ColorsObject.Pink:

                if (SaveSystem.PencilUpgrades.PinkCollect == 1)
                {
                    return true;
                }
                break;
            case (ColorsObject)PencilUpgrader.ColorsObject.Orange:

                if (SaveSystem.PencilUpgrades.OrangeCollect == 1)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    private int ChanceCheck()
    {
        int randomNumberPercent = Random.Range(0, 100);
        return randomNumberPercent;
    }

    private void InsertPencil()
    {
        for (int i = 0; i < _pencilUpgraders.Count; i++)
        {
            if ((int)_pencilUpgraders[i].ColorPencil == (int)_color)
            {
                _pencilUpgraders[i].transform.parent = _parentObject;
                _animatorObject.gameObject.SetActive(true);
                _poolObjectUpgraderPencil.Upgrade(i);
                return;
            }
        }
    }

    public enum ColorsObject
    {
        Blue = 0,
        Red = 1,
        Green = 2,
        Purple = 3,
        Yellow = 4,
        Pink = 5,
        Orange = 6,
    }
}
