using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private short _obstacleCount = 0;
    [SerializeField]
    private bool _isSpawnerActive = true;
    [SerializeField]
    private bool _isSeedRandomized = false;
    [SerializeField]
    private GameObject _obstaclePrefab = null;
    [SerializeField]
    private RectTransform _obstacleContainerTransform = null;
    private int _seed = 0;


    void Start()
    {
        if (!_isSpawnerActive) return;
        if (_obstaclePrefab == null) return;
        if (_obstacleContainerTransform == null) return;
        /*else
        {
            Debug.Log($"Obstacle container dimensions: width={_obstacleContainerTransform.rect.width}, height={_obstacleContainerTransform.rect.height}");
        }*/

        System.Random rng = null;
        if (_isSeedRandomized)
        {
            rng = new System.Random();
        }
        else
        {
            rng = new System.Random(_seed);
        }

        for(int i = 0; i < _obstacleCount; i++)
        {
            Vector3 position = Vector2.zero;
            int halfWidth = (int)(_obstacleContainerTransform.rect.width * 0.5);
            int halfHeight = (int)(_obstacleContainerTransform.rect.height * 0.5);

            position.x = rng.Next(-halfWidth, halfWidth);
            position.y = rng.Next(-halfHeight, halfHeight);

            GameObject lastObstacle = Instantiate(_obstaclePrefab, _obstacleContainerTransform);
            lastObstacle.transform.localPosition = position;
        }
    }
}