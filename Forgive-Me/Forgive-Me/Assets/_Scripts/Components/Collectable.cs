using _Scripts.Model;
using UnityEngine;

namespace _Scripts
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private ItemNames _id;

        private GameSession _session;
        private InventoryItemModel Inventory => _session.Data.Inventory;

        public delegate void CollectEvent(ItemNames id);

        public static event CollectEvent OnCollect;

        private void Start()
        {
            _session = FindFirstObjectByType<GameSession>();
        }

        public void Collect()
        {
            Inventory.AddItem(_id);
            OnCollect?.Invoke(_id);
        }
    }
}