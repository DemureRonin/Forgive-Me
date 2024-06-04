using _Scripts.Components.Audio;
using UnityEngine;

namespace _Scripts.UI
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedWindow : MonoBehaviour

    {
        [SerializeField] protected PlayAudioComponent _playAudioComponent;

        private Animator _animator;
        private static readonly int Hide = Animator.StringToHash("Hide");
        private static readonly int Show = Animator.StringToHash("Show");

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected void ShowWindow()
        {
            _playAudioComponent.Play("Show");
            _animator.SetTrigger(Show);
        }

        protected void HideWindow()
        {
            _playAudioComponent.Play("Hide");
            _animator.SetTrigger(Hide);
        }
    }
}