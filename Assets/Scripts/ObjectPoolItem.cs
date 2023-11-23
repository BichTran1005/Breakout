using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject Prefab;
    public int Amount;
    public string PoolId
    {
        get { return Prefab.name; }
    }
}
