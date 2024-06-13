using System.Collections;
using System.Collections.Generic;
using UltimateXR.Extensions.Unity;
using UnityEngine;

public class UIFixedPoint : MonoBehaviour
{
    [SerializeField] private PointUI _pointUI;
    [SerializeField] private RectTransform _rectTransform;

    private void OnEnable()
    {
        StartCoroutine(SetPosition());
    }

    private IEnumerator SetPosition()
    {
        while (true)
        {
            transform.SetPositionAndRotation(_pointUI.transform.position, _pointUI.transform.rotation);
            //_rectTransform.SetPositionX(11f); //m_AnchoredPosition.x
            yield return null;
        }
    }
}
