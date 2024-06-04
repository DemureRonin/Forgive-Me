using _Scripts.Model;
using UnityEngine;

namespace _Scripts.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class LevelMusicComponent : MonoBehaviour
    {
        private AudioSource _source;
        private GameSession _session;

        public void PlayMusic()
        {
            _source.Play();
        }
    }
}