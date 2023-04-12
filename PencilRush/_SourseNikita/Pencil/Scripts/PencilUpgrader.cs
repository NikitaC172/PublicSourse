using UnityEngine;

public class PencilUpgrader : MonoBehaviour
{
    [SerializeField] private PoolObjectUpgraderPencil _poolObjectUpgraderPencil;
    [SerializeField] private ColorsObject _color = ColorsObject.Blue;
    [SerializeField] private bool _isUpgradable = true;
    [SerializeField] private bool _isRotatable = true;
    [SerializeField] private PencilObjectMiddle _pencilobjectMiddle;
    [SerializeField] private PencilObjectMiddleUpgrade _pencilobjectMiddleUpgrade;    

    private bool _isUpgrade = false;

    public bool IsUpgrade => _isUpgrade;
    public ColorsObject ColorPencil  => _color;

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

    private void OnValidate()
    {
        _poolObjectUpgraderPencil = FindObjectOfType<PoolObjectUpgraderPencil>();
    }

    private void OnEnable()
    {
        _poolObjectUpgraderPencil.Upgraded += CheckImprovement;
    }

    public void Upgrade()
    {
        _isUpgrade = true;
        _pencilobjectMiddleUpgrade.EnableObject(_isRotatable);
        _pencilobjectMiddle.DisableObject();
    }

    private void CheckImprovement(int numberColor)
    {
        if(numberColor == ((int)_color) && _isUpgradable == true)
        {
            Upgrade();
        }
    }    
}
