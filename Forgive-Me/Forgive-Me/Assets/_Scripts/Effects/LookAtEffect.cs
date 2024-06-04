using System.Collections;
using _Scripts.PlayerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Effects
{
    public class LookAtEffect : MonoBehaviour
    {
        [SerializeField] private float _lookDelay;
        [SerializeField] private Renderer _renderer;

        private WaitForSeconds _delay;
        private Player _player;
        private bool _canLook;

        private void Awake()
        {
            _delay = new WaitForSeconds(_lookDelay);
        }

        private void Update()
        {
            if (_renderer.isVisible)
            {
                _canLook = false;
                return;
            }

            if (!_canLook) return;
            LookAtPlayer();
        }

        private void Start()
        {
            _player = FindAnyObjectByType<Player>();
            StartCoroutine(LookAtChance());
        }

        private IEnumerator LookAtChance()
        {
            while (true)
            {
                yield return _delay;
                if (!_renderer.isVisible)
                {
                    var rand = Random.Range(1, 20);
                    if (rand == 1)
                    {
                        _canLook = true;
                    }
                }
            }
        }

        private void LookAtPlayer()
        {
            transform.LookAt(_player.transform, Vector3.up);
        }
    }
}