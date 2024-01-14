using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;
    [SerializeField]
    private CircleCollider2D _circleCollider2D = null;
    [SerializeField]
    private Rigidbody2D _rigidbody2D = null;
    [SerializeField]
    private float _criticalScaleMultiplier = 0.1f;
    [SerializeField]
    private bool _visualizeReachingCriticalScale = true;
    [SerializeField]
    private Color _criticalScaleColor = Color.red;
    private float _criticalScaleValueSqr = 0f;
    private Vector3 _scaleColorShiftVec = Vector3.zero;
    private Transform _transform = null;
    private Vector3 Scale
    {
        get => _transform.localScale;
        set
        {
            _transform.localScale = value;
            if (_transform.localScale.sqrMagnitude <= _criticalScaleValueSqr)
            {
                CriticalScaleReachedEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public event EventHandler CriticalScaleReachedEvent = null;

    public CircleCollider2D CircleCollider2D
    {
        get => _circleCollider2D;
    }
    public Rigidbody2D Rigidbody2D
    {
        get => _rigidbody2D;
    }
    public Vector3 ScaleColorShiftVec
    {
        set => _scaleColorShiftVec = value;
    }

    void Start()
    {
        _transform = this.gameObject.transform;

        _criticalScaleValueSqr = _transform.localScale.sqrMagnitude * _criticalScaleMultiplier;

        if (!_visualizeReachingCriticalScale) return;

    }

    public void Shrink2D(float step)
    {
        Scale -= new Vector3(step, step);

        if (!_visualizeReachingCriticalScale) return;

        if(_scaleColorShiftVec ==  Vector3.zero)
        {
            CalculateColorShiftVec(step);
        }

        ShiftColor();
    }

    private void ShiftColor()
    {
        float red = _spriteRenderer.color.r + _scaleColorShiftVec.x;
        float green = _spriteRenderer.color.g + _scaleColorShiftVec.y;
        float blue = _spriteRenderer.color.b + _scaleColorShiftVec.z;
        _spriteRenderer.color = new Color(red, green, blue);
    }

    public void Reset()
    {
        Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        CircleCollider2D.enabled = true;
    }

    private void CalculateColorShiftVec(float step)
    {
        float scaleDifference = _transform.localScale.x - _transform.localScale.x * _criticalScaleMultiplier;
        float stepCount = scaleDifference / step;

        Vector3 colorDifference = (Vector4)(_criticalScaleColor - _spriteRenderer.color);
        _scaleColorShiftVec = colorDifference / stepCount;
    }
}
