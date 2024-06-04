using UnityEngine;

namespace _Scripts.UI
{
    public class ChoiceWindowObserver : MonoBehaviour
    {
        public delegate void ChoiceWindowEvent();

        public static event ChoiceWindowEvent OnWindowShow;

        public void ShowWindow()
        {
            OnWindowShow?.Invoke();   
        }
    }
}