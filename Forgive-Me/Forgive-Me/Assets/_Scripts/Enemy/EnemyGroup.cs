using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Enemy
{
    public class EnemyGroup : MonoBehaviour
    {
        [SerializeField] private List<EnemyAI> _enemies;
        [SerializeField] private List<ShootingEnemyAI> _shootingEnemies;
        [SerializeField] private UnityEvent _onAllDead;
        private int _count;

        public void OnSeePlayer(GameObject player)
        {
            if (_enemies.Count > 0)
                foreach (var enemy in _enemies)
                {
                    enemy.OnSeePlayer(player);
                }

            if (_shootingEnemies.Count <= 0) return;

            foreach (var enemy in _shootingEnemies)
            {
                enemy.OnSeePlayer(player);
            }
        }

        public void OnEnemyDeath()
        {
            _count++;
            if (_count != _enemies.Count +_shootingEnemies.Count ) return;
            _onAllDead?.Invoke();
        }
    }
}