using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int _startSize;
    [SerializeField] private List<PooledObject> _objectPool = new List<PooledObject>();
    [SerializeField] private List<PooledObject> _usedPool = new List<PooledObject>();

    private PooledObject _tempObject;

    void Start()
    {
        Initialized();
    }

    void Update()
    {

    }

    private void Initialized()
    {
        for (int i = 0; i < _startSize; i++)
        {
            AddNewObject();
        }
    }

    void AddNewObject()
    {
        _tempObject = Instantiate(objectToPool, transform).GetComponent<PooledObject>();
        _tempObject.gameObject.SetActive(false);
        _tempObject.SetdObjectPool(this);
        _objectPool.Add(_tempObject);
    }

    public PooledObject GetPooledObject()
    {
        PooledObject tempObject;
        if (_objectPool.Count > 0)
        {
            tempObject = _objectPool[0];
            _usedPool.Add(tempObject);
            _objectPool.RemoveAt(0);
        }
        else
        {
            AddNewObject();
            tempObject = GetPooledObject();
        }

        tempObject.gameObject.SetActive(true);
        tempObject.ResetObject();

        return tempObject;
    }

    public void DestroyPooledObject(PooledObject obj, float time = 0)
    {
        if (time == 0)
        {

            obj.Destroy();
        }
        else
        {
            obj.Destroy(time);
        }
    }

    public void RestoreObject(PooledObject obj)
    {
        obj.gameObject.SetActive(false);
        _usedPool.Remove(obj);
        _objectPool.Add(obj);

    }

}
