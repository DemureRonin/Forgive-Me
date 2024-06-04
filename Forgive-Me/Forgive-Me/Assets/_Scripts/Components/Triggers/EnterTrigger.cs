using UnityEngine;

namespace _Scripts.Components.Triggers
{
    public class EnterTrigger : MonoBehaviour
    {
        [SerializeField] protected Actions[] _actions;

        private void OnTriggerEnter(Collider otherCollider)
        {
            foreach (var action in _actions)
            {
                if (!otherCollider.CompareTag(action.OtherTag)) continue;
                action.GameEvent.Invoke(otherCollider.gameObject);
                return;
            }
        }
    }
}