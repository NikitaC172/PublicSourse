using UnityEngine;

public class LinePencilPicker : MonoBehaviour
{
    [SerializeField] private LinePositionSetter _positionSetter;
    [SerializeField] private LinePickerPoint _pointStartMoveForPencil;
    [SerializeField] private int numberLine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PencilMoverToLinePosition>(out PencilMoverToLinePosition _moverLinePosition))
        {
            if (_moverLinePosition.GetState())
            {
                _moverLinePosition.MoveToPosition(_pointStartMoveForPencil, _positionSetter.GetPosition(_moverLinePosition.gameObject.GetComponent<Pencil>()), _positionSetter.GetPositionObjectParent(), _positionSetter, numberLine);
            }
        }
    }
}
