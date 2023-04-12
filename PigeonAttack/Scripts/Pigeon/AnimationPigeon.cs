
using UnityEngine;

public class AnimationPigeon : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AttackState _attackState;
    [SerializeField] private MoveState _moveState;
    [SerializeField] private DieState _dieState;
    [SerializeField] private CompleteState _compliteState;
    [SerializeField] private Rigidbody _rigidbody;

    private const string FlyAnim = "StartFly";
    private const string FinishFlyAnim = "StartFlyFinish";
    private const string StopFlyAnim = "StopFly";
    private const string MoveAnim = "Run";
    private const string SpeedFloat = "Speed";

    private void OnEnable()
    {
        _dieState.StartedDie += Die;
        _compliteState.Complited += CompliteAnimation;
    }

    private void Update()
    {
        _animator.SetFloat(SpeedFloat, _rigidbody.velocity.magnitude);
    }

    private void OnDisable()
    {
        _dieState.StartedDie -= Die;
        _compliteState.Complited -= CompliteAnimation;
    }

    private void FlyAnimation()
    {
        _animator.Play(FlyAnim);
    }

    private void StopFlyAnimation()
    {
        _animator.Play(StopFlyAnim);
    }

    private void MoveAnimation()
    {
        Debug.Log($"{gameObject} + Run");
        _animator.Play(MoveAnim);
    }

    private void CompliteAnimation()
    {
        _animator.Play(FinishFlyAnim);
    }

    private void Die(){}
}
