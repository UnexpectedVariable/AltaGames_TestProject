using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTraverseManager : MonoBehaviour
{
    [SerializeField]
    private float _traverseSpeed = 1f;
    [SerializeField]
    private Player _player = null;
    [SerializeField]
    private Transform _target = null;
    private Bullet _bullet = null;

    public BulletSpawner Spawner = null; //refactor later?

    void Start()
    {
        Spawner.BulletFiredEvent += HandleFireEvent;
    }

    private IEnumerator TraverseCoroutine()
    {
        while (_bullet)
        {
            Vector2 traverseVector = (_target.localPosition - _bullet.transform.localPosition).normalized * _traverseSpeed;
            _bullet.transform.localPosition += new Vector3(traverseVector.x, traverseVector.y);

            yield return new WaitForSeconds(0.034f);
        }
    }

    private void HandleFireEvent(object sender, EventArgs args)
    {
        _bullet = Spawner.Bullet;
        if (!_bullet) return;

        StartCoroutine(TraverseCoroutine());
    }
}
