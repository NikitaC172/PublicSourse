using UnityEngine;

public class FrameObjectPanel : MonoBehaviour
{
    [SerializeField] private Animator _animatorPanel;

    private float delayDeactivation = 0.35f;

    private const string HideAnimation = "Hide";
    private const string ShowAnimation = "Show";

    public void HidePanel()
    {
        _animatorPanel.Play(HideAnimation);
        Invoke(nameof(DeactivationPanel), delayDeactivation);
    }

    public void ShowPanel()
    {
        _animatorPanel.Play(ShowAnimation);
    }

    private void DeactivationPanel()
    {
        gameObject.SetActive(false);
    }
}
