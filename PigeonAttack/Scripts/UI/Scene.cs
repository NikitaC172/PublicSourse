using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _hideButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Animator _animatorEndPanel;
    [SerializeField] private Animator _animatorStartPanel;
    [SerializeField] private Animator _animatorScore;
    [SerializeField] private UIJoystick _uIJoystick;
    [SerializeField] private PlayerMover _playerMover;

    private float _delayBeforeRestartScene = 1.2f;
    private float _delayBeforeRestartPlayer = 0.5f;

    private const string RepeatGameAnimButton = "RepeatButton";
    private const string HideEndScreenAnim = "HideEndScreen";
    private const string ShowDragToMoveAnim = "ShowDragToMove";
    private const string HideDragToMoveAnim = "HideDragToMove";
    private const string ShowScoreAnim = "Show";

    public event UnityAction Restarted;
    public event UnityAction Started;

    private void OnEnable()
    {
        _menuButton.onClick.AddListener(OpenMenu);
        _restartButton.onClick.AddListener(OnClickButtonRestart);
        _hideButton.onClick.AddListener(OnClickButtonHide);
        _playerMover.Moved += OnClickButtonHide;
    }

    private void OnDisable()
    {
        _menuButton.onClick.RemoveListener(OpenMenu);
        _restartButton.onClick.RemoveListener(OnClickButtonRestart);
        _hideButton.onClick.RemoveListener(OnClickButtonHide);
    }

    private void OnClickButtonHide()
    {
        _playerMover.Moved -= OnClickButtonHide;
        Started?.Invoke();
        _animatorStartPanel.Play(HideDragToMoveAnim);
        _animatorScore.Play(ShowScoreAnim);
        _hideButton.enabled = false;
        _animatorStartPanel.gameObject.SetActive(false);
        _uIJoystick.gameObject.SetActive(true);
    }

    private void OnClickButtonRestart()
    {
        Restarted?.Invoke();
        _animatorEndPanel.Play(RepeatGameAnimButton);
        Invoke(nameof(RestartPlayer), _delayBeforeRestartPlayer);
        Invoke(nameof(RestartScene), _delayBeforeRestartScene);
    }

    private void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void RestartPlayer()
    {
        Restarted?.Invoke();
        _animatorEndPanel.Play(HideEndScreenAnim);
        _animatorStartPanel.gameObject.SetActive(true);
        _animatorStartPanel.Play(ShowDragToMoveAnim);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
