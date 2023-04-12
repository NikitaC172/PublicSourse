using System;
using System.Collections;
using UnityEngine;

public class PackageAnimator : MonoBehaviour
{
    [SerializeField] private Animator _packageAnimator;
    [SerializeField] private Animator _ropeAnimator;
    [SerializeField] private float _delayBetweenRope = 1.0f;
    [SerializeField] private float _delayBetweenPackage = 3.0f;

    private const string PackageOpenAnim = "OpenPackage";

    private const string RopeDownOpenAnim = "OpenRopeDown";
    private const string RopeUpOpenAnim = "OpenRopeUp";
    private const string RopeLeftOpenAnim = "OpenRopeLeft";
    private const string RopeRightOpenAnim = "OpenRopeRight";

    public Action Opened;

    public void Open()
    {
        StartCoroutine(OpenPackage());
    }

    private IEnumerator OpenPackage()
    {
        var waitRope = new WaitForSeconds(_delayBetweenRope);
        var waitPackage = new WaitForSeconds(_delayBetweenPackage);

        _ropeAnimator.Play(RopeRightOpenAnim);
        yield return waitRope;

        _ropeAnimator.Play(RopeLeftOpenAnim);
        yield return waitRope;

        _ropeAnimator.Play(RopeUpOpenAnim);
        yield return waitRope;

        _ropeAnimator.Play(RopeDownOpenAnim);
        yield return waitRope;

        _packageAnimator.Play(PackageOpenAnim);
        yield return waitPackage;
        Opened?.Invoke();
    }
}
