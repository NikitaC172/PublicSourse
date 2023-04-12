using System.Collections.Generic;
using UnityEngine;

public class LineSystemPencilsCount : MonoBehaviour
{
    [SerializeField] private List<LinePositionSetter> _linePositionSetters;


    private List<Pencil> _pencils = new List<Pencil>();
    private bool _isEmpty = false;

    public List<Pencil> Pencils => _pencils;
    public bool IsEmpty => _isEmpty;

    private void OnEnable()
    {
        foreach (LinePositionSetter linePositionSetter in _linePositionSetters)
        {
            linePositionSetter.AddedPencil += AddPencil;
            linePositionSetter.RemovedPencil += RemovePencil;
        }
    }

    public void RemoveDroppedPencil(Pencil pencil, bool isLeftBorder)
    {
        pencil.GetComponent<PencilDroper>().FallOverBorder(isLeftBorder);
    }

    private void AddPencil(Pencil pencil)
    {
        _pencils.Add(pencil);
        _isEmpty = false;
    }

    private void RemovePencil(Pencil pencil)
    {
        _pencils.Remove(pencil);

        if (_pencils.Count == 0)
        {
            _isEmpty = true;
        }
    }
}
