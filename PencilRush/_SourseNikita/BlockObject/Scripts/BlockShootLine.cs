using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BlockShootLine : MonoBehaviour
{
    [SerializeField] private PencilColors _color;
    [SerializeField] private BlockShootTarget _shootTarget;
    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PencilColors>(out PencilColors color) && _isActive == true)
        {
            _isActive = false;

            if (color.TryGetComponent<PencilEffects>(out PencilEffects pencilEffects))
            {
                pencilEffects.SetedgePositionParent();
                pencilEffects.EnableEffects();
            }

            color.GetComponent<PencilShooter>().PrepareShoot(_shootTarget.transform);
        }
    }
}
