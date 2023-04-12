using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private ScoreStore _scoreStore;
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Button _buttonChangeSoundStatus;
    [SerializeField] private Image _buttonIcon;
    [SerializeField] private Sprite _imageOn;
    [SerializeField] private Sprite _imageOff;
    private bool _isSoundStatus;

    private const string MasterVolume = "Master";

    private void OnValidate()
    {
        _scoreStore = FindObjectOfType<ScoreStore>();
    }

    private void OnEnable()
    {
        if (_buttonChangeSoundStatus != null)
        {
            _buttonChangeSoundStatus.onClick.AddListener(ChangeSoundStatus);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _isSoundStatus = _scoreStore.GetSoundStatus();

        if (_isSoundStatus == false)
        {
            SetSoundStatus(_isSoundStatus);
        }
    }

    private void OnDisable()
    {
        if (_buttonChangeSoundStatus != null)
        {
            _buttonChangeSoundStatus.onClick.RemoveListener(ChangeSoundStatus);
        }
    }

    public void SetOffSound()
    {
        SetSoundStatus(false);
    }

    public void SetOnSound()
    {
        SetSoundStatus(_isSoundStatus);
    }

    private void SetSoundStatus(bool status)
    {
        if (status == true)
        {
            _audioMixer.audioMixer.SetFloat(MasterVolume, 0);

            if (_buttonIcon != null)
            {
                _buttonIcon.sprite = _imageOn;
            }
        }
        else
        {
            _audioMixer.audioMixer.SetFloat(MasterVolume, -80);

            if (_buttonIcon != null)
            {
                _buttonIcon.sprite = _imageOff;
            }
        }        
    }

    private void ChangeSoundStatus()
    {
        _isSoundStatus = !_isSoundStatus;
        _scoreStore.ChangeSound(_isSoundStatus);
        SetSoundStatus(_isSoundStatus);
    }
}
