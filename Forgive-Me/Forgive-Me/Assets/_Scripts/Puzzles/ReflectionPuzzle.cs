using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Puzzles
{
    public class ReflectionPuzzle : MonoBehaviour
    {
        [SerializeField] private List<ReflectionPane> _panes;

        [SerializeField] private UnityEvent _onComplete;
        private bool _isComplete;
        
        private int _activeCount;

        public void Check()
        {
            if (_isComplete) return;
            foreach (var pane in _panes)
            {
                if (pane.IsActivated)
                {
                    _activeCount++;
                }
            }

            if (_activeCount != _panes.Count)
            {
                _activeCount = 0;
                return;
            }

            foreach (var pane in _panes)
            {
                pane.OnComplete();
            }
            
            _onComplete?.Invoke();
            _panes[0].AudioSource.PlayOneShot(_panes[0].ONCompleteSound);
            _isComplete = true;
        }
    }
}