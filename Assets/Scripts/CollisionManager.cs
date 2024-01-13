using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
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
        BulletDestroyedEvent?.Invoke(this, args);
        //destroy obstacles
    }
}
