using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private ScoreStore _scoreStore;
    [SerializeField] private Sprite _labelSpriteImage;
    [SerializeField] private Sprite _lockImage;
    [SerializeField] private Image _labelImage;
    [SerializeField] private string _labelNameTitle;
    [SerializeField] private string _levelNameForLoad;
    [SerializeField] private int _slotForSaveGame;
    [SerializeField] private Text _bestKills;
    [SerializeField] private Text _labelName;
    [SerializeField] private Text _score;
    [SerializeField] private List<GameObject> _breads;
    [SerializeField] private GameObject _panelMain;
    [SerializeField] private Button _buttonChoise;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _source;

    private bool _isActive;
    private int _breadCount;

    public event UnityAction<string> LevelSelected;

    private void OnEnable()
    {
        _isActive = _scoreStore.GetLevelStatus(_slotForSaveGame);
        _labelName.text = _labelNameTitle;

        if (_isActive == true)
        {
            _panelMain.SetActive(true);
            _buttonChoise.onClick.AddListener(SelectLevel);
            _bestKills.text = _scoreStore.GetBestKillsScore(_slotForSaveGame).ToString();
            _score.text = _scoreStore.GetScore(_slotForSaveGame).ToString();
            _breadCount = _scoreStore.GetBreadCount(_slotForSaveGame);
            _labelImage.sprite = _labelSpriteImage;

            for (int i = 0; i < _breadCount; i++)
            {
                _breads[i].SetActive(true);
            }
        }
        else
        {
            _panelMain.SetActive(false);
            _labelImage.sprite = _lockImage;
        }

    }

    private void OnDisable()
    {
        if (_isActive == true)
        {
            _buttonChoise.onClick.RemoveListener(SelectLevel);
        }
    }

    private void SelectLevel()
    {
        _source.PlayOneShot(_clip);
        LevelSelected?.Invoke(_levelNameForLoad);
        SceneManager.LoadScene(_levelNameForLoad);
    }
}
