using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private List<Pin> _pins;

    public Pin Take()
    {
        Pin pin = _pins[0];
        Item item = pin.gameObject.GetComponent<Item>();
        item.DeactivateGravity();
        _pins.RemoveAt(0);
        return pin;
    }
}
