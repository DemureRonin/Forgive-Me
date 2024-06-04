using System;
using UnityEngine;

namespace _Scripts.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioData[] _sounds;

        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void Play(string id)
        {
            foreach (var audioData in _sounds)
            {
                if (audioData.Id != id) continue;

                _source.Stop();
                _source.PlayOneShot(audioData.Clip);
                return;
            }
        }

        [Serializable]
        public class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }
}