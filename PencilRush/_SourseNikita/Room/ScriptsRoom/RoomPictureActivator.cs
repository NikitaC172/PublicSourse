using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomPictureActivator : MonoBehaviour
{
    [SerializeField] private List<PictureOpener> _pictureOpeners;
    [SerializeField] private int _openPictures = 3;
    [SerializeField] private int _openedPictures = 2;

    public Action ChangedOpenedPictures;

    public IReadOnlyList<PictureOpener> PictureOpeners => _pictureOpeners;

    private void Awake()
    {
        TakeSaveData();

        for (int i = 0; i < _pictureOpeners.Count; i++)
        { 
            if (i > _openedPictures - 1)
            {
                bool isReadyToOpen;

                if (i <= _openPictures - 1)
                {
                    isReadyToOpen = true;
                    _pictureOpeners[i].Opened += ChangeOpenedPicture;
                    _pictureOpeners[i].TakeStatus(isReadyToOpen);
                }
                else
                {
                    isReadyToOpen = false;
                    _pictureOpeners[i].TakeStatus(isReadyToOpen);
                }
            }
        }
    }

    private void TakeSaveData()
    {
        _openPictures = SaveSystem.CurrentUnlockInteractivePicture;
        _openedPictures = SaveSystem.CurrentOpenedInteractivePicture;
    }

    private void ChangeOpenedPicture()
    {
        _openedPictures++;
        _pictureOpeners[_openedPictures-1].Opened -= ChangeOpenedPicture;
        ChangedOpenedPictures?.Invoke();
    }
}
