using UnityEngine;

public class Frame : MonoBehaviour
{
    private const string IsOpenName = nameof(IsOpenName);

    [SerializeField] private Sprite _cover;
    [SerializeField] private Sprite _coverInactive;
    [SerializeField] private int _cost = 500;
    [SerializeField] private bool _isSale;
    [SerializeField] private bool _isDefault;

    private bool _isActive = false;

    public bool IsOpen { get { return PlayerPrefs.GetInt(IsOpenName + gameObject.name, 0) == 1; } set { PlayerPrefs.SetInt(IsOpenName + gameObject.name, value == true ? 1 : 0); } }

    private void Awake()
    {
        if (_isDefault == true)
        {
            IsOpen = true;
            _isSale = true;
        }
    }

    public void Deactivation()
    {
        gameObject.SetActive(false);
    }

    public int GetCost()
    {
        if (_isSale == true)
        {
            return _cost;
        }
        else
        {
            return 0;
        }
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }

    public void SetOpen()
    {
        IsOpen = true;
    }

    public bool GetActiveStatus()
    {
        return _isActive;
    }

    public bool GetOpenStatus()
    {
        return IsOpen;
    }

    public bool GetSaleStatus()
    {
        return _isSale;
    }

    public Sprite GetCover()
    {
        return _cover;
    }

    public Sprite GetCoverInactive()
    {
        return _coverInactive;
    }
}
