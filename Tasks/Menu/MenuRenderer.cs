using UnityEngine;

public class MenuRenderer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _timeDeactivation = 1f;

    private const string ShowPanel = "ShowMenu";
    private const string HidePanel = "HideMenu";

    public void ShowMenu()
    {
        _animator.gameObject.SetActive(true);
        _animator.Play(ShowPanel);
    }

    public void HideMenu()
    {
        _animator.Play(HidePanel);
        Invoke(nameof(DeactivateObject), _timeDeactivation);
    }

    private void DeactivateObject()
    {
        _animator.gameObject.SetActive(false);
    }
}
