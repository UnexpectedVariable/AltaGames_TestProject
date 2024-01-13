using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform = null;
    [SerializeField]
    private Rigidbody2D _rigidbody2D = null;
    [SerializeField]
    private CircleCollider2D _circleCollider2D = null;

    public event EventHandler<CollisionEventArgs> CollisionEvent = null;
    public Rigidbody2D Rigidbody2D
    {
        get => _rigidbody2D;
    }
    public CircleCollider2D CircleCollider2D 
    { 
        get => _circleCollider2D; 
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

    public void Reset()
    {
        _transform.localScale = Vector3.zero;
        Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        CircleCollider2D.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger stay detected");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter detected");
    }
}
