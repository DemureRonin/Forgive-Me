using System.Collections;
using _Scripts.PlayerScripts;
using _Scripts.Sphere;
using UnityEngine;

namespace _Scripts.Components
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Animator _volumeAnimator;
        [SerializeField] private float _teleportTime;

        private AudioSource _audioSource;
        private GameObject _player;
  

        private void Start()
        {
            _player = FindAnyObjectByType<Player>().gameObject;
            _audioSource = GetComponent<AudioSource>();
        }

        public void TeleportToSelf()
        {
            var sphere = FindAnyObjectByType<RoundSphereProjectile>();
            if (sphere.IsReturning) return;
            var playerRigidbody = _player.GetComponent<Rigidbody>();
            playerRigidbody.velocity = Vector3.zero;
            _player.SetActive(false);
            _volumeAnimator.Play("teleport");
            _audioSource.Play();
            StartCoroutine(StartTeleporting());
        }

        private IEnumerator StartTeleporting()
        {
            yield return new WaitForSeconds(_teleportTime);
            _player.transform.position = transform.position;
            _volumeAnimator.Play("afterTeleport");
            yield return new WaitForSeconds(_teleportTime);
            _player.SetActive(true);
        }
    }
}