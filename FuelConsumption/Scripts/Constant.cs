using UnityEngine;

public class Constant : MonoBehaviour
{
    public float _PGD = 0.2f;
    public float _Kabina = 0.2f;
    public float _Bydka = 0.2f;
    public float _Generator = 0.2f;
    public float _RasxodToplivaNaKilometrLeto = 0.2f;
    public float _RasxodToplivaNaKilometrLetoPricep = 0.2f;
    public float _RasxodToplivaNaKilometrZima = 0.2f;
    public float _RasxodToplivaNaKilometrZimaPricep = 0.2f;

    private void Awake()
    {
        Application.targetFrameRate = 24;
    }
}
