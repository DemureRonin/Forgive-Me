using System.Collections;
using System.Collections.Generic;
using _Scripts.Model;
using _Scripts.UI.Dialogs;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Utils
{
    public class WaterDeath : MonoBehaviour
    {
        [SerializeField] private int _deathCount;
        [SerializeField] private List<DialogData> _dialogs;
        [SerializeField] private UnityEvent _onComplete;

        private DialogData _currentDialog;
        private DialogObserver _observer;
        private bool _lastDeathIsByWater;


        private void Awake()
        {
            _observer = GetComponent<DialogObserver>();
        }

        private void Check()
        {
            if (_lastDeathIsByWater && _deathCount < _dialogs.Count)
                StartCoroutine(Count());
        }

        private void SetDeath()
        {
            _lastDeathIsByWater = true;
        }

        private IEnumerator Count()
        {
            for (var i = 0; i < _dialogs.Count; i++)
            {
                if (i != _deathCount) continue;
                _currentDialog = _dialogs[i];
                break;
            }

            _deathCount++;
            yield return new WaitForSeconds(0.3f);
            if (_deathCount == _dialogs.Count)
            {
                _observer.SetEventOnComplete(_onComplete); 
            }
            _observer.SetDialog(_currentDialog);
            _observer.Show();
            _lastDeathIsByWater = false;
        }

        private void OnEnable()
        {
            WaterDeathObserver.OnWaterDeath += SetDeath;
            GameSession.OnSessionStart += Check;
        }

        private void OnDisable()
        {
            WaterDeathObserver.OnWaterDeath -= SetDeath;
            GameSession.OnSessionStart -= Check;
        }
    }
}