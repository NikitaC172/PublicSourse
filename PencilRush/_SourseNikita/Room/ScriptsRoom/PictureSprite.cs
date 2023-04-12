using System;
using UnityEngine;

public class PictureSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    public void ChangeSprite(SpriteRenderer sprite)
    {
        if (sprite == null)
            throw new NullReferenceException(nameof(sprite));

        _sprite.sprite = sprite.sprite;
        _sprite.color = sprite.color;
    }
}
