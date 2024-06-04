using UnityEngine;

namespace _Scripts.Model
{
    [CreateAssetMenu(menuName = "Model/PerksModel", fileName = "Perks")]
    public class PlayerPerksModel : ScriptableObject
    {
        [SerializeField] private bool _isDashingUnlocked = true;
        [SerializeField] private bool _isSphereAllowed = true;
        
        public bool IsDashingUnlocked => _isDashingUnlocked;

        public bool IsSphereAllowed => _isSphereAllowed;
        
        public void EnableSphere()
        {
            _isSphereAllowed = true;
        }

        public void EnableDashing()
        {
            _isDashingUnlocked = true;
        } 
        
        public void DisableSphere()
        {
            _isSphereAllowed = false;
        }

        public void DisableDashing()
        {
            _isDashingUnlocked = false;
        }
    }
}