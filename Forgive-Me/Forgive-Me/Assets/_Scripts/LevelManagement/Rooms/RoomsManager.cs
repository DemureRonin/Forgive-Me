using _Scripts.Model;
using UnityEngine;

namespace _Scripts.LevelManagement.Rooms
{
    public class RoomsManager : MonoBehaviour
    {
        [SerializeField] private RoomData _roomData;

        private void Start()
        {
            InvokeOnStart();
        }

        private void InvokeOnStart()
        {
            var roomsOnScene = FindObjectsByType<RoomComponent>(FindObjectsSortMode.None);
            foreach (var room in _roomData.Rooms)
            {
                foreach (var roomComponent in roomsOnScene)
                {
                    if (room.ID == roomComponent.ID && room.isCleared)
                    {
                        roomComponent.InvokeAction();
                    }
                }
            }
        }

        public void Clear(string id)
        {
            foreach (var room in _roomData.Rooms)
            {
                if (room.ID == id)
                {
                    room.isCleared = true;
                }
            }
        }

        private void OnEnable()
        {
            GameSession.OnSessionStart += InvokeOnStart;
        }


        private void OnDisable()
        {
            GameSession.OnSessionStart -= InvokeOnStart;
        }
    }
}