using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.LevelManagement.Rooms
{
    public class RoomComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onComplete;
        [SerializeField] private string _id;

        public string ID => _id;


        private RoomsManager _roomsManager;
        private void Start()
        {
            _roomsManager = FindAnyObjectByType<RoomsManager>();
        }

        public void Clear()
        {
            _roomsManager.Clear(_id);
        }

        public void InvokeAction()
        {
            _onComplete?.Invoke();
        }
    }
}