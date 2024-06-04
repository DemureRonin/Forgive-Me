using System;
using System.Collections.Generic;
using _Scripts.Components.Triggers;
using _Scripts.Model;
using UnityEngine;

namespace _Scripts.Components
{
    [CreateAssetMenu(fileName = "GivenCollectables")]
    public class GivenCollectibles : ScriptableObject
    {
        [SerializeField] private List<ItemNames> _givenItems = new();

        public List<ItemNames> GivenItems => _givenItems;
        public int disksGiven;

        private void Add(ItemNames item)
        {
            _givenItems.Add(item);
        }

        private void OnEnable()
        {
            GivenCollectiblesObserver.OnGive += Add;
        }

        private void OnDisable()
        {
            GivenCollectiblesObserver.OnGive -= Add;
        }
    }
}