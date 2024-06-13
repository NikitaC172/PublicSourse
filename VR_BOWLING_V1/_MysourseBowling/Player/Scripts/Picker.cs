using System;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private XRIDefaultInputActions _playerInput;
    [SerializeField] private bool _isLeftHand = true;

    private bool _isTakeButtonPush;
    private Item _item;

    public Action Activated;

    private void Awake()
    {
        _playerInput = new XRIDefaultInputActions();

        if (_isLeftHand == true)
        {
            _playerInput.XRILeftHandInteraction.Select.started += ctx => PushButtonTake();
            _playerInput.XRILeftHandInteraction.Select.canceled += ctx => ReleaseButtonTake();
        }
        else
        {
            _playerInput.XRIRightHandInteraction.Select.started += ctx => PushButtonTake();
            _playerInput.XRIRightHandInteraction.Select.canceled += ctx => ReleaseButtonTake();
        }
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_item == null)
        {
            if (other.TryGetComponent<Item>(out Item item))
            {
                TakeItem(item);
            }
            else if (other.transform.parent != null)
            {
                if (other.transform.parent.TryGetComponent<Item>(out Item itemParent))
                {
                    TakeItem(itemParent);
                }
            }
        }
    }

    private void TakeItem(Item item)
    {
        if (_isTakeButtonPush == true)
        {
            _item = item;
            _item.SetParent(gameObject);
        }
    }

    private void PushButtonTake()
    {
        _isTakeButtonPush = true;
        Activated?.Invoke();
    }

    private void ReleaseButtonTake()
    {
        if (_item != null)
        {
            _isTakeButtonPush = false;
            _item.SetParent(null);
            _item = null;
        }

        _isTakeButtonPush = false;
    }
}
