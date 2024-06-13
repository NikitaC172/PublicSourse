using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class AudioPlayerEngine : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<Arm> _arms;
        [SerializeField] private float _deltaTimeVolumeChange = 0.1f;
        [SerializeField] private float _maxVolume = 1.0f;
        [SerializeField] private float _minVolume = 0.0f;

        private float _maxAngle = 0;
        private List<float> _listAngls;
        float quaternionCorrector = 0.07f;

        private void FixedUpdate()
        {
            _audioSource.volume = Mathf.Clamp(Mathf.Lerp(_audioSource.volume, FindMaxInclineArm(), _deltaTimeVolumeChange), _minVolume, _maxVolume);
        }

        private void CreateList()
        {
            _listAngls = new List<float>();

            foreach (Arm arm in _arms)
            {
                _listAngls.Add(Mathf.Abs(arm.transform.localRotation.z));
            }
        }

        private float FindMaxInclineArm()
        {
            CreateList();
            _maxAngle = (Mathf.Max(_listAngls.ToArray()))/ quaternionCorrector;
            return _maxAngle;
        }
    }
}
