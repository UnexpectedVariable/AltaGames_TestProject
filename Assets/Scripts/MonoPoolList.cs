using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPoolList<T> where T : MonoBehaviour
{
    private T _pooledObject = null;
    private GameObject _parent = null;
    private List<T> _pool = null;
    public List<T> Pool
    {
        get => _pool;
    }

    private bool _isAutoExpand = false;
    public  bool IsAutoExpand
    {
        get => _isAutoExpand;
        set => _isAutoExpand = value;
    }

    public MonoPoolList(T pooledObject, GameObject parent, int count, bool isAutoExpand = false)
    {
        _pooledObject = pooledObject;
        _parent = parent;
        IsAutoExpand = isAutoExpand;

        if(count > 0) InitializePool(count);
    }

    private void InitializePool(int count)
    {
        _pool = new List<T>(count);

        for (int i = 0; i < _pool.Capacity; i++)
        {
            InstantiatePoolObject(false);
        }
    }

    private T InstantiatePoolObject(bool isActive)
    {
        {
            if (!_pooledObject) return null;
            if (!_parent) return null;
        }

        T objectInstance = UnityEngine.Object.Instantiate(_pooledObject, _parent.transform);
        objectInstance?.gameObject.SetActive(isActive);
        _pool.Add(objectInstance);
        return objectInstance;
    }

    /// <summary>
    /// Return true only if inactive preexisting instance was found, otherwise create new instance if allowed and return false
    /// </summary>
    /// <param name="objectInstance"></param>
    /// <returns></returns>
    public bool Get(out T objectInstance)
    {
        objectInstance = FindInactiveObject();
        if (objectInstance) return true;

        if (IsAutoExpand) objectInstance = InstantiatePoolObject(true);

        return false;
    }

    public void Release(T objectInstance)
    {
        objectInstance?.gameObject.SetActive(false);
    }

    private T FindInactiveObject()
    {
        foreach (T obj in _pool)
        {
            if(!obj.gameObject.activeSelf) return obj;
        }

        return null;
    }
}
