using System;
using System.Collections;
using UnityEngine;

namespace _Scripts.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        [SerializeField] private bool _destroySelf;
        [SerializeField] private bool _destroyWithDelay;
        [SerializeField] private bool _destroyOnStartWithDelay;
        [SerializeField] private float _lifeTime;

        private Coroutine _coroutine;

        private void Start()
        {
            if (_destroyOnStartWithDelay)
            {
                _coroutine = StartCoroutine(StartDestroying());
            }
        }

        public void DestroyGameObject()
        {
            if (_coroutine != null) return;
            StartCoroutine(StartDestroying());
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private IEnumerator StartDestroying()
        {
            if (_destroyWithDelay)
            {
                yield return new WaitForSeconds(_lifeTime);
            }

            Destroy(_destroySelf ? gameObject : _objectToDestroy);
        }
    }
}