using System.Collections;
using UnityEngine;

public class PencilMoverToLinePosition : MonoBehaviour
{
    [SerializeField] private float _timeMove = 1.0f;
    [SerializeField] private float _timeWait = 0.5f;
    [SerializeField] private PencilAnimator _pencilAnimator;
    [SerializeField] private PencilEffectsObject _pencilEffects;
    [SerializeField] private PencilChangerSize _changerSize;
    [SerializeField] private PencilAudioPlayer _audioPlayer;
    [SerializeField] private PencilDroper _pencilDroper;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PencilObjectParticleUpgradeEffects _upgradeEffects;

    private bool _isStay = true;

    private const int NumberLineWithoutChange = 0;
    private const int NumberLineWithEffects = 1;

    private void Start()
    {
        if(gameObject.transform.localEulerAngles == Vector3.zero)
        {
            _upgradeEffects.gameObject.transform.localEulerAngles = Vector3.zero;
        }
    }

    public bool GetState()
    {
        return _isStay;
    }

    public void ChangePositionOnLine(Vector3 newPosition)
    {
        StartCoroutine(Move(newPosition));
    }

    public void MoveToPosition(Vector3? positionSetter, LinePositionSetter linePositionSetter, int numberLine)
    {
        if (positionSetter != null)
        {
            _isStay = false;
            _timeWait = 0f;
            _upgradeEffects.ResetPosition();
            transform.parent = linePositionSetter.transform;
            _pencilDroper.TakeLinePositionSetter(linePositionSetter);
            Vector3 position = (Vector3)positionSetter;
            _audioPlayer.PlayTakeSoundFromPencilCase();
            StartCoroutine(Move(position, numberLine));
        }

    }

    public void MoveToPosition(LinePickerPoint pickerPoint, Vector3? positionSetter, LinePositionObject linePosition, LinePositionSetter linePositionSetter, int numberLine)
    {
        _isStay = false;
        ResetRigidbody();
        _upgradeEffects.ResetPosition();

        if (positionSetter != null)
        {
            _pencilDroper.TakeLinePositionSetter(linePositionSetter);
            gameObject.transform.SetParent(pickerPoint.gameObject.transform);
            gameObject.transform.localPosition = pickerPoint.transform.localPosition;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.SetParent(linePosition.gameObject.transform);
            Vector3 position = (Vector3)positionSetter;
            StartCoroutine(Move(position, numberLine));
            _pencilAnimator.DownPositionAnim();
            TrySetEffectsAndSize();
        }
        else
        {
            _audioPlayer.PlayTakeSound();
            gameObject.SetActive(false);
        }
    }

    private void TrySetEffectsAndSize()
    {
        if (_changerSize.IsNewPoolObject == false)
        {
            _changerSize.StartChanger();
            _audioPlayer.PlayTakeSound();
        }
    }

    private void ResetRigidbody()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    private IEnumerator Move(Vector3 positionSetter, int numberLine = NumberLineWithoutChange)
    {
        float time = 0;
        float delayforEffects = 0.5f;

        while (time < _timeWait)
        {
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localEulerAngles = Vector3.zero;
        gameObject.GetComponent<PencilIncliner>().enabled = true;
        time = 0;

        while (time < _timeMove && _pencilDroper.IsDroped == false)
        {
            time += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, positionSetter, time / _timeMove);

            if (time / _timeMove > delayforEffects && numberLine == NumberLineWithEffects && _changerSize.IsNewPoolObject == false)
            {
                _pencilEffects.gameObject.SetActive(true);
            }

            yield return null;
        }
    }
}
