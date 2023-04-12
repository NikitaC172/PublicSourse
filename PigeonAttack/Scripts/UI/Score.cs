using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Score : MonoBehaviour
{
    [SerializeField] private int _saveSlot;
    [SerializeField] private TMP_Text _kills;
    [SerializeField] private int _minKillsForLevel;
    [SerializeField] private TMP_Text _bestScore;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private ScoreStore _store;
    [SerializeField] private Color _colorComplite;
    [SerializeField] private int _pointsMultiplier = 10;
    [SerializeField] private Player _player;

    private float _currentKills = 0;
    private float _currentScore = 0;
    private int _bestCurrentKills = 0;
    private int _currentBread = 0;
    private bool _isComplete = false;
    private bool _isKillMax = false;

    public event UnityAction ChangedBestScore;
    public event UnityAction ChangedScore;
    public event UnityAction CollectedBread;
    public event UnityAction<int, int, int, bool> SavedData;
    public event UnityAction LevelCompleted;

    private const string KillsScoreText = "Голубей: ";
    private const string BestScoreText = "Лучший: ";
    private const string ScoreText = "Счет: ";
    private const float ScaleYMax = 1.5f;
    private const float TimeChangeScale = 0.5f;

    public float CurrentScore => _currentScore;
    public int CurrentBread => _currentBread;

    private void OnEnable()
    {
        TrySetBestScore(_store.GetBestKillsScore(_saveSlot));
        _player.Won += SetCompleteLevel;
        _player.Dead += SaveData;
    }

    public void SaveData()
    {
        SavedData?.Invoke(_bestCurrentKills, (int)_currentScore, _currentBread, _isComplete);
    }

    public void IncreaseScore()
    {
        _currentKills++;
        int comboKills = 2;
        int kills = (int)_currentKills / comboKills;
        SetScore(kills);
        ChangeScale(_kills);
        _kills.text = KillsScoreText + kills + " / " + _minKillsForLevel;
        TrySetBestScore(kills);

        if (kills == _minKillsForLevel && _isKillMax == false)
        {
            _isKillMax = true;
            _kills.faceColor = _colorComplite;
            LevelCompleted?.Invoke();
        }
    }

    public void SetScoreForBread(int reward)
    {
        _currentBread++;
        _currentScore += reward;
        ChangeScale(_score);
        _score.text = ScoreText + _currentScore;
        CollectedBread?.Invoke();
    }

    private void SetCompleteLevel()
    {
        _isComplete = true;
        SaveData();
    }

    private void SetScore(int kills)
    {
        if (kills < _bestCurrentKills)
        {
            ChangedScore?.Invoke();
        }

        _currentScore += _pointsMultiplier * kills;
        ChangeScale(_score);
        _score.text = ScoreText + _currentScore;
    }

    private void TrySetBestScore(int score)
    {
        if (_bestCurrentKills < score)
        {
            ChangeScale(_bestScore);
            _bestCurrentKills = score;
            _bestScore.text = BestScoreText + _bestCurrentKills;
            ChangedBestScore?.Invoke();
        }
    }

    private void ChangeScale(TMP_Text TMPtext)
    {
        TMPtext.rectTransform.DOScaleY(ScaleYMax, 0f);
        TMPtext.rectTransform.DOScaleY(1.0f, TimeChangeScale);
    }
}
