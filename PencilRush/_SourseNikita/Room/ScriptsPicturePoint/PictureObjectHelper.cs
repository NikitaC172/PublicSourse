using System.Collections;
using UnityEngine;

public class PictureObjectHelper : MonoBehaviour
{
    [SerializeField] private RoomImageHelper _roomImageHelper;
    [SerializeField] private HelperObject _helperObject;
    [SerializeField] private SpriteRenderer _spriteHelper;
    [SerializeField] private PictureOpener _pictureOpener;

    private void OnValidate()
    {
        _roomImageHelper = GetComponentInParent<RoomImageHelper>();
    }

    private void OnEnable()
    {
        _roomImageHelper.ShowedHelper += ShowPromt;
        _roomImageHelper.HidedHelper += HidePromt;
    }

    private void OnDisable()
    {
        _roomImageHelper.ShowedHelper -= ShowPromt;
        _roomImageHelper.HidedHelper -= HidePromt;
    }

    private void ShowPromt()
    {
        if (CheckOpenStatus() == true)
        {
            float alfaMax = 1.0f;
            StartCoroutine(ChangeAlfaChanel(alfaMax));
            _helperObject.gameObject.SetActive(true);
        }
    }

    private void HidePromt()
    {
        if (CheckOpenStatus() == true)
        {
            float alfaMin = 0;
            StartCoroutine(ChangeAlfaChanel(alfaMin));
        }
    }

    private IEnumerator ChangeAlfaChanel(float target)
    {
        float timeChange = 1.0f;
        float time = 0.0f;
        Color currentColor = _spriteHelper.color;
        float alfa = currentColor.a;

        while (time < timeChange)
        {
            time += Time.deltaTime;
            alfa = Mathf.Lerp(currentColor.a, target, time / timeChange);
            _spriteHelper.color = new Color(currentColor.r, currentColor.g, currentColor.b, alfa);
            yield return null;
        }

        alfa = target;
        _spriteHelper.color = new Color(currentColor.r, currentColor.g, currentColor.b, alfa);

        if (target == 0)
        {
            _helperObject.gameObject.SetActive(false);
        }
    }

    private bool CheckOpenStatus()
    {
        if(_pictureOpener == null)
        {
            return true;
        }
        else
        {
            if(_pictureOpener.IsOpen == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
