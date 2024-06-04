using UnityEngine;

namespace _Scripts.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        private Transform _cameraPosition;

        private void Start()
        {
            _cameraPosition = FindAnyObjectByType<CameraPosition>().transform;
        }

        private void Update()
        {
            transform.position = _cameraPosition.position;
        }
    }
}