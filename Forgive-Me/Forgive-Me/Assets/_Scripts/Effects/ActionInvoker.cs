using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Effects
{
    public class ActionInvoker : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void Start()
        {
            _action?.Invoke();
        }
    }
}