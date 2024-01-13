using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;

public class ScreenBoundingTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider2D = null;
    private RectTransform _rectTransform = null;

    public event EventHandler<CollisionEventArgs> BulletOffscreenEvent = null;
    private void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        boxCollider2D.size = _rectTransform.rect.size;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Bullet") return;

        CollisionEventArgs args = new CollisionEventArgs();
        args.Bullet = collision.gameObject.GetComponent<Bullet>();

        if (IsScreenBound(args.Bullet.transform.localPosition)) return;
        BulletOffscreenEvent?.Invoke(this, args);
    }

    private bool IsScreenBound(Vector2 position)
    {
        return _rectTransform.rect.Contains(position);
    }
}
