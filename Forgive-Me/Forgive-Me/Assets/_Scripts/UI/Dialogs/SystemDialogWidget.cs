namespace _Scripts.UI.Dialogs
{
    public class SystemDialogWidget : DialogWidget
    {
        protected override void OnEnable()
        {
            SystemDialogObserver.OnSystemDialogShown += StartDialog;
        }

        protected override void OnDisable()
        {
            SystemDialogObserver.OnSystemDialogShown -= StartDialog;
        }
    }
}