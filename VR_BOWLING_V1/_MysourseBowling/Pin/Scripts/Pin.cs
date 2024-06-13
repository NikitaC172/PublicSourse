using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool _isActive;

    private void OnEnable()
    {
        _isActive = true;
    }

    private void OnDisable()
    {
        _isActive = false;
    }

    public bool FallCheck()
    {
        if (_isActive == true)
        {
            if (transform.rotation.eulerAngles.x > 30 || transform.rotation.eulerAngles.x < -30)
            {
                return true;
            }
            else if (transform.rotation.eulerAngles.z > 30 || transform.rotation.eulerAngles.z < -30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
