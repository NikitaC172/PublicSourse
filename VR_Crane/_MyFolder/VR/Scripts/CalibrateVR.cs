using DaVanciInk.AdvancedPlayerPrefs;
using System.Collections.Generic;
using UnityEngine;

public class CalibrateVR : MonoBehaviour
{
    [SerializeField] private List<CalibrateVR_Set> _calibrateVR_Sets;
    [SerializeField] private Vector3 _offsetY = new Vector3(0, 0.82f, 0);
    [SerializeField] private Vector2 _rangeX;
    [SerializeField] private Vector2 _rangeY;
    [SerializeField] private Vector2 _rangeZ;

    private void Start()
    {
        Invoke(nameof(LoadPosition), 2f);
        //load +
    }

    private void OnEnable()
    {
        foreach (var set in _calibrateVR_Sets)
        {
            set.OffsetChanged += ChangeOffset;
        }
    }

    private void OnDisable()
    {
        foreach (var set in _calibrateVR_Sets)
        {
            set.OffsetChanged -= ChangeOffset;
        }
    }

    private void ChangeOffset(Vector3 vector3)
    {
        Vector3 position;

        if (vector3 == Vector3.zero)
        {
            position = _offsetY;
            gameObject.transform.localPosition = position;
        }
        else
        {
            position = gameObject.transform.localPosition;
            position += vector3;

            if (CheñkCorrectPosition(position) == true)
            {
                gameObject.transform.localPosition = position;
            }
            else
            {
                position -= vector3;
            }
        }


        SavePosition(position);
    }

    private bool CheñkCorrectPosition(Vector3 position)
    {
        Debug.Log(position);

        if(_rangeX.x < position.x && position.x < _rangeX.y)
        {
            if (_rangeY.x < position.y && position.y < _rangeY.y)
            {
                if (_rangeZ.x < position.z && position.z < _rangeZ.y)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void LoadPosition()
    {
        gameObject.transform.localPosition = _offsetY;
        gameObject.transform.localPosition = new Vector3(AdvancedPlayerPrefs.GetFloat("OffsetX"), AdvancedPlayerPrefs.GetFloat("OffsetY"), AdvancedPlayerPrefs.GetFloat("OffsetZ"));
    }

    private void SavePosition(Vector3 position)
    {
        //save
        AdvancedPlayerPrefs.SetFloat("OffsetX", position.x);
        AdvancedPlayerPrefs.SetFloat("OffsetY", position.y);
        AdvancedPlayerPrefs.SetFloat("OffsetZ", position.z);
    }
}
