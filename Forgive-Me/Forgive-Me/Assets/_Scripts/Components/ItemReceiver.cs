using System;
using System.Collections.Generic;
using _Scripts.Model;
using _Scripts.PlayerScripts;
using _Scripts.UI.Items;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components
{
    public class ItemReceiver : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onFail;
        [SerializeField] private List<ItemActions> _itemActions;
        [SerializeField] private GameObject _icoSphere;
        [SerializeField] private GameObject _dodecoSphere;
        [SerializeField] private bool _isDolphin;
        [SerializeField] private GivenCollectibles _givenCollectibles;
        [SerializeField] private ItemObserver _dodecoItemObserver;
        [SerializeField] private ItemObserver _icoItemObserver;
        [SerializeField] private ItemObserver _leftBlessing;
        [SerializeField] private Collectable _leftBlessingCollectable;

        private GameSession _session;
        private InventoryItemModel Inventory => _session.Data.Inventory;

        public delegate void ReceiveEvent();

        public static event ReceiveEvent OnSphereReceived;
        public static event ReceiveEvent OnDolphinInteractable;
        public static event ReceiveEvent OnColumnInteractable;

        private bool _canInteract;

        private void Start()
        {
            _session = FindFirstObjectByType<GameSession>();
        }

        public void CheckGivenDisk()
        {
            if (_givenCollectibles.disksGiven == 0)
            {
                _icoItemObserver?.Show();
                Inventory.AddSphere(_icoSphere);
            }
            else
            {
                _dodecoItemObserver.Show();
                Inventory.AddSphere(_dodecoSphere);
            }
            _givenCollectibles.disksGiven++;
            OnSphereReceived?.Invoke();
        }

        public void SetInteraction(bool value)
        {
            _canInteract = value;
            if (_isDolphin)
            {
                OnDolphinInteractable?.Invoke();
                return;
            }
            OnColumnInteractable?.Invoke();
        }

        public void CheckCollectables()
        {
            if (_givenCollectibles.GivenItems.Contains(ItemNames.Fish))
            {
                _leftBlessing.Show();
                _leftBlessingCollectable.Collect();
            } 
        }

        private void Interact()
        {
            if (!_canInteract) return;
            foreach (var itemAction in _itemActions)
            {
                foreach (var item in Inventory.Items)
                {
                    if (itemAction.Id != item.Id || item.Value <= 0) continue;
                    Inventory.RemoveItem(item.Id);
                    itemAction.OnSuccess?.Invoke();
                    _itemActions.Remove(itemAction);
                    return;
                }
            }

            _onFail?.Invoke();
        }

        private void OnEnable()
        {
            Player.OnInteraction += Interact;
        }

        private void OnDisable()
        {
            Player.OnInteraction -= Interact;
        }
    }


    [Serializable]
    public class ItemActions
    {
        [SerializeField] private ItemNames _id;

        [SerializeField] private UnityEvent _onSuccess;
        public ItemNames Id => _id;
        public UnityEvent OnSuccess => _onSuccess;
    }
}