using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RagDollBiker : MonoBehaviour
{
    [SerializeField] private IKSetting _iKSettings;
    [SerializeField] private Animator _animatorBiker;
    [SerializeField] private Transform _rootPositionTarget;
    [SerializeField] private Transform _splineTarget;
    [SerializeField] private Vector3 _splineTargetOffset = new Vector3(0, 3, 3);
    [SerializeField] private Rigidbody _rigidbodyRagDollBike;
    [SerializeField] private Rigidbody _rigidbodyRagDollRider;

    [System.Serializable]
    public class IKSetting
    {
        public IKPoints IKPoints;
        public float HandSeperation = 0.34f;
        public Vector3 HandRoll;
        public float LegSeperation = 0.24f;
        public Vector3 FootRoll;

    }
    [System.Serializable]
    public class IKPoints
    {
        public Transform rightHand, leftHand;
        public Transform rightFoot, leftFoot;
    }

    private void OnEnable()
    {
        _animatorBiker.enabled = true;
        _rigidbodyRagDollRider.velocity = _rigidbodyRagDollBike.velocity;
    }

    private void OnAnimatorIK()//void SetPosition()
    {
        //AvatarIKHint.RightElbow = 
        //set ik pos
        _animatorBiker.SetIKPosition(AvatarIKGoal.RightHand, _iKSettings.IKPoints.rightHand.position);
        _animatorBiker.SetIKRotation(AvatarIKGoal.RightHand, _iKSettings.IKPoints.rightHand.rotation);
        _animatorBiker.SetIKHintPosition(AvatarIKHint.RightElbow, _iKSettings.IKPoints.rightHand.position - Vector3.up * 1f);

        _animatorBiker.SetIKPosition(AvatarIKGoal.LeftHand, _iKSettings.IKPoints.leftHand.position);
        _animatorBiker.SetIKRotation(AvatarIKGoal.LeftHand, _iKSettings.IKPoints.leftHand.rotation);
        _animatorBiker.SetIKHintPosition(AvatarIKHint.LeftElbow, _iKSettings.IKPoints.leftHand.position - Vector3.up * 1f);

        _animatorBiker.SetIKPosition(AvatarIKGoal.RightFoot, _iKSettings.IKPoints.rightFoot.position);
        _animatorBiker.SetIKRotation(AvatarIKGoal.RightFoot, _iKSettings.IKPoints.rightFoot.rotation);

        _animatorBiker.SetIKPosition(AvatarIKGoal.LeftFoot, _iKSettings.IKPoints.leftFoot.position);
        _animatorBiker.SetIKRotation(AvatarIKGoal.LeftFoot, _iKSettings.IKPoints.leftFoot.rotation);

        //set weight
        _animatorBiker.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        _animatorBiker.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        _animatorBiker.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        _animatorBiker.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        _animatorBiker.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);
        _animatorBiker.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);

        _animatorBiker.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
        _animatorBiker.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);
        Invoke(nameof(TurnOffAnimator), 0.1f);
    }

    private void TurnOffAnimator()
    {
        _animatorBiker.enabled = false;
    }
}
