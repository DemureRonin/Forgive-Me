using _Scripts.Model;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.LevelManagement
{
    public class CheckInventory : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onEmpty;
        [SerializeField] private ItemNames[] _itemsToCheck;
        [SerializeField] private bool _checkOnStart = true;

        private InventoryItemModel Inventory => _session.Data.Inventory;
        private GameSession _session;

        private void Start()
        {
            _session = FindAnyObjectByType<GameSession>();
            if (_checkOnStart)
                Check();
        }

        public void Check()
        {
            foreach (var items in _itemsToCheck)
            {
                var anyItems = Inventory.CheckStock(items);
                if (anyItems) return;
            }

            _onEmpty?.Invoke();
        }

        public bool Check(ItemNames itemNames)
        {
            var anyItems = Inventory.CheckStock(itemNames);
            return anyItems;
        }
    }
}