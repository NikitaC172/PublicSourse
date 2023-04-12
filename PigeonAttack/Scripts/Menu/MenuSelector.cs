using UnityEngine;
using UnityEngine.UI;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _start;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _scrollViewLevels;
    [SerializeField] private Button _play;
    [SerializeField] private Button _exit;

    private void OnEnable()
    {
        _play.onClick.AddListener(OnClickPlay);
        _exit.onClick.AddListener(OnClickExit);
    }

    private void OnDisable()
    {
        _play.onClick.RemoveListener(OnClickPlay);
        _exit.onClick.RemoveListener(OnClickExit);
    }

    private void OnClickPlay()
    {
        _scrollViewLevels.SetActive(true);
        _audioSource.PlayOneShot(_start);
    }

    private void OnClickExit()
    {

    }
}
