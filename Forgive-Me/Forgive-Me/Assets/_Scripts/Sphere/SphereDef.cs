using UnityEngine;

namespace _Scripts.Sphere
{
    [CreateAssetMenu(fileName = "SphereDef", menuName = "Sphere")]
    public class SphereDef : ScriptableObject
    {
        [SerializeField] private GameObject _roundSphere;
        [SerializeField] private GameObject _icoSphere;
        [SerializeField] private GameObject _dodecoSphere;

        public GameObject RoundSphere => _roundSphere;

        public GameObject IcoSphere => _icoSphere;

        public GameObject DodecoSphere => _dodecoSphere;
    }
}