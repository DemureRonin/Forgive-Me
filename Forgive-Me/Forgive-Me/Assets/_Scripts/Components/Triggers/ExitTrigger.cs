using UnityEngine;

namespace _Scripts.Components.Triggers
{
    public class ExitTrigger : MonoBehaviour
    {
        [SerializeField] protected Actions[] _actions;

        private void OnTriggerExit(Collider otherCollider)
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