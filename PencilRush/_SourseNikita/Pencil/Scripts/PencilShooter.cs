using System.Collections;
using UnityEngine;

public class PencilShooter : MonoBehaviour
{
    [SerializeField] private PencilDroper _pencilDroper;
    [SerializeField] private PencilAnimator _pencilAnimator;
    [SerializeField] private PoolObject _poolObject;
    [SerializeField] private PencilIncliner _pencilIncliner;
    [SerializeField] private PencilAudioPlayer _pencilAudioPlayer;
    [SerializeField] private float _liftingHeight = 1.0f;
    [SerializeField] private float _timeToMoveUp = 0.5f;
    [SerializeField] private float _timeToMoveTarget = 0.3f;
    [SerializeField] private float _timeToMoveUpInFinal = 2.0f;
    [SerializeField] private float _timeToMoveTargetInFinal = 1.5f;

    private bool _isFinal = false;
    private Transform _target;

    private void OnValidate()
    {
        _poolObject = FindObjectOfType<PoolObject>();
    }

    public void ChangeTimeForFinal()
    {
        _timeToMoveUp = _timeToMoveUpInFinal;
        _timeToMoveTarget = _timeToMoveTargetInFinal;
        _isFinal = true;
    }

    public void PrepareShoot(Transform target)
    {
        _target = target;
        _pencilDroper.RemovePencilInLine();
        transform.parent = _poolObject.transform;
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        float time = 0;
        Vector3 upPosition = transform.localPosition + new Vector3(0, _liftingHeight, 0);
        _pencilIncliner.StopIncline();

        if (_isFinal)
        {
            _pencilAnimator.StandDownAnim();
        }
        else
        {
            _pencilAnimator.StandDownWithMoveAnim();
        }

        while (time < _timeToMoveUp)
        {
            time += Time.deltaTime;
            transform.LookAt(_target);
            transform.localPosition = Vector3.Lerp(transform.localPosition, upPosition, time / _timeToMoveUp);
            yield return null;
        }

        time = 0;

        if (_isFinal == true)
        {
            PlaySoundPaint();
        }

        while (time < _timeToMoveTarget)
        {
            time += Time.deltaTime;
            transform.LookAt(_target);
            transform.position = Vector3.Lerp(transform.position, _target.position, time / _timeToMoveTarget);
            yield return null;
        }
    }

    private void PlaySoundPaint()
    {
        _pencilAudioPlayer.PlayPaintSound();
    }
}
