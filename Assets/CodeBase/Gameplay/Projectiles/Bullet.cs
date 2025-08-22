using System;
using CodeBase.Data.StatsSystem.Main;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Projectiles
{
    public class Bullet : Projectile
    {
        [Inject]
        public void Construct(Stats stats)
        {
            
        }

        private void Update()
        {
            transform.position += transform.right * Time.deltaTime*10f;
        }
    }
}