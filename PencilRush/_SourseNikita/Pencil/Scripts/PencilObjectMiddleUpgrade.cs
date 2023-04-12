using System.Collections;
using UnityEngine;

public class PencilObjectMiddleUpgrade : MonoBehaviour
{
    [SerializeField] private float _fullTurnTime = 10.0f;
    [SerializeField] private float deltaTime = 0.1f;


    public void EnableObject(bool isRotatable)
    {
        gameObject.SetActive(true);

        if (isRotatable == true)
        {
            StartCoroutine(Rotate());
        }
    }

    private IEnumerator Rotate()
    {
        float roundeCircle = 360;
        float currentAngle = 0;
        float deltaAngle = (roundeCircle / _fullTurnTime) * deltaTime;
        var waitSeconds = new WaitForSeconds(deltaTime);

        while (true)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, currentAngle, 0);
            currentAngle += deltaAngle;
            yield return waitSeconds;
        }
    }
}