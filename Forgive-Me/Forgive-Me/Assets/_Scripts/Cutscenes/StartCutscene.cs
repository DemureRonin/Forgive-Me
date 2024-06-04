using System.Collections;
using _Scripts.PlayerScripts;
using _Scripts.UI.Dialogs;
using UnityEngine;


namespace _Scripts.Cutscenes
{
    public class StartCutscene : MonoBehaviour
    {
       [SerializeField] private InputToggle _input;

        private DialogObserver _dialogObserver;

        private void Awake()
        {
            _dialogObserver = GetComponent<DialogObserver>();
        }

        private void Start()
        {

            StartCoroutine(ShowDialog());
            _input?.DisableInput();
            _input?.DisableCameraMovement();
        }

        private IEnumerator ShowDialog()
        {
            yield return new WaitForSeconds(5f);
            _dialogObserver.Show();
        }
        
    }
}