using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _doorSprite = null;
    [SerializeField]
    private Transform _doorTransform = null;
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
        StartCoroutine(IsTargetReachedCoroutine(collision.transform));

        Debug.Log("Player reached the target!");
    }

    private IEnumerator IsTargetReachedCoroutine(Transform player)
    {
        Vector3 playerToTargetVector = _doorTransform.position - player.position;
        Vector3 normalized = playerToTargetVector.normalized;
        float margin = 0.1f;
        while ((playerToTargetVector.normalized - normalized).sqrMagnitude < margin)
        {
            playerToTargetVector = _doorTransform.position - player.position;
            yield return new WaitForSeconds(0.25f);
        }
        TargetReachedEvent?.Invoke(this, EventArgs.Empty);
        _doorSprite.color = _closedColor;
    }
}
