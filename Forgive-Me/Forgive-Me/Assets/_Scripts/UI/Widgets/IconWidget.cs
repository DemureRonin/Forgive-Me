using _Scripts.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.UI.Widgets
{
    public class IconWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _dolphinImage;
        [SerializeField] private GameObject _columnImage;

        private readonly string[] _activeScenes = {"Safespace 1","Safespace 2"};

        private Animator _dolphinAnimator;
        private Animator _columnAnimator;

        private bool _isDolphinInteractable;
        private bool _isColumnInteractable;

        public delegate void DisableEvent();
        public static event DisableEvent OnDolphinDisabled;
        public static event DisableEvent OnColumnDisabled;

        private void Awake()
        {
            _dolphinAnimator = _dolphinImage.GetComponent<Animator>();
            _columnAnimator = _columnImage.GetComponent<Animator>();
            Check();
        }

        private void SetActive(bool value, bool isDolphin)
        {
            if (isDolphin)
            {
                _dolphinImage.SetActive(value);
                OnDolphinDisabled?.Invoke();
            }
            else
            {
                _columnImage.SetActive(value);
                OnColumnDisabled?.Invoke();
            }
        }

        private void AnimateDolphin()
        {
            if (!_dolphinAnimator.isActiveAndEnabled) return;
            _isDolphinInteractable = !_isDolphinInteractable;
            if (_isDolphinInteractable)
            {
                _dolphinAnimator.Play("start");
                return;
            }

            _dolphinAnimator.Play("end");
        }

        private void AnimateColumn()
        {
            if (!_columnAnimator.isActiveAndEnabled) return;
            _isColumnInteractable = !_isColumnInteractable;
            if (_isColumnInteractable)
            {
                _columnAnimator.Play("start");
                return;
            }

            _columnAnimator.Play("end");
        }

        private void Check()
        {
            foreach (var activeScene in _activeScenes)
            {
                if (SceneManager.GetActiveScene().name == activeScene)
                {
                    _dolphinImage.SetActive(true);
                    _columnImage.SetActive(true);
                    return;
                }

                _dolphinImage.SetActive(false);
                _columnImage.SetActive(false);
            }
        }


        private void OnEnable()
        {
            ItemReceiver.OnDolphinInteractable += AnimateDolphin;
            ItemReceiver.OnColumnInteractable += AnimateColumn;
            IconEnableObserver.OnIconToggle += SetActive;
        }

        private void OnDisable()
        {
            ItemReceiver.OnDolphinInteractable -= AnimateDolphin;
            ItemReceiver.OnColumnInteractable -= AnimateColumn;
            IconEnableObserver.OnIconToggle -= SetActive;
        }
    }
}