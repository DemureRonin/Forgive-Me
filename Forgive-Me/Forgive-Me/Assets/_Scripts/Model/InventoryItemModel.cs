using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Model
{
    [CreateAssetMenu(menuName = "Model/InventoryModel", fileName = "Inventory")]
    public class InventoryItemModel : ScriptableObject
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef[] Items => _items;

        [SerializeField] private List<GameObject> _spheres;

        public List<GameObject> Spheres => _spheres;

        public void AddItem(ItemNames id)
        {
            foreach (var item in _items)
            {
                if (id == item.Id)
                {
                    item.Value++;
                }
            }
        }

        public void RemoveItem(ItemNames id)
        {
            foreach (var item in _items)
            {
                if (id == item.Id)
                {
                    item.Value--;
                }
            }
        }

        public bool IsEmpty()
        {
            foreach (var item in _items)
            {
                if (item.Value > 0) return false;
            }

            return true;
        }

        public bool CheckStock(ItemNames id)
        {
            foreach (var item in _items)
            {
                if (id == item.Id &&item.Value > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddSphere(GameObject sphere)
        {
            _spheres.Add(sphere);
        }
    }

    [Serializable]
    public class ItemDef
    {
        [SerializeField] private ItemNames _id;
        [SerializeField] private int _value;

        public ItemNames Id => _id;

        public int Value
        {
            get => _value;
            set
            {
                if (_value == 0 && value < 0) return;
                _value = value;
            }
        }
    }

    public enum ItemNames
    {
        Disk,
        Fish,
        Sun,
        Patience,
        Hope,
        BlessingHalfRight,
        BlessingHalfLeft,
        Blessing
    }
}