using System;
using UnityEngine;

public class PoolObjectUpgraderPencil : MonoBehaviour
{
    private ColorsObject _color;

    public Action<int> Upgraded;

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

    private void Start()
    {
        CheckingSaveForOpenImprovement();
    }

    public void Upgrade(int numberColor)
    {
        Upgraded?.Invoke(numberColor);
    }

    private void CheckingSaveForOpenImprovement()
    {
        if (SaveSystem.PencilUpgrades.BlueCollect == 1)
        {
            Upgrade((int)ColorsObject.Blue);
        }

        if (SaveSystem.PencilUpgrades.RedCollect == 1)
        {
            Upgrade((int)ColorsObject.Red);
        }

        if (SaveSystem.PencilUpgrades.GreenCollect == 1)
        {
            Upgrade((int)ColorsObject.Green);
        }

        if (SaveSystem.PencilUpgrades.PurpleCollect == 1)
        {
            Upgrade((int)ColorsObject.Purple);
        }

        if (SaveSystem.PencilUpgrades.YellowCollect == 1)
        {
            Upgrade((int)ColorsObject.Yellow);
        }

        if (SaveSystem.PencilUpgrades.PinkCollect == 1)
        {
            Upgrade((int)ColorsObject.Pink);
        }

        if (SaveSystem.PencilUpgrades.OrangeCollect == 1)
        {
            Upgrade((int)ColorsObject.Orange);
        }
    }
}
