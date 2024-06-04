using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class HealthWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _image;

        private void SetHpBar(int hp, int maxHp)
        {
            _image.SetProgress((float) hp / maxHp);
        }

        private void OnEnable()
        {
            HealthChangeObserver.OnHealthChanged += SetHpBar;
        }

        private void OnDisable()
        {
            HealthChangeObserver.OnHealthChanged -= SetHpBar;
        }
    }
}