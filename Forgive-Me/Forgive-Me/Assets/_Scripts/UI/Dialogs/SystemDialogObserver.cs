namespace _Scripts.UI.Dialogs
{
    public class SystemDialogObserver : DialogObserver
    {
        public static event DialogEvent OnSystemDialogShown;

        public override void Show()
        {
            OnSystemDialogShown?.Invoke(_dialogData, _onShow, _onComplete);
        }
    }
}