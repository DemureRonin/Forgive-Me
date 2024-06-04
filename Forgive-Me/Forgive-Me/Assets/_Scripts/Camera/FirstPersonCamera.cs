using _Scripts.Model;
using UnityEngine;

namespace _Scripts.Camera
{
    public class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField] private PlayerPersistentSettings _playerSettings;

        private Transform _orientation;

        private float SensX => _playerSettings.SensX;
        private float SensY => _playerSettings.SensY;
        private float _xRotation;
        private float _yRotation;

        private void Start()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _orientation = FindAnyObjectByType<Orientation>().transform;
        }

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX * 1000;
            var mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY * 1000;

            _yRotation += mouseX;
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90);

            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        }
    }
}