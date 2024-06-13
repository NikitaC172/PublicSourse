using UnityEngine;

public class PickerV2 : MonoBehaviour
{
    [SerializeField] private XRIDefaultInputActions _playerInput;
    [SerializeField] private Joint _joint;
    [SerializeField] private bool _isLeftHand = true;

    private bool _isTakeButtonPush;
    private ItemV2 _item;

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

    private void FixedUpdate()
    {
        if (_item != null)
        {
            if (_joint.currentForce.magnitude >= 1000f)
            {
                _joint.connectedBody = null;
            }
        }
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_item == null && _isTakeButtonPush == true)
        {
            if (other.TryGetComponent<ItemV2>(out ItemV2 item))
            {
                TakeItem(item);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ItemV2>(out ItemV2 item))
        {
            _item = null;
        }

    }

    private void OnJointBreak(float breakForce)
    {
        ReleaseButtonTake();
    }

    private void TakeItem(ItemV2 item)
    {
        {
            _item = item;
            _joint.connectedBody = item.GetRigidbody();
        }
    }

    private void PushButtonTake()
    {
        _isTakeButtonPush = true;
    }

    private void ReleaseButtonTake()
    {
        if (_item != null)
        {
            _isTakeButtonPush = false;
            _item = null;
            _joint.connectedBody = null;
        }
        else
        {
            _isTakeButtonPush = false;
        }
    }
}
