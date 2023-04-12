using GameAnalyticsSDK;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _textResultLevel;
    [SerializeField] private Animator _score;
    [SerializeField] private Animator _endGamePanel;
    [SerializeField] private ParticleSystem _particle;

    private const string HideScoreAnimation = "Hide";
    private const string WastedText = "ПОймали!";
    private const string CompliteText = "Победа!";

    private void OnEnable()
    {
        _player.Dead += StartAnimation;
        _player.Won += StartAnimation;
    }

    private void Start()
    {
        GameAnalytics.Initialize();
    }

    private void OnDisable()
    {
        _player.Dead -= StartAnimation;
        _player.Won += StartAnimation;
    }

    private void StartAnimation()
    {
        if (_player.IsComplite == false)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, SceneManager.GetActiveScene().buildIndex.ToString());
            _score.Play(HideScoreAnimation);
            _endGamePanel.gameObject.SetActive(true);
        }
        else
        {
            _particle.Play();
            _textResultLevel.text = CompliteText;
            _score.Play(HideScoreAnimation);
            _endGamePanel.gameObject.SetActive(true);
        }
    }
}
