using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Effects;
using _Scripts.PlayerScripts;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Player _player;
        private Spawner _spawner;

        private void Awake()
        {
            _spawner = GetComponent<Spawner>();
        }

        private void Start()
        {
            _player = FindAnyObjectByType<Player>();
            StartCoroutine(LifeTime());

        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(5f);
            _spawner.Spawn(transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void Update()
        {
            GoToPlayer();
        }

        private void GoToPlayer()
        {
            transform.position = Vector3.Lerp(transform.position,_player.transform.position, _speed * Time.deltaTime );
        }
    }
}