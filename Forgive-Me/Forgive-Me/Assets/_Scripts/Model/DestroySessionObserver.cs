using UnityEngine;

namespace _Scripts.Model
{
    public class DestroySessionObserver : MonoBehaviour
    {
        public delegate void DestroyEvent();

        public static event DestroyEvent OnDestroySession;

        public void DestroySession()
        {
            OnDestroySession?.Invoke();
        }
    }
}