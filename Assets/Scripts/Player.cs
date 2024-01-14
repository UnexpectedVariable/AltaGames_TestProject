using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D _circleCollider2D = null;
    [SerializeField]
    private Rigidbody2D _rigidbody2D = null;
    private Transform _transform = null;
    private Vector3 Scale
    {
        get => _transform.localScale;
        set
        {
            _transform.localScale = value;
            if (_transform.localScale.magnitude <= 0) //change 0 to minimal value
            {
                //lose callback
            }
        }
    }

    public CircleCollider2D CircleCollider2D
    {
        get => _circleCollider2D;
    }
    public Rigidbody2D Rigidbody2D
    {
        get => _rigidbody2D;
    }

    void Start()
    {
        _transform = this.gameObject.transform;
    }

    public void Shrink2D(float multiplier)
    {
        Scale -= new Vector3(multiplier, multiplier);
    }

    public void Reset()
    {
        Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        CircleCollider2D.enabled = true;
    }
}
