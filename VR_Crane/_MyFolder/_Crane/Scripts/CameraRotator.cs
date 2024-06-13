using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class CameraRotator : MonoBehaviour
    {
        private void FixedUpdate()
        {
            transform.SetPositionAndRotation(transform.position,Quaternion.identity);
        }
    }
}
