using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Components
{
    public class LookAtComponent : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
        }

        private void Update()
        {
            transform.LookAt(_player.transform);
        }
    }
}