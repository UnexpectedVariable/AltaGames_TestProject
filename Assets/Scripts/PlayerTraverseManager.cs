using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraverseManager : MonoBehaviour
{
    [SerializeField]
    private float _traverseForce = 1f;
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private Player _player = null;
    [SerializeField] GameDirector _gameDirector = null;

    void Start()
    {
        if (!_gameDirector) return;

        _gameDirector.PathClearEvent += HandlePathClearEvent;
    }

    private void HandlePathClearEvent(object sender, EventArgs args)
    {
        Debug.Log("Player path is clear!");

        Vector2 forceVector = (_target.localPosition - _player.transform.localPosition).normalized * _traverseForce;
        _player.Rigidbody2D.AddForce(forceVector);
    }
}
