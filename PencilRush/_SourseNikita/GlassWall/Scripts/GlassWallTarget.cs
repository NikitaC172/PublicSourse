using UnityEngine;

public class GlassWallTarget : MonoBehaviour
{
    [SerializeField] private ColorsObject _color = ColorsObject.Blue;
    [SerializeField] private float _timeToDeactivation = 0.1f;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private GlassWallPartsObject _glassWallPartsObject;
    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PencilUpgrader>(out PencilUpgrader pencilUpgrader) && _isActive == true)
        {
            if ((int)pencilUpgrader.ColorPencil == (int)_color)
            {
                _isActive = false;

                if (pencilUpgrader.TryGetComponent<PencilEffects>(out PencilEffects pencilEffects))
                {
                    pencilEffects.ChangeTimeLifeTrail();
                    pencilEffects.ReturnParent();
                    pencilEffects.DisableObjectModel();
                }

                _meshRenderer.enabled = false;
                _glassWallPartsObject.gameObject.SetActive(true);
                Invoke(nameof(DeactivateColiders), _timeToDeactivation);
            }
        }
    }

    private void DeactivateColiders()
    {
        gameObject.SetActive(false);
    }

    private enum ColorsObject
    {
        Blue = 0,
        Red = 1,
        Green = 2,
        Purple = 3,
        Yellow = 4,
        Pink = 5,
        Orange = 6,
    }
}
