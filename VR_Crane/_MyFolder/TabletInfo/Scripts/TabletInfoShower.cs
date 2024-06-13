using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace CraneGame
{
    public class TabletInfoShower : MonoBehaviour
    {
        [SerializeField] private CargoJoint _cargoJoint;
        [SerializeField] private SimpleAxisRotator _simpleAxisRotatorBase;
        [SerializeField] private SimpleAxisRotator _SimpleAxisRotatorBeam;
        [SerializeField] private SimpleAxisMover _simpleAxisMoverBeam1;
        [SerializeField] private SimpleAxisMover _simpleAxisMoverBeam2;
        [SerializeField] private SimpleAxisMover _simpleAxisMoverBeam3;
        [SerializeField] private HookMover _hookMover;
        [SerializeField] private SimpleMassTorqueCalculator _simpleMassTorqueCalculator;
        [SerializeField] private AudioSource _audioSourceAlarm;

        [SerializeField] private TMP_Text _textRotateBase;
        [SerializeField] private TMP_Text _textRotateBeam;
        [SerializeField] private TMP_Text _textMoverBeam1;
        [SerializeField] private TMP_Text _textMoverBeam2;
        [SerializeField] private TMP_Text _textMoverBeam3;
        [SerializeField] private TMP_Text _textHookMover;
        [SerializeField] private TMP_Text _textMass;
        [SerializeField] private Slider _slider;
        [SerializeField] private Sprite _emptyCargo;

        [SerializeField] private TMP_Text _cargoName;
        [SerializeField] private TMP_Text _cargoMass;
        [SerializeField] private TMP_Text _textHealth;
        [SerializeField] private Image _imageCargo;
        [SerializeField] private Image _imageSlider;

        [SerializeField] private Color _colorGreen;
        [SerializeField] private Color _colorRed;

        private Cargo currentCargo;

        private void OnEnable()
        {
            _cargoJoint.ConnectChanged += CargoSet;
        }

        private IEnumerator Start()
        {
            float baseRot;
            float beamRot;
            float beam1;
            float beam2;
            float beam3;
            float lenght;
            float mass;
            Color color;

            float startPosBeam = 35f;
            float lengthCorrectionBasedScale = 10f / 750f;
            float correctionMassScaleToPercent = 1.43f;

            while (true)
            {
                baseRot = Mathf.RoundToInt(_simpleAxisRotatorBase.PositionAxis);
                beamRot = startPosBeam + Mathf.RoundToInt(_SimpleAxisRotatorBeam.PositionAxis);
                beam1 = Mathf.RoundToInt(Mathf.Abs(_simpleAxisMoverBeam1.PositionAxis) * lengthCorrectionBasedScale);
                beam2 = Mathf.RoundToInt(Mathf.Abs(_simpleAxisMoverBeam2.PositionAxis) * lengthCorrectionBasedScale);
                beam3 = Mathf.RoundToInt(Mathf.Abs(_simpleAxisMoverBeam3.PositionAxis) * lengthCorrectionBasedScale);
                lenght = Mathf.RoundToInt(_hookMover.PositionAxis);
                mass = Mathf.Clamp(Mathf.RoundToInt(_simpleMassTorqueCalculator.Force * correctionMassScaleToPercent), 0, 100);
                _textRotateBase.text = baseRot.ToString() + "°";
                _textRotateBeam.text = beamRot.ToString() + "°";
                _textMoverBeam1.text = beam1.ToString() + "m";
                _textMoverBeam2.text = beam2.ToString() + "m";
                _textMoverBeam3.text = beam3.ToString() + "m";
                _textHookMover.text = lenght.ToString() + "m";
                _textMass.text = mass.ToString() + "%";
                color = Color.Lerp(_colorGreen, _colorRed, mass / 100);
                AudioAlarmSet(mass);
                _textMass.color = color;
                _slider.value = mass;
                _imageSlider.color = color;
                yield return null;
            }
        }

        private void CargoSet(Rigidbody? rigidbody)
        {
            if (rigidbody != null)
            {
                if (rigidbody.TryGetComponent<Cargo>(out Cargo cargo))
                {
                    currentCargo = cargo;
                    ShowCargoInfo(currentCargo);
                }
            }
            else
            {
                ShowCargoInfo(null);
            }
        }

        private void AudioAlarmSet(float force)
        {
            float minForceForAlarm = 70;

            if (force >= minForceForAlarm)
            {
                _audioSourceAlarm.mute = false;
            }
            else
            {
                _audioSourceAlarm.mute = true;
            }

            ChangePith(force);
        }

        private void ChangePith(float force)
        {
            float maxForceAlarm = 90;

            if(force >= maxForceAlarm)
            {
                _audioSourceAlarm.pitch = 3;
            }
            else
            {
                _audioSourceAlarm.pitch = 1;
            }
        }

        private void ShowCargoInfo(Cargo cargo)
        {
            if (cargo != null)
            {
                currentCargo.HealthChanged += ChangeHealth;
                _cargoName.text = cargo.GetCargoName();
                _cargoMass.text = cargo.GetMass().ToString() + "t";
                _textHealth.text = cargo.GetHealth().ToString() + "%";
                _imageCargo.sprite = cargo.GetImageName();
            }
            else
            {
                if (currentCargo != null)
                {
                    currentCargo.HealthChanged -= ChangeHealth;
                }
                _cargoName.text = null;
                _cargoMass.text = null;
                _imageCargo.sprite = _emptyCargo;
            }
        }

        private void ChangeHealth(int health)
        {
            _textHealth.text = health.ToString() + "%";
        }
    }
}
