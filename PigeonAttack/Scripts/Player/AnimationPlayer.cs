using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    private const string AnimDance = "Dance";

    private void OnEnable()
    {
        _player.Won += DanceAnimation;
    }

    private void OnDisable()
    {
        _player.Won -= DanceAnimation;
    }

    private void DanceAnimation()
    {
        _animator.Play(AnimDance);
    }
}
