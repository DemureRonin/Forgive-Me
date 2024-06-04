using System.Collections;
using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class DashWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _bar;

        private float _updateValue = 0.1f;
        private float _fillAmount;
        private WaitForSeconds _updateTime;

        private void Start()
        {
            _updateTime = new WaitForSeconds(_updateValue);
        }

        private void StartFilling(float coolDownTime)
        {
            StartCoroutine(Fill(coolDownTime));
        }

        private IEnumerator Fill(float coolDownTime)
        {
            _bar.SetProgress(0);
            var time = 0f;
            while (time < coolDownTime)
            {
                _fillAmount = time / coolDownTime;

                _bar.SetProgress(_fillAmount);
                time += Time.deltaTime + _updateValue;
                yield return _updateTime;
            }

            _bar.SetProgress(1);
        }

        private void OnEnable()
        {
            Player.OnDash += StartFilling;
        }

        private void OnDisable()
        {
            Player.OnDash -= StartFilling;
        }
    }
}