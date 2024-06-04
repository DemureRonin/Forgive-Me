using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components.Triggers
{
    public class EnterCollision : MonoBehaviour
    {
        [SerializeField] private Actions[] _actions;


        public  void OnCollisionEnter(Collision otherCollision)
        {
            foreach (var action in _actions)
            {
                if (!otherCollision.gameObject.CompareTag(action.OtherTag)) continue;
                action.GameEvent.Invoke(otherCollision.gameObject);
               
                return;
            }
        }
    }

    [Serializable]
    public class Actions
    {
        [SerializeField] private string _otherTag = "Player";
        public string OtherTag => _otherTag;

        [SerializeField] private EnterEvent _gameEvent;

        public EnterEvent GameEvent => _gameEvent;
    }

    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
    }
}