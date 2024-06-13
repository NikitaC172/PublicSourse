using UnityEngine;
using UnityEngine.UI;

public class MybuttonClick : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        Application.targetFrameRate = 15;
    }

    public void Click()
    {
        _button.onClick.Invoke();
    }
}
