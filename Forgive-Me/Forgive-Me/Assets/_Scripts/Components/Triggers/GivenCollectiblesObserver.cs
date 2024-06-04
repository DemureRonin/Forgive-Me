using _Scripts.Model;
using UnityEngine;

namespace _Scripts.Components.Triggers
{
    public class GivenCollectiblesObserver : MonoBehaviour
    {
        [SerializeField] private ItemNames _id;
        public delegate void GiveEvent(ItemNames id);

        public static event GiveEvent OnGive;

        public void Give()
        {
            OnGive?.Invoke(_id);
        }
    }
}