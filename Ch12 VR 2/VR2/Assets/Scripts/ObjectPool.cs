using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject ObjectPrefab = null;
    public int PoolSize = 10;

    private void Start()
    {
        GeneratePool();
    }

    public void GeneratePool()
    {
        for(int i = 0; i < PoolSize; i++)
        {
            // instances will be added as child objects and hidden away until needed
            GameObject Obj = Instantiate(ObjectPrefab, Vector3.zero, Quaternion.identity, transform);
            // active false means script event functions such as Update will not occur, physics not updated, collisions do not occur
            Obj.SetActive(false);
        }
    }

    public Transform Spawn(Transform Parent, Vector3 Position = new Vector3(), Quaternion Rotation = new Quaternion(), Vector3 Scale = new Vector3())
    {
        if (transform.childCount == 0) return null;

        Transform Child = transform.GetChild(0);
        Child.SetParent(Parent);
        Child.position = Position;
        Child.rotation = Rotation;
        Child.localScale = Scale;
        Child.gameObject.SetActive(true);
        return Child;
    }

    public void DeSpawn(Transform ObjectToDespawn)
    {
        // adds the object to the pool's transform so it can be selected for later re-use, makes inactive and position to 0,0,0
        ObjectToDespawn.gameObject.SetActive(false);
        ObjectToDespawn.SetParent(transform);
        ObjectToDespawn.position = Vector3.zero;
    }
}
