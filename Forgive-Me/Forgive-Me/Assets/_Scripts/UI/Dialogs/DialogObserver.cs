using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.UI.Dialogs
{
    public class DialogObserver : MonoBehaviour
    {
        [SerializeField] protected DialogData _dialogData;
        [SerializeField] protected UnityEvent _onShow;
        [SerializeField] protected UnityEvent _onComplete;

        public delegate void DialogEvent(DialogData dialogData, UnityEvent onShow, UnityEvent onComplete);

        public static event DialogEvent OnDialogShown;

        public virtual void Show()
        {
            OnDialogShown?.Invoke(_dialogData, _onShow, _onComplete);
        }

        public void SetDialog(DialogData dialogData)
        {
            if (dialogData == null) throw new Exception("No dialog set");
            _dialogData = dialogData;
        }

        public void SetEventOnComplete(UnityEvent onComplete)
        {
            _onComplete = onComplete ?? throw new Exception("No dialog set");
        }
    }
}