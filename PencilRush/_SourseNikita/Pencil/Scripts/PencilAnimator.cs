using UnityEngine;

public class PencilAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animatorPencil;

    private const string Idle = "Idle";
    private const string StandUp = "StandUp";
    private const string StandDown = "StandDown";
    private const string StandDownWithMove = "StandDownWithMove";
    private const string DownPosition = "DownPosition";

    public void IdleAnim()
    {
        _animatorPencil.Play(Idle);
    }

    public void StandUpAnim()
    {
        _animatorPencil.Play(StandUp);
    }

    public void StandDownAnim()
    {        
        _animatorPencil.Play(StandDown);
    }

    public void StandDownWithMoveAnim()
    {
        _animatorPencil.Play(StandDownWithMove);
    }

    public void DownPositionAnim()
    {     
        _animatorPencil.Play(DownPosition);
    }
}
