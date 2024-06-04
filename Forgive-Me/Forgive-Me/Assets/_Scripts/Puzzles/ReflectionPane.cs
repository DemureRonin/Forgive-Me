using _Scripts.Effects;
using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Puzzles
{
    public class ReflectionPane : MonoBehaviour
    {
        [SerializeField] private string _normalNoise ="WhiteNoise" ;
        [SerializeField] private string _completeNoise = "ColorNoise";
        [SerializeField] private AudioClip _onCompleteSound;

        public AudioClip ONCompleteSound => _onCompleteSound;

        private AudioSource _audioSource;

        public AudioSource AudioSource => _audioSource;

        private SpriteAnimations _animation;

        private ReflectionPuzzle _puzzle;

        private bool _isActivated;
        public bool IsActivated => _isActivated;

        private void Awake()
        {
            _puzzle = GetComponentInParent<ReflectionPuzzle>();
            _animation = GetComponentInChildren<SpriteAnimations>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void Reflect(GameObject other)
        {
            _isActivated = true;
            _animation.SetClip(_completeNoise);
            _puzzle.Check();
        }

        private void DeactivatePane()
        {
            _isActivated = false;
            _animation.SetClip(_normalNoise);
        }

        private void OnEnable()
        {
            Player.OnRoundSphereReturn += DeactivatePane;
        }

        public void OnComplete()
        {
            Player.OnRoundSphereReturn -= DeactivatePane;
        }

        private void OnDisable()
        {
            Player.OnRoundSphereReturn -= DeactivatePane;
        }
    }
}