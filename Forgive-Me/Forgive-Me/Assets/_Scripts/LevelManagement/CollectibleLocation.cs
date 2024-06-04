using _Scripts.Model;
using UnityEngine;

namespace _Scripts.LevelManagement
{
    public class CollectibleLocation : MonoBehaviour
    {
        [SerializeField] private ItemNames _id;

        public ItemNames ID => _id;
    }
}