using System.Collections;
using UnityEngine;

public class PencilChangerSize : MonoBehaviour
{
    [SerializeField] private PencilObjectMiddleScaleJoin _objectMiddle;
    [SerializeField] private PencilObjectUp _objectUp;
    [SerializeField] private PencilUpgrader _pencilUpgrader;
    [SerializeField] private PowerUpgrade _powerUpgrade;
    [SerializeField] private PaintUpgrade _paintUpgrade;
    [SerializeField] private float _minScale = 0.2f;
    [SerializeField] private float _timeSecondsToMinScale = 20.0f;
    [SerializeField] private float _multiplicatorTime = 1.0f;
    [SerializeField] private float _epicPencilBonusPaint = 0.15f;
    [SerializeField] private float _stepMultiplicatorTime = 0.05f;
    [SerializeField] private float _stepPencilPercentMultiplicator = 0.025f;

    private Vector3 _currentScaleMiddleObject;
    private Vector3 _currentPositionUpObject;

    private bool _isNewPoolObject = false;
    private bool _isActive = true;
    private float _pencilPercent = 1.0f;
    private float _pencilPercentMultiplicator;

    public float PencilPercent => _pencilPercent;
    public bool IsNewPoolObject => _isNewPoolObject;

    private void OnValidate()
    {
        _powerUpgrade = FindObjectOfType<PowerUpgrade>(true);
        _paintUpgrade = FindObjectOfType<PaintUpgrade>(true);
    }

    private void Awake()
    {
        SetMultiplicator();
        SetPencilPercentMultiplicator();
        CheckUpgrade();
        _currentScaleMiddleObject = _objectMiddle.gameObject.transform.localScale;
        _currentPositionUpObject = _objectUp.gameObject.transform.localPosition;
    }

    private void OnEnable()
    {
        _powerUpgrade.ChangedPower += SetMultiplicator;
        _paintUpgrade.ChangedPaint += SetPencilPercentMultiplicator;
    }

    private void OnDisable()
    {
        _powerUpgrade.ChangedPower -= SetMultiplicator;
        _paintUpgrade.ChangedPaint -= SetPencilPercentMultiplicator;
    }

    public void StartChanger()
    {
        if (_isNewPoolObject == false)
        {
            StartCoroutine(ChangeSize());
        }
    }

    public void StopChanger()
    {
        _isActive = false;
    }

    public void SetNewPoolObject(bool isNewObject)
    {
        _isNewPoolObject = isNewObject;
    }

    private void CheckUpgrade()
    {
        if(_pencilUpgrader.IsUpgrade == false)
        {
            _epicPencilBonusPaint = 0.0f;
        }
    }

    private void SetMultiplicator()
    {
        float defaultMultiplicatorTime = 1.0f;
        _multiplicatorTime = defaultMultiplicatorTime + _stepMultiplicatorTime * SaveSystem.Upgrader.PowerUpgradeLevel;
    }

    private void SetPencilPercentMultiplicator()
    {
        _pencilPercentMultiplicator = _stepPencilPercentMultiplicator * SaveSystem.Upgrader.PaintUpgradeLevel;
    }

    private IEnumerator ChangeSize()
    {
        float time = 0;
        float DecimalTranslation = 10;
        Vector3 middleScale = _currentScaleMiddleObject;

        while (time < _timeSecondsToMinScale * _multiplicatorTime && _isActive)
        {
            time += Time.deltaTime;
            _currentScaleMiddleObject = Vector3.Lerp(middleScale, new Vector3(1, _minScale, 1), time / (_timeSecondsToMinScale * _multiplicatorTime));
            _objectMiddle.transform.localScale = _currentScaleMiddleObject;
            float deltapositionY = (1 - _currentScaleMiddleObject.y) * DecimalTranslation;
            _objectUp.transform.localPosition = new Vector3(0, _currentPositionUpObject.y - deltapositionY, 0);
            _pencilPercent = _currentScaleMiddleObject.y + _pencilPercentMultiplicator + _epicPencilBonusPaint;
            yield return null;
        }
    }
}
