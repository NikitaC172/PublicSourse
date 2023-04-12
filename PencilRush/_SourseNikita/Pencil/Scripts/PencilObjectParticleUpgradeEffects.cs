using UnityEngine;

public class PencilObjectParticleUpgradeEffects : MonoBehaviour
{
    public void ResetPosition()
    {
        transform.localEulerAngles = Vector3.zero;
    }
}
