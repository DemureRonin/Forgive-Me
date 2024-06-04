using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.UI
{
    public class FadeScreen : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _fadeOnStart = true;

        private UnityEngine.Camera _camera;


        private void Start()
        {
            if (_fadeOnStart)
                StartSpawnAnimation();
        }

        private void StartSpawnAnimation()
        {
            _animator.Play("start");
        }

        private void StartDeathAnimation(float time)
        {
            _animator.Play("die");
        }

        private void OnDisable()
        {
            Player.OnPlayerDeath -= StartDeathAnimation;
        }

        private void OnEnable()
        {
            Player.OnPlayerDeath += StartDeathAnimation;
        }
    }
}