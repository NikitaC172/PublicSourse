using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageClick : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Button _button;
    private int _count = 0;

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeImage);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeImage);
    }

    private void ChangeImage()
    {
        _count++;

        if (_count > _sprites.Count - 1)
        {
            _count = 0;
        }

        _image.sprite = _sprites[_count];
    }
}
