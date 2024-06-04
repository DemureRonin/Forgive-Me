using UnityEngine;

namespace _Scripts.UI.Dialogs
{
    [CreateAssetMenu(fileName = "Dialog")]
    public class DialogData : ScriptableObject
    {
        [TextArea] [SerializeField] private string[] _sentences;

        [SerializeField] private float _textSpeed = 0.05f;
        [SerializeField] private float _sentenceDelay = 2f;
        [SerializeField] private float _startDelay = 0.2f;
        [SerializeField] private float _endDelay = 0.5f;
        public string[] Sentences => _sentences;

        public float TextSpeed => _textSpeed;

        public float SentenceDelay => _sentenceDelay;

        public float StartDelay => _startDelay;
        public float EndDelay => _endDelay;
    }
}