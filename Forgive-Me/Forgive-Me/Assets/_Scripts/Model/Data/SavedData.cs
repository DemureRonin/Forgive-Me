using UnityEngine;

namespace _Scripts.Model.Data
{
    [CreateAssetMenu(menuName = "SavedData", fileName = "SavedData")]
    public class SavedData : ScriptableObject
    {
        public string _lastCheckpoint = "checkpoint";
        public string _lastScene = "Prologue1";
    }
}