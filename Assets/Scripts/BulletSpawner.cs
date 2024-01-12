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
    private float _scaleMultiplier = 0;
    private Bullet _bullet = null;
    public Bullet Bullet
    {
        get => _bullet;
    }
    private bool _isShooting = false;

    private Vector2 _bulletSpawnPosition = Vector2.zero;
    public event EventHandler BulletFiredEvent = null;

    void Start()
    {
        Vector3 playerToTargetVec = _target.localPosition - _player.transform.localPosition;
        _bulletSpawnPosition = _player.transform.localPosition + _player.transform.localScale.x * 0.5f * playerToTargetVec.normalized;

        Debug.DrawLine(_target.position, _player.transform.position, Color.red, 60f);
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (!_bulletPrefab) return;
            if (!_bulletContainer) return;
            if (!_player) return;

            if(!_isShooting)
            {
                _isShooting = true;
                _bullet = Instantiate(_bulletPrefab, _bulletContainer.transform).GetComponent<Bullet>();
                _bullet.gameObject.transform.localPosition = _bulletSpawnPosition;
            }
            else
            {
                _bullet.Enlarge2D(_scaleMultiplier);
                _player.Shrink2D(_scaleMultiplier);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            _isShooting = false;

            EventHandler bulletFiredEvent = BulletFiredEvent;
            if(bulletFiredEvent != null)
            {
                bulletFiredEvent.Invoke(this, null);
            }
        }
    }


}
