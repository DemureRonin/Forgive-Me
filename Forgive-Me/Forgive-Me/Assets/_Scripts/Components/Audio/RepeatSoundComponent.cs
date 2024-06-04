using System.Collections;
using UnityEngine;

namespace _Scripts.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RepeatSoundComponent : MonoBehaviour
    {
      [SerializeField]  private float _soundDelay;
      private AudioSource _audioSource;

      private void Awake()
      {
          _audioSource = GetComponent<AudioSource>();
      }

      private void Start()
        {
            StartCoroutine(MakeSounds());
        }

        private IEnumerator MakeSounds()
        {
            while (enabled)
            {
                _audioSource.Play();
                yield return new WaitForSeconds(_soundDelay);
            }
        }
    }
}
