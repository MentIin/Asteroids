using System;
using CodeBase.Data.StatsSystem;
using CodeBase.Data.StatsSystem.Main;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Projectiles
{
    public class Bullet : Projectile
    {
        private Stats _stats;

        [Inject]
        public void Construct(Stats stats)
        {
            _stats = stats;
        }

        private void Start()
        {
            DestroyAfterLifetime();
        }

        private void Update()
        {
            transform.position += transform.right * Time.deltaTime* (_stats.GetStat<SpeedStat>().Value);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage();
                Destroy(this.gameObject);
            }
        }
        private async UniTaskVoid DestroyAfterLifetime()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_stats.GetStat<LifetimeStat>().Value));
            if (this != null)
                Destroy(this.gameObject);
        }
    }
}