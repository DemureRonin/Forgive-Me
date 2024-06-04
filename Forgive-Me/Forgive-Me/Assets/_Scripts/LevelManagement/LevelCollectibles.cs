using System;
using System.Collections.Generic;
using _Scripts.Model;
using UnityEngine;

namespace _Scripts.LevelManagement
{
    public class LevelCollectibles : MonoBehaviour
    {
        [SerializeField] private CollectablesDef[] _collectibles;
        private readonly HashSet<ItemNames> _collected = new();

        private void Start()
        {
            CheckCollected();
        }

        private void AddCollected(ItemNames id)
        {
            foreach (var collectible in _collectibles)
            {
                if (collectible.ID != id) continue;
                _collected.Add(collectible.ID);
            }
        }

        private void CheckCollected()
        {
            var positions = FindObjectsByType<CollectibleLocation>(FindObjectsSortMode.None);
            foreach (var collectible in _collectibles)
            {
                if (_collected.Contains(collectible.ID)) continue;
                
                foreach (var position in positions)
                {
                    if (collectible.ID != position.ID) continue;
                    Instantiate(collectible.Collectible, position.transform.position, Quaternion.Euler(90,90,0));
                }
               
            }
        }

        private void OnEnable()
        {
            Collectable.OnCollect += AddCollected; 
            GameSession.OnSessionStart += CheckCollected;
        }

        private void OnDisable()
        {
            Collectable.OnCollect -= AddCollected; 
            GameSession.OnSessionStart -= CheckCollected;
        }
    }

    [Serializable]
    public class CollectablesDef
    {
        [SerializeField] private ItemNames _id;

        [SerializeField] private GameObject _collectible;


        public ItemNames ID => _id;

        public GameObject Collectible => _collectible;
    }
}