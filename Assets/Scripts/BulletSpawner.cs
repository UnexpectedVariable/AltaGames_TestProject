using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab = null;
    [SerializeField]
    private GameObject _bulletContainer = null;
    [SerializeField]
    private Player _player = null;
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _scaleChangeValue = 0;
    [SerializeField]
    private float _playerScaleChangeMultiplier = 1;
    /*[SerializeField]
    private CollisionManager _collisionManager = null;*/
    [SerializeField]
    private BulletPool _bulletPool = null;
    private Bullet _bullet = null;
    public Bullet Bullet
    {
        get => _bullet;
    }
    private bool _isShooting = false;

    public event EventHandler BulletFiredEvent = null;

    void Start()
    {
        Debug.DrawLine(_target.position, _player.transform.position, Color.red, 60f);
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            {
                if (!_bulletPrefab) return;
                if (!_bulletContainer) return;
                if (!_player) return;
                //if (!_collisionManager) return;
                if (!_bulletPool) return;
            }

            if(!_isShooting)
            {
                _isShooting = true;
                //bullet = Instantiate(_bulletPrefab, _bulletContainer.transform).GetComponent<Bullet>();
                _bullet = _bulletPool.Get();
                _bullet.gameObject.SetActive(true);
                Vector3 playerToTargetVec = _target.localPosition - _player.transform.localPosition;
                Vector2 bulletSpawnPosition = _player.transform.localPosition + _player.transform.localScale.x * 0.5f * playerToTargetVec.normalized;
                _bullet.gameObject.transform.localPosition = bulletSpawnPosition;

                //_collisionManager.Subscribe(_bullet);
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
