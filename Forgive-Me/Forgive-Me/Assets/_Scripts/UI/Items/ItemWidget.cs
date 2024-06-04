using _Scripts.PlayerScripts;
using _Scripts.UI.Dialogs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI.Items
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _description;
        [SerializeField] private Text _buttonText;
        [SerializeField] private GameObject _itemDescription;
        [SerializeField] private GameObject _ui;
        [SerializeField] private GameObject _dolphinImage;
        [SerializeField] private DialogWidget _dialogWidget;

        private const string ItemHolder = "ItemHolder";

        private InputToggle _inputToggle;
        
        private UnityEvent _onComplete;

        private GameObject _itemPosition;
        private GameObject _item;

        private void Awake()
        {
            _inputToggle = GetComponent<InputToggle>();
        }

        public void Show(ItemData data, UnityEvent onShow, UnityEvent onComplete)
        {
            _inputToggle.DisableInput();
            _inputToggle.DisableCameraMovement();
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            
            _onComplete = onComplete;
            onShow?.Invoke();

            _itemDescription.SetActive(true);
            _ui.SetActive(false);
            var image  = _dolphinImage.GetComponent<Image>();
            image.enabled = false;
            
            _name.text = data.Name;
            _description.text = data.Description;
            _buttonText.text = data.ButtonText;
            
            _itemPosition = GameObject.FindWithTag(ItemHolder);
            _item = Instantiate(data.Prefab, _itemPosition.transform, false);
        }

        public void Hide()
        {
            _inputToggle.EnableInput();
            _inputToggle.EnableCameraMovement();
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            
            Destroy(_item);
            _itemDescription.SetActive(false);
            _ui.SetActive(true);
            var image  = _dolphinImage.GetComponent<Image>();
            image.enabled = true;
            
            _onComplete?.Invoke();
        }

        private void OnEnable()
        {
            ItemObserver.OnItemShown += Show;
        }

        private void OnDisable()
        {
            ItemObserver.OnItemShown -= Show;
        }
    }
}