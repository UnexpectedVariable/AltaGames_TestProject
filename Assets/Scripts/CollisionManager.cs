using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private float _explosionMultiplier = 1f;
    public event EventHandler<CollisionEventArgs> BulletDestroyedEvent = null;

    public void Subscribe(Bullet bullet)
    {
        if(!bullet) return;

        bullet.CollisionEvent += HandleBulletCollision;
    }

    public void HandleBulletCollision(object sender, CollisionEventArgs args)
    {
        Debug.Log("Bullet collision event invoked");
        //make an explosion
        //destroy bullet
        //destroy obstacles
        StartCoroutine(CollisionCoroutine(args));
        
    }

    private IEnumerator CollisionCoroutine(CollisionEventArgs args)
    {
        //stop the bullet and prevent further interaction
        args.Bullet.Rigidbody2D.bodyType = RigidbodyType2D.Static;
        args.Bullet.CircleCollider2D.enabled = false;

        //simulate explosion
        args.Bullet.transform.localScale *= _explosionMultiplier;

        var hitObstacles = Physics2D.OverlapCircleAll(args.Bullet.transform.position, args.Bullet.transform.lossyScale.x * 0.5f);

        //infect obstacles
        foreach(var hit in hitObstacles)
        {
            if (hit.gameObject.tag == "Obstacle")
            {
                hit.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        yield return new WaitForSeconds(0.34f);

        //release obstacles
        foreach (var hit in hitObstacles)
        {
            if (hit.gameObject.tag == "Obstacle")
            {
                hit.gameObject.SetActive(false);
            }
        }

        //release bullet
        BulletDestroyedEvent?.Invoke(this, args);

        //check if player can fit through

    }
}
