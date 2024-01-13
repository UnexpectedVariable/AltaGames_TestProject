using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private BulletPool _bulletPool = null;
    [SerializeField]
    private GameObject _player = null;
    [SerializeField]
    private GameObject _target = null;

    public event EventHandler PathClearEvent = null;
    public event EventHandler LossEvent = null;

    private void Start()
    {
        {
            if (!_bulletPool) return;
            if (!_player) return;
            if (!_target) return;
        }

        _bulletPool.BulletReleasedEvent += HandleBulletRelease;
    }

    private void HandleBulletRelease(object sender, EventArgs args)
    {
        Vector2 castOrigin = _player.transform.position;
        float radius = _player.transform.lossyScale.x * 0.5f;
        Vector2 direction = _target.transform.position - _player.transform.position;
        int layerMask = 1 << 6;

        if (!Physics2D.CircleCast(castOrigin, radius, direction, direction.magnitude, layerMask))
        {
            PathClearEvent?.Invoke(this, EventArgs.Empty);
        }
        else Debug.Log("Player path is not clear!");
    }
}