using UnityEngine;

namespace _Scripts.UI
{
    public class IconEnableObserver : MonoBehaviour

    {
        [SerializeField] private bool _isDolphin;


        public delegate void IconEvent(bool value, bool isDolphin);

        public static event IconEvent OnIconToggle;

        public void ToggleIcons(bool value)
        {
            OnIconToggle?.Invoke(value, _isDolphin);
        }
    }
}