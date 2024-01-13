using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private int _bulletCount = 0;
    [SerializeField]
    private bool _isAutoExpand = true;
    [SerializeField]
    private Bullet _bulletPrefab = null;
    [SerializeField]
    private CollisionManager _collisionManager = null;

    private MonoPoolList<Bullet> _bulletList = null;

    private void Start()
    {
        _bulletList = new MonoPoolList<Bullet>(_bulletPrefab, this.gameObject, _bulletCount, _isAutoExpand);
        _collisionManager.BulletDestroyedEvent += HandleBulletDestruction;

        InitializePooledBullets();
    }

    private void InitializePooledBullets()
    {
        foreach (Bullet bullet in _bulletList.Pool)
        {
            InitializePooledBullet(bullet);
        }
    }

    private void InitializePooledBullet(Bullet instance)
    {
        _collisionManager.Subscribe(instance);
    }

    public Bullet Get()
    {
        if(_bulletList.Get(out Bullet instance))
        {
            return instance;
        }
        InitializePooledBullet(instance);
        return instance;
    }

    private void HandleBulletDestruction(object sender, CollisionEventArgs args)
    {
        args.Bullet.Reset();
        _bulletList.Release(args.Bullet);
    }
}
