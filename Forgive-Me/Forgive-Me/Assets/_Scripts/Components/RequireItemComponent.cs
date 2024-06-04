using _Scripts.Model;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components
{
    public class RequireItemComponent : MonoBehaviour
    {
        private GameSession _session;
        private InventoryItemModel Inventory => _session.Data.Inventory;

        private void Start()
        {
            _session = FindAnyObjectByType<GameSession>();
        }

        public void Remove(ItemNames id, UnityEvent onSuccess,UnityEvent onFail = null )
        {
       
        }
    }
}