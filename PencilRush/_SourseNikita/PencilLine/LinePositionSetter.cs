using System;
using System.Collections.Generic;
using UnityEngine;

public class LinePositionSetter : MonoBehaviour
{
    [SerializeField] private LinePositionObject _positionObject;
    [SerializeField] private LineObjectPencilPicker _objectPencilPicker;
    [SerializeField] private float _stepBetweenPencil = 0.75f;
    [SerializeField] private int _countPencileOnSide = 7;
    [SerializeField] private float _delayResetPosition = 1.0f;    

    private Pencil _pencilCentrPosition;
    private List<Pencil> _pencilLeftSidePosition = new List<Pencil>();
    private List<Pencil> _pencilRightSidePosition = new List<Pencil>();
    private bool _isFull = false;

    public Action<LinePositionSetter, bool> Fulled;
    public Action<Pencil> AddedPencil;
    public Action<Pencil> RemovedPencil;

    public bool IsFull  => _isFull;

    public LinePositionObject GetPositionObjectParent()
    {
        return _positionObject;
    }

    public void RemovePencile(Pencil pencil)
    {
        RemovedPencil.Invoke(pencil);

        if (_pencilLeftSidePosition.Remove(pencil))
        {
            Invoke(nameof(ResetPositionLeft), _delayResetPosition);
        }
        else if (_pencilRightSidePosition.Remove(pencil)) 
        {
            Invoke(nameof(ResetPositionRight), _delayResetPosition);
        }
        else if (_pencilCentrPosition == pencil)
        {
            _pencilCentrPosition = null;
        }

        _isFull = false;
        Fulled.Invoke(this, _isFull);
    }

    public void ResetPositionRight()
    {
        foreach (Pencil pencil in _pencilRightSidePosition)
        {
            PencilMoverToLinePosition pencilMover = pencil.GetComponent<PencilMoverToLinePosition>();
            pencilMover.ChangePositionOnLine(new Vector3((_pencilRightSidePosition.LastIndexOf(pencil) + 1) * _stepBetweenPencil, 0, 0));
        }
    }

    public void ResetPositionLeft()
    {
        int leftSideX = -1;

        foreach (Pencil pencil in _pencilLeftSidePosition)
        {
            PencilMoverToLinePosition pencilMover = pencil.GetComponent<PencilMoverToLinePosition>();
            pencilMover.ChangePositionOnLine(new Vector3((_pencilLeftSidePosition.LastIndexOf(pencil) + 1) * _stepBetweenPencil * leftSideX, 0, 0));
        }
    }

    public Vector3? GetPosition(Pencil pencil)
    {
        if (_pencilCentrPosition == null)
        {
            AddedPencil.Invoke(pencil);
            _pencilCentrPosition = pencil;
            return new Vector3(0, 0, 0);
        }
        else if (_pencilLeftSidePosition.Count > _pencilRightSidePosition.Count)
        {
            if (_pencilRightSidePosition.Count < _countPencileOnSide)
            {
                _pencilRightSidePosition.Add(pencil);
                AddedPencil.Invoke(pencil);
                FillingCheck();
                return new Vector3((_pencilRightSidePosition.LastIndexOf(pencil) + 1) * _stepBetweenPencil, 0, 0);
            }
            else
            {
                FillingCheck();
                return null;
            }
        }
        else
        {
            int leftSideX = -1;

            if (_pencilLeftSidePosition.Count < _countPencileOnSide)
            {
                _pencilLeftSidePosition.Add(pencil);
                AddedPencil.Invoke(pencil);
                FillingCheck();
                return new Vector3((_pencilLeftSidePosition.LastIndexOf(pencil) + 1) * _stepBetweenPencil * leftSideX, 0, 0);
            }
            else
            {
                FillingCheck();
                return null;
            }
        }
    }

    private void FillingCheck()
    {
        if (_pencilCentrPosition != null && _pencilRightSidePosition.Count == _countPencileOnSide && _pencilLeftSidePosition.Count == _countPencileOnSide)
        {
            _isFull = true;
            Fulled.Invoke(this, _isFull);
        }
    }
}
