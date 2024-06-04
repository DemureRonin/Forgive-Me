using _Scripts.Model;
using _Scripts.UI.Items;
using UnityEngine;

namespace _Scripts.Components
{
    public class CheckBlessing : MonoBehaviour
    {
        [SerializeField] private bool _isRightBlessing;
        [SerializeField] private InventoryItemModel _inventory;
        [SerializeField] private ItemObserver _blessing;
        [SerializeField] private Collectable _blessingCollectable;

        public void CheckBlessingInInventory()
        {
            if (_isRightBlessing)
            {
                foreach (var item in _inventory.Items)
                {
                    if (item.Id != ItemNames.BlessingHalfLeft || item.Value <= 0) continue;
                    _blessingCollectable.Collect();
                    _blessing.Show();
                    return;

                }
            }
            else
            {
                foreach (var item in _inventory.Items)
                {
                    if (item.Id != ItemNames.BlessingHalfRight || item.Value <= 0) continue;
                    _blessingCollectable.Collect();
                    _blessing.Show();
                    return;

                }
            }
        }
    }
}