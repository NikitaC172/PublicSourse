using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using AdvancedInputFieldPlugin;

public class Calculate : MonoBehaviour
{
    [SerializeField] private Constant _constant;
    [SerializeField] private Button _buttonResult;

    [SerializeField] private AdvancedInputField _inputFieldOdometr;
    [SerializeField] private AdvancedInputField _inputFieldOstatok;
    [SerializeField] private AdvancedInputField _inputFieldZapravil;
    [SerializeField] private AdvancedInputField _inputFieldPGD;
    [SerializeField] private AdvancedInputField _inputFieldAvKabina;
    [SerializeField] private AdvancedInputField _inputFieldAVBydka;
    [SerializeField] private AdvancedInputField _inputFieldGenerator;
    [SerializeField] private AdvancedInputField _inputFieldOdometrExit;

    [SerializeField] private TMP_Text _outputLeto;
    [SerializeField] private TMP_Text _outputLetoPricep;
    [SerializeField] private TMP_Text _outputZima;
    [SerializeField] private TMP_Text _outputZimaPricep;

    private float _odometr;
    private float _ostatok;
    private float _zapravil;
    private float _PGD;
    private float _AvKabina;
    private float _AVBydka;
    private float _Generator;
    private float _odometrExit;

    public Action Resulted;

    private void OnEnable()
    {
        _buttonResult.onClick.AddListener(Check);
    }

    private void OnDisable()
    {
        _buttonResult.onClick.RemoveListener(Check);
    }

    private void Check()
    {
        bool isCorrectedOdometr = float.TryParse(_inputFieldOdometr.Text.Replace('.', ','), out _odometr);
        bool isCorrectedOstatok = float.TryParse(_inputFieldOstatok.Text.Replace('.', ','), out _ostatok);
        bool isCorrectedZapravil = float.TryParse(_inputFieldZapravil.Text.Replace('.', ','), out _zapravil);
        bool isCorrectedPGD = float.TryParse(_inputFieldPGD.Text.Replace('.', ','), out _PGD);
        bool isCorrectedAvKabina = float.TryParse(_inputFieldAvKabina.Text.Replace('.', ','), out _AvKabina);
        bool isCorrectedAVBydka = float.TryParse(_inputFieldAVBydka.Text.Replace('.', ','), out _AVBydka);
        bool isCorrectedGenerator = float.TryParse(_inputFieldGenerator.Text.Replace('.', ','), out _Generator);
        bool isCorrectedOdometrExit = float.TryParse(_inputFieldOdometrExit.Text.Replace('.', ','), out _odometrExit);

        if(isCorrectedOdometr && isCorrectedOstatok && isCorrectedZapravil && isCorrectedPGD && isCorrectedAvKabina && isCorrectedAVBydka && isCorrectedGenerator && isCorrectedOdometrExit)
        {
            CalculateFuel();
        }
    }

    private void CalculateFuel()
    {
        float deltaOdometr = _odometrExit - _odometr;
        float totalTopliva = _zapravil + _ostatok;
        float ostatokLeto = totalTopliva - (deltaOdometr * _constant._RasxodToplivaNaKilometrLeto) - (_PGD * _constant._PGD) - (_AvKabina * _constant._Kabina) - (_AVBydka * _constant._Bydka) - (_Generator * _constant._Generator);
        float ostatokLetoPricep = totalTopliva - (deltaOdometr * _constant._RasxodToplivaNaKilometrLetoPricep) - (_PGD * _constant._PGD) - (_AvKabina * _constant._Kabina) - (_AVBydka * _constant._Bydka) - (_Generator * _constant._Generator);
        float ostatokZima = totalTopliva - (deltaOdometr * _constant._RasxodToplivaNaKilometrZima) - (_PGD * _constant._PGD) - (_AvKabina * _constant._Kabina) - (_AVBydka * _constant._Bydka) - (_Generator * _constant._Generator); ;
        float ostatokZimaPricep = totalTopliva - (deltaOdometr * _constant._RasxodToplivaNaKilometrZimaPricep) - (_PGD * _constant._PGD) - (_AvKabina * _constant._Kabina) - (_AVBydka * _constant._Bydka) - (_Generator * _constant._Generator);
        OutputResult(ostatokLeto, ostatokLetoPricep, ostatokZima, ostatokZimaPricep);
    }

    private void OutputResult(float ostatokLeto,  float ostatokLetoPricep, float ostatokZima, float ostatokZimaPricep)
    {
        _outputLeto.text = Math.Round(ostatokLeto, 1).ToString();
        _outputLetoPricep.text = Math.Round(ostatokLetoPricep, 1).ToString();
        _outputZima.text = Math.Round(ostatokZima, 1).ToString();
        _outputZimaPricep.text = Math.Round(ostatokZimaPricep, 1).ToString();
        Resulted?.Invoke();
    }
}
