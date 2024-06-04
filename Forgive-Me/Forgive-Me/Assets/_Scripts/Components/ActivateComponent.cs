using _Scripts.Model;
using UnityEngine;

namespace _Scripts.Components
{
    public class ActivateComponent : MonoBehaviour
    {
        [SerializeField] private ItemNames _id;
        [SerializeField] private GameObject _object;
        [SerializeField] private GivenCollectibles _givenCollectibles;

        private void Start()
        {
            Activate();
        }

        private void Activate()
        {
            foreach (var givenCollectible in _givenCollectibles.GivenItems)
            {
                if (_id != givenCollectible) continue;
                _object.SetActive(true);
            }
        }
    }
}