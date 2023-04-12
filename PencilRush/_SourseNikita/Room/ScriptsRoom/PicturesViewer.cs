using UnityEngine;

public class PicturesViewer : MonoBehaviour
{
    [SerializeField] private InteractivePictureList _interactivePictureList;
    [SerializeField] private RoomPictureActivator _roomPictureActivator;

    private void Awake()
    {
        for (int i = 0; i < _roomPictureActivator.PictureOpeners.Count; i++)
            if (_roomPictureActivator.PictureOpeners[i].TryGetComponent(out PictureSprite pictureSprite))
                pictureSprite.ChangeSprite(_interactivePictureList.Pictures[i].Sprite);
    }
}
