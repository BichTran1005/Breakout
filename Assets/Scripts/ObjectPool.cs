using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<ObjectPoolItem> itemToPool = new List<ObjectPoolItem>();
    private List<GameObject> pooledObjects = new List<GameObject>();

    private static Vector3 root = new Vector3(1000000f, 1000000f, 1000000f);

    private void Awake()
    {
        StartCoroutine(InitPool());
    }

    public void Return(GameObject go)
    {
        go.SetActive(false);
        go.transform.position = root;
        SetGameObjectParrent(go);
    }

    public static T Get<T>(string poolId)
    {
        return Instance.GetPooledObject(poolId).GetComponent<T>();
    }

    private GameObject GetPooledObject(string poolId)
    {
        GameObject pool = pooledObjects.Find(item => item != null && !item.activeInHierarchy && item.name == poolId);
        return pool == null ? AddGameObject(poolId) : pool;
    }

    private GameObject AddGameObject(string poolId)
    {
        ObjectPoolItem poolItem = itemToPool.Find(item => item.PoolId == poolId);

        if (poolItem == null)
        {
            poolItem = new ObjectPoolItem()
            {
                Prefab = Resources.Load<GameObject>(poolId)
            };
        }

        return AddGameObject(poolItem);
    }

    private GameObject AddGameObject(ObjectPoolItem item)
    {
        GameObject obj = Instantiate(item.Prefab);
        obj.name = item.PoolId;
        obj.transform.position = root;
        SetGameObjectParrent(obj);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }

    public IEnumerator InitPool()
    {
        foreach (var item in itemToPool)
        {
            for (var i = 0; i < item.Amount; i++)
            {
                AddGameObject(item);
                yield return null;
            }
        }
    }

    public void SetGameObjectParrent(GameObject obj)
    {
        obj.transform.SetParent(transform);
    }

    public void ReturnAll()
    {
        for (var i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].gameObject != null && pooledObjects[i].gameObject.activeSelf)
            {
                Return(pooledObjects[i].gameObject);
            }
        }
    }
}
