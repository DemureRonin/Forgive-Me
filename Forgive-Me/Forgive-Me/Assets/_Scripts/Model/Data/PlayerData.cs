using System;
using UnityEngine;

namespace _Scripts.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        private const string InventoryPathName = "Inventory";
        private const string PerksPathName = "Perks";
        public InventoryItemModel Inventory { get; private set; }

        public PlayerPerksModel PlayerPerks { get; private set; }


        public void Initialize()
        {
            Inventory = Resources.Load<InventoryItemModel>(InventoryPathName);
            PlayerPerks = Resources.Load<PlayerPerksModel>(PerksPathName);
        }
    }
}