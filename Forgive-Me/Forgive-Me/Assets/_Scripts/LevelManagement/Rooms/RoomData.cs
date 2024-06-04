using System;
using UnityEngine;

namespace _Scripts.LevelManagement.Rooms
{
    [CreateAssetMenu(fileName = "RoomData")]
    public class RoomData : ScriptableObject
    {
        [SerializeField] private Room[] _rooms;

        public Room[] Rooms => _rooms;
    }

    [Serializable]
    public class Room
    {
        [SerializeField] private string _id;

        public string ID => _id;
        public bool isCleared;
    }
}