using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseChanger : MonoBehaviour
{
    [SerializeField] private Animator _animatorHand;
    [SerializeField] private AnimationClip _point;
    [SerializeField] private AnimationClip _bigUp;
    [SerializeField] private AnimationClip _take;
    [SerializeField] private AnimationClip _default;

    [SerializeField] private const string PointHand = "Point";
    [SerializeField] private const string BigUpHand = "BigUp";
    [SerializeField] private const string TakeHand = "Take";
    [SerializeField] private const string DefaultHand = "Default";

    public void SetPointPose()
    {
        _animatorHand.SetTrigger(PointHand);
        //_animatorHand.Play(Point.name);
    }

    public void SetBigUpPose()
    {
        _animatorHand.SetTrigger(BigUpHand);
        //_animatorHand.Play(BigUp.name);
    }

    public void SetTakePose()
    {
        _animatorHand.SetTrigger(TakeHand);
        //_animatorHand.Play(Take.name);
    }

    public void SetDefaultPose()
    {
        _animatorHand.SetTrigger(DefaultHand);
        //_animatorHand.Play(Default.name);
    }
}
