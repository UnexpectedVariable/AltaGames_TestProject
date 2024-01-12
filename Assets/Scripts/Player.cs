using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform _transform = null;
    private Vector3 Scale
    {
        get => _transform.localScale;
        set
        {
            _transform.localScale = value;
            if(_transform.localScale.magnitude <= 0) //hcange 0 to minimal value
            {
                //lose callback
            }
        }
    }

    void Start()
    {
        _transform = this.gameObject.transform;
    }

    public void Shrink2D(float multiplier)
    {
        Scale -= new Vector3(multiplier, multiplier);
    }
}
