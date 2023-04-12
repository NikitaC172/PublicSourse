using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private bool _isObjectBreaks = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PencilDroper>(out PencilDroper pencilDroper))
        {
            pencilDroper.Drop(gameObject.transform, _isObjectBreaks);
        }
    }
}
