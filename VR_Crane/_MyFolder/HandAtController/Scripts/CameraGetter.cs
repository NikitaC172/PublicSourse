using UnityEngine;

namespace CraneGame
{
    public class CameraGetter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public Camera GetCamera() { return _camera; }
    }
}
