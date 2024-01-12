using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform = null;

    void Start()
    {
        _transform = this.gameObject.transform;
        _transform.localScale = Vector3.zero;   
    }

    void Update()
    {
        
    }

    public void Enlarge2D(float multiplier)
    {
        _transform.localScale += new Vector3(multiplier, multiplier);
    }
}
