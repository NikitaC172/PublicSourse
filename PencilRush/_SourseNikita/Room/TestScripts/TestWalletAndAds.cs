using UnityEngine;

public class TestWalletAndAds : MonoBehaviour
{
    [SerializeField] private int _money = 600;

    public bool TryTakeFrame(int price)
    {
        if ((_money - price) >= 0)
        {
            _money -= price;
            return true;
        }
        else
        {
            return false;
        }
    }
}
