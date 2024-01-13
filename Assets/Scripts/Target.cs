using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D _boxCollider2D = null;
    [SerializeField]
    private SpriteRenderer _doorSprite = null;
    private Color _closedColor = Color.black;

    public event EventHandler TargetReachedEvent = null;

    private void Start()
    {
        _closedColor = _doorSprite.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        _doorSprite.color = Color.white;
        StartCoroutine(TargetReachedCoroutine(collision.transform));

        Debug.Log("Player reached the target!");
    }

    private IEnumerator TargetReachedCoroutine(Transform player)
    {
        while((player.localPosition - transform.localPosition).magnitude > 1f)
        {
            yield return new WaitForSeconds(0.05f);
        }
        TargetReachedEvent?.Invoke(this, EventArgs.Empty);
        _doorSprite.color = _closedColor;
    }
}
