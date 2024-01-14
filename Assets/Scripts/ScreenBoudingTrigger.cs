using System;
using UnityEngine;

public class ScreenBoundingTrigger : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider2D = null;
    private RectTransform _rectTransform = null;

    public event EventHandler<BulletEventArgs> BulletOffscreenEvent = null;
    private void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        boxCollider2D.size = _rectTransform.rect.size;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Bullet") return;

        BulletEventArgs args = new BulletEventArgs();
        args.Bullet = collision.gameObject.GetComponent<Bullet>();

        if (IsScreenBound(args.Bullet.transform.localPosition)) return;
        BulletOffscreenEvent?.Invoke(this, args);
    }

    private bool IsScreenBound(Vector2 position)
    {
        return _rectTransform.rect.Contains(position);
    }
}
