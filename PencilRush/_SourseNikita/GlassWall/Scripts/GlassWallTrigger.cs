using UnityEngine;

public class GlassWallTrigger : MonoBehaviour
{
    [SerializeField] private ColorsObject _color = ColorsObject.Blue;
    [SerializeField] private GlassWallTarget _shootTarget;

    private bool _isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PencilUpgrader>(out PencilUpgrader pencilUpgrader))
        {
            if ((int)pencilUpgrader.ColorPencil == (int)_color && _isActive == true)
            {
                _isActive = false;

                if (pencilUpgrader.TryGetComponent<PencilEffects>(out PencilEffects pencilEffects))
                {
                    pencilEffects.SetedgePositionParent();
                    pencilEffects.EnableEffects();
                }

                pencilUpgrader.GetComponent<PencilShooter>().PrepareShoot(_shootTarget.transform);
            }
        }
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
