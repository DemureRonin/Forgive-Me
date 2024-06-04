using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.LevelManagement
{
    [CreateAssetMenu(fileName = "Checkpoints")]
    public class CheckpointsData : ScriptableObject
    {
        public List<string> Checkpoints { get; } = new();
    }
}