using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineSystemPickerSetter))]
public class LineSystemAdderPencil : MonoBehaviour
{
    [SerializeField] private List<LinePositionSetter> _linePositionSetters;
    [SerializeField] private LineSystemPickerSetter _lineSystemPickerSetter;
    [SerializeField] private CountUpgrade _countUpgrade;
    [SerializeField] private PoolObject _poolObject;
    [SerializeField] private int _startPencilsCount = 2;

    private int _pencilCount;

    private void OnValidate()
    {
        _countUpgrade = FindObjectOfType<CountUpgrade>(true);
        _poolObject = FindObjectOfType<PoolObject>();
        _lineSystemPickerSetter = GetComponent<LineSystemPickerSetter>();
    }

    private void OnEnable()
    {
        _countUpgrade.IncreasedLevel += AddPencilLevelUp;
    }

    private void Start()
    {
        SetPencilCount();

        for (int i = 0; i < _pencilCount; i++)
        {
            AddPencil();
        }
    }

    private void OnDisable()
    {
        _countUpgrade.IncreasedLevel -= AddPencilLevelUp;
    }

    public void AddPencilLevelUp()
    {
        AddPencil();
    }

    private void SetPencilCount()
    {
        _pencilCount = _startPencilsCount + SaveSystem.Upgrader.CountUpgradeLevel;
    }

    public void AddPencilFromPencilCase(Pencil pencil)
    {
        for (int i = 0; i < _linePositionSetters.Count; i++)
        {
            if (_linePositionSetters[i].IsFull == false)
            {
                if (pencil.TryGetComponent<PencilMoverToLinePosition>(out PencilMoverToLinePosition pencilMoverToLinePosition))
                {
                    pencil.TryGetComponent<PencilChangerSize>(out PencilChangerSize pencilChangerSize);
                    pencilChangerSize.SetNewPoolObject(true);
                    pencilMoverToLinePosition.MoveToPosition(_linePositionSetters[i].GetPosition(pencil), _linePositionSetters[i], i);
                    return;
                }
            }
        }
    }

    private void AddPencil()
    {
        Pencil pencil = _poolObject.TrygetPencil();

        if (pencil != null)
        {
            pencil.TryGetComponent<PencilChangerSize>(out PencilChangerSize pencilChangerSize);
            pencilChangerSize.SetNewPoolObject(true);
            pencil.transform.SetParent(_lineSystemPickerSetter.transform);
            pencil.transform.localPosition = Vector3.zero;
        }
    }
}
