using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.UI.Items
{
    public class ItemObserver : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private UnityEvent _onShow;
        [SerializeField] private UnityEvent _onComplete;

        public delegate void ItemEvent(ItemData itemData, UnityEvent onShow, UnityEvent onComplete);
        public delegate void ItemShowEvent();

        public static event ItemEvent OnItemShown;
        public static event ItemShowEvent OnShow;

        public void Show()
        {
            OnItemShown?.Invoke(_itemData, _onShow, _onComplete);
            OnShow?.Invoke();
        }
    }
}