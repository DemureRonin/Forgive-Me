using System;
using System.Collections;
using _Scripts.PlayerScripts;
using _Scripts.UI.Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI.Dialogs
{
    
    public class DialogWidget : AnimatedWindow
    {
        [SerializeField] private Text _text;

        private Coroutine _dialogCoroutine;
        private AudioSource _audioSource;

        private WaitForSeconds _letterDelay;
        private WaitForSeconds _sentenceDelay;
        private WaitForSeconds _startDelay;
        private WaitForSeconds _endDelay;

        private UnityEvent _onShow;
        private UnityEvent _onComplete;

        private int _enqueuedCount;

        private bool _isDialogRunning;

        public bool IsDialogRunning => _isDialogRunning;

        protected override void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            base.Awake();
        }

        [ContextMenu("StartDialog")]
        protected virtual void StartDialog(DialogData dialog, UnityEvent onShow, UnityEvent onComplete)
        {
            if (dialog == null)
            {
                throw new ArgumentException("No dialog passed.");
            }

            if (_isDialogRunning)
            {
                StartCoroutine(EnqueueDialog(dialog, onShow, onComplete));
                return;
            }

            _isDialogRunning = true;

            _onShow = onShow;
            _onComplete = onComplete;

            _onShow.Invoke();
            _letterDelay = new WaitForSeconds(dialog.TextSpeed);
            _sentenceDelay = new WaitForSeconds(dialog.SentenceDelay);
            _startDelay = new WaitForSeconds(dialog.StartDelay);
            _endDelay = new WaitForSeconds(dialog.EndDelay);

            ShowWindow();
            _dialogCoroutine =  StartCoroutine(TypeDialog(dialog));
        }

        private IEnumerator EnqueueDialog(DialogData dialog, UnityEvent onShow, UnityEvent onComplete)
        {
            if (_enqueuedCount > 1) yield break;
            _enqueuedCount++;
            yield return new WaitUntil(() => _isDialogRunning);
            StartDialog(dialog, onShow, onComplete);
            _enqueuedCount--;
        }

        private IEnumerator TypeDialog(DialogData dialog)
        {
            yield return _startDelay;

            foreach (var sentence in dialog.Sentences)
            {
                _text.text = string.Empty;
                foreach (var letter in sentence.ToCharArray())
                {
                    _audioSource.Stop();
                    _audioSource.Play();
                    _text.text += letter;
                    yield return _letterDelay;
                }

                yield return _sentenceDelay;
            }

            _text.text = string.Empty;
            HideWindow();
            _isDialogRunning = false;
            yield return _endDelay;
            _onComplete?.Invoke();
        }

        private void HideDialog()
        {
            if (!_isDialogRunning) return;
            if (_dialogCoroutine != null)
                StopCoroutine(_dialogCoroutine);
            _text.text = string.Empty;
            HideWindow();
            _onComplete?.Invoke();
            _isDialogRunning = false;
        }


        protected virtual void OnEnable()
        {
            DialogObserver.OnDialogShown += StartDialog;
            PlayerSystemInputHandler.OnDialogSkip += HideDialog;
            ItemObserver.OnShow += HideDialog;
        }

        protected virtual void OnDisable()
        {
            DialogObserver.OnDialogShown -= StartDialog;
            PlayerSystemInputHandler.OnDialogSkip -= HideDialog;
            ItemObserver.OnShow -= HideDialog;
        }
    }
}