using UnityEngine;

public class PicturePointAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string HideAnimation = "Hide";
    private const string ShowAnimation = "Show";

    public void HideAnim()
    {
        _animator.Play(HideAnimation);
    }

    public void ShowAnim()
    {
        _animator.Play(ShowAnimation);
    }
}
