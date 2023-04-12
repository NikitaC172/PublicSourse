using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BlockShootTarget : MonoBehaviour
{
    [SerializeField] private PencilColors _color;
    [SerializeField] private float _timeToDeactivation = 1.5f;
    [SerializeField] private Collider _colliderTriger;
    [SerializeField] private Collider _colliderBlock;

    private PencilColors _colorPencil;

    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PencilColors>(out PencilColors color) && _isActive == true)
        {
            _isActive = false;
            _colorPencil = color;

            if (_colorPencil.TryGetComponent<PencilEffects>(out PencilEffects pencilEffects))
            {
                pencilEffects.ChangeTimeLifeTrail();
                pencilEffects.ReturnParent();
                pencilEffects.DisableObjectModel();
            }

            Invoke(nameof(DeactivateColiders), _timeToDeactivation);
        }
    }

    private void DeactivateColiders()
    {
        _colliderTriger.enabled = false;
        _colliderBlock.enabled = false;
        gameObject.SetActive(false);
    }
}
