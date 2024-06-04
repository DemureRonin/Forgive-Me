using _Scripts.LevelManagement;
using _Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ChoiceWindowWidget : AnimatedWindow
    {
        [SerializeField] private TMP_Text  _blessingText;
        [SerializeField] private Button _blessingButton;
        [SerializeField] private CheckInventory _checkInventory;

        private void CheckInventory()
        {
            if (!_checkInventory.Check(ItemNames.Blessing)) return;
            _blessingText.text = "Use the Blessing";
            _blessingButton.interactable = true;
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void OnEnable()
        {
            ChoiceWindowObserver.OnWindowShow += ShowWindow;
            ChoiceWindowObserver.OnWindowShow += ShowCursor;
            ChoiceWindowObserver.OnWindowShow += CheckInventory;
        }

        private void OnDisable()
        {
            ChoiceWindowObserver.OnWindowShow -= ShowWindow;
            ChoiceWindowObserver.OnWindowShow -= ShowCursor;
            ChoiceWindowObserver.OnWindowShow -= CheckInventory;
        }

        public void OnYesClick()
        {
            SceneManager.LoadScene("NeutralEnding");
        }

        public void OnNoClick()
        {
            SceneManager.LoadScene("BadEnding");
        }

        public void OnBlessingClick()
        {
            SceneManager.LoadScene("GoodEnding");
        }
    }
}