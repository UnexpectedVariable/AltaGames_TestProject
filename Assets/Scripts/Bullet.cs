using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform = null;
    [SerializeField]
    private Rigidbody2D _rigidbody2D = null;

    public event EventHandler<CollisionEventArgs> CollisionEvent = null;
    public Rigidbody2D Rigidbody2D
    {
        get => _rigidbody2D;
    }

    void Start()
    {
        _transform = this.gameObject.transform;
        _transform.localScale = Vector3.zero;   
    }

    public void Enlarge2D(float multiplier)
    {
        _transform.localScale += new Vector3(multiplier, multiplier);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEventArgs args = new CollisionEventArgs();
        args.Bullet = this;

        EventHandler<CollisionEventArgs> handler = CollisionEvent;
        handler?.Invoke(handler, args);
    }
}
