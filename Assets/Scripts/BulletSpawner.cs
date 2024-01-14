using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private Player _player = null;
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _scaleChangeValue = 0;
    [SerializeField]
    private float _playerScaleChangeMultiplier = 1;
    [SerializeField]
    private BulletPool _bulletPool = null;
    private Bullet _bullet = null;
    public Bullet Bullet
    {
        get => _bullet;
    }
    private bool _isShooting = false;
    private bool _isShootingAllowed = true;

    public bool IsShootingAllowed
    {
        set
        {
            _isShootingAllowed = value;
            if (_isShootingAllowed) return;
            if (!_isShooting) return;

            BulletEventArgs args = new BulletEventArgs();
            args.Bullet = _bullet;

            BulletDestroyedEvent?.Invoke(this, args);
        }
    }

    public event EventHandler BulletFiredEvent = null;
    public event EventHandler<BulletEventArgs> BulletDestroyedEvent = null;

    void Update()
    {
        if (!_isShootingAllowed) return;

        if(Input.GetMouseButton(0))
        {
            if(!_isShooting)
            {
                _isShooting = true;
                _bullet = _bulletPool.Get();
                _bullet.gameObject.SetActive(true);
                Vector3 playerToTargetVec = _target.localPosition - _player.transform.localPosition;
                Vector2 bulletSpawnPosition = _player.transform.localPosition + _player.transform.localScale.x * 0.5f * playerToTargetVec.normalized;
                _bullet.gameObject.transform.localPosition = bulletSpawnPosition;
            }
            else
            {
                _bullet.Enlarge2D(_scaleChangeValue);
                _player.Shrink2D(_scaleChangeValue * _playerScaleChangeMultiplier);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            _isShooting = false;

            EventHandler bulletFiredEvent = BulletFiredEvent;
            bulletFiredEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
