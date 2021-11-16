﻿using System;
using MarblesTD.Towers;
using UnityEngine;

namespace MarblesTD.Core
{
    public class TowerView : MonoBehaviour, ITowerView
    {
        [SerializeField] private GameObject projectilePrefab;

        public Projectile SpawnProjectile(ProjectileConfig config)
        {
            var go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var projectileView = go.GetComponent<IProjectileView>();
            var projectile = new Projectile(projectileView, new Vector2(transform.position.x, transform.position.z), config);

            Bootstrap.Instance.Projectiles.Add(projectile);
            return projectile;
        }
    }
}