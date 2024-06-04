using UnityEngine;

namespace _Scripts.UI.Items
{
    [CreateAssetMenu(fileName = ("ItemData"))]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _name;
        [TextArea] [SerializeField] private string _description;
        [SerializeField] private string _buttonText;
        [SerializeField] private GameObject _prefab;

        public string Name => _name;

        public string Description => _description;

        public GameObject Prefab => _prefab;
        public string ButtonText => _buttonText;
    }
}