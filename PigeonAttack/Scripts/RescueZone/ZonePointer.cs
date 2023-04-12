using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePointer : MonoBehaviour
{
    [SerializeField] private RescueZone _rescueZone;

    private void Update()
    {
        transform.LookAt(_rescueZone.transform.position, Vector3.up);
    }
}
