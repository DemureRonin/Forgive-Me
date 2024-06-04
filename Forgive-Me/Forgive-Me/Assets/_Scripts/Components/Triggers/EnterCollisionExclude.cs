using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components.Triggers
{
    public class EnterCollisionExclude : MonoBehaviour
    {
        [SerializeField] private string _otherTag;
        [SerializeField] private EnterEvent _action;

        public void OnCollisionEnter(Collision otherCollision)
        {
            if (!otherCollision.gameObject.CompareTag(_otherTag))
                _action?.Invoke(otherCollision.gameObject);
        }
    }
}